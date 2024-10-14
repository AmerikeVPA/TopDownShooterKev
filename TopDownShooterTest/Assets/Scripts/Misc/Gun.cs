using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool _canShoot = true, _limitAmmo;
    private float _fireRate;
    [HideInInspector]
    public int _maxAmmo, _currentAmmo;
    private AmmoType _ammoType;
    private Transform _bulletHolder;
    private Queue<Bullet> _bulletAvailable = new Queue<Bullet>();

    public void SetGun(AmmoType ammoType)
    {
        _maxAmmo = ammoType.ammoPoolSize;
        _limitAmmo = !ammoType.infiniteAmmo;
        _ammoType = ammoType;
        _currentAmmo = _maxAmmo;
        _fireRate = ammoType.fireRate;

        _bulletHolder = new GameObject($"{ammoType.name} Bullet Holder").transform;
        _bulletHolder.localPosition = Vector3.zero;

        SetBulletPool();
    }
    private void SetBulletPool()
    {
        for (int i = 0; i < _maxAmmo; i++)
        {
            CreateBullet(i);
        }
    }
    private void CreateBullet(int index)
    {
        GameObject newBullet = new GameObject($"{_ammoType.bulletType}_{index}", typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Bullet));
        newBullet.tag = _ammoType.bulletTag;
        newBullet.transform.parent = _bulletHolder;
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        newBullet.GetComponent<Rigidbody2D>().freezeRotation = true;
        newBullet.GetComponent<Bullet>().SetBullet(_ammoType);
        _bulletAvailable.Enqueue(newBullet.GetComponent<Bullet>());
        newBullet.SetActive(false);
    }
    public void Shoot(Vector3 targetPosition)
    {
        if (!_canShoot || _currentAmmo <= 0) { return; }
        _canShoot = false;
        Bullet nextBullet = _bulletAvailable.Dequeue();
        nextBullet.gameObject.SetActive(true);
        nextBullet.ShootBullet(transform.position, targetPosition);
        _bulletAvailable.Enqueue(nextBullet);
        if (_limitAmmo) { _currentAmmo--; }
        StartCoroutine(FireCooldown());
    }
    private IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }
    public void UpgradeAmmo(int upgrade)
    {
        _maxAmmo += upgrade;
        for (int i = 0; i < upgrade; i++)
        {
            CreateBullet(_maxAmmo - i);
        }
    }
    public void RefiilAmmo()
    {
        _currentAmmo = _maxAmmo;
    }
}
