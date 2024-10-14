using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Gun))]
public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    private bool _stopMovement = false;

    private Gun _enemyGun;
    private Health _enemyHealth;
    private Transform _spriteTransform;
    private Rigidbody2D _rigidbody2D;

    private Vector3 _targetPosition;
    [HideInInspector]
    public EnemyManager _enemyManager;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        _enemyGun = GetComponent<Gun>();

        _enemyHealth = new Health(enemyType.maxHealth, enemyType.maxHealth);

        _enemyGun.SetGun(enemyType.bulletType);
    }
    private void OnEnable()
    {
        _enemyHealth.ResetHealth();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            float damageToTake = 0;

            if(collision.gameObject.GetComponent<Explosion>()) { damageToTake = collision.gameObject.GetComponent<Explosion>().damage; }

            if (collision.gameObject.GetComponent<Bullet>())
            {
                damageToTake = collision.gameObject.GetComponent<Bullet>()._bulletDamage;
                collision.gameObject.GetComponent<Bullet>().TurnBulletOff();
            }
            TakeDamage(damageToTake);
        }
    }
    private void TakeDamage(float damageTaken)
    {
        if (_enemyHealth.ReduceHealth(damageTaken)) { return; }
        _enemyManager.EnemyDestroyed(gameObject);
    }
    private void Update()
    {
        CheckRadar();
        Move();
        Rotate();
    }
    private void CheckRadar()
    {
        _targetPosition = _enemyManager.player.position;
        float distanceToTarget = Vector3.Distance(_targetPosition, transform.position);
        if (distanceToTarget < enemyType.detectionRange * _enemyManager._difficultyMultiplier) { ShootAtTarget(); }
        _stopMovement = distanceToTarget <= enemyType.followRange * _enemyManager._difficultyMultiplier;
    }
    private void Move()
    {
        float movementSpeed = _stopMovement ? 0 : enemyType.speed;
        _rigidbody2D.velocity = (_targetPosition - transform.position).normalized * movementSpeed * _enemyManager._difficultyMultiplier;
    }
    private void Rotate()
    {
        Vector2 directionToTarget = (_targetPosition - transform.position).normalized;
        _spriteTransform.up = directionToTarget;
    }
    private void ShootAtTarget()
    {
        _enemyGun.Shoot(_targetPosition);
    }
}
