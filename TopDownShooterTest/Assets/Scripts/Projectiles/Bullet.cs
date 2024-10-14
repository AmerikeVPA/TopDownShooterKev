using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private bool _stopAtTarget;
    private float _lifetime, _speed;
    [HideInInspector]
    public float _bulletDamage;
    private Vector3 _startPosition, _targetPosition, _direction;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb2D;
    private GameObject _bulletExplosion;
    private UnityEvent _onTargetCollision = new UnityEvent();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
    }
    public void SetBullet(AmmoType ammoData)
    {
        transform.localScale = Vector3.one * 0.4f;
        _lifetime = ammoData.bulletLifetime;
        _speed = ammoData.bulletSpeed;
        _spriteRenderer.sprite = ammoData.bulletSprite;
        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.isTrigger = true;

        _onTargetCollision.AddListener(() => _rb2D.velocity = Vector2.zero);
        _onTargetCollision.AddListener(() => gameObject.SetActive(false));

        _stopAtTarget = ammoData.stopAtTarget;
        if (!_stopAtTarget) 
        { 
            _bulletDamage = ammoData.bulletDamage;
            return; 
        }
        CreateExplosionGO(ammoData.explosionRadious, ammoData.bulletDamage, ammoData.explosionPrefab);
        _onTargetCollision.AddListener(() => SpawnExplosion());
    }
    private void CreateExplosionGO(float explosionRadious, float damage, GameObject explosionPrefab)
    {
        _bulletExplosion = Instantiate(explosionPrefab);
        _bulletExplosion.tag = gameObject.tag;
        _bulletExplosion.GetComponent<Explosion>().RecieveData(explosionRadious, damage);
        _bulletExplosion.SetActive(false);
    }
    private void SpawnExplosion()
    {
        _bulletExplosion.transform.position = transform.position;
        _bulletExplosion.SetActive(true);
    }
    public void ShootBullet(Vector3 start, Vector3 end)
    {
        _startPosition = start;
        _targetPosition = end;
        _direction = (end - start).normalized;
        _direction.z = 0;
        transform.up = _direction;
        transform.position = _startPosition;
        _rb2D.velocity = _direction * _speed;
        StartCoroutine(CheckBulletLifetime());
        if (_stopAtTarget) 
        { 
            _targetPosition.z = 0;
            StartCoroutine(CheckBulletPosition()); 
        }
    }
    private IEnumerator CheckBulletLifetime()
    {
        yield return new WaitForSeconds(_lifetime);
        TurnBulletOff();
    }
    private IEnumerator CheckBulletPosition()
    {
        if(Vector3.Distance(transform.position, _targetPosition) < 0.1f) { TurnBulletOff(); }
        yield return new WaitForEndOfFrame();
        StartCoroutine(CheckBulletPosition());
    }
    public void TurnBulletOff()
    {
        _onTargetCollision.Invoke();
    }
}
