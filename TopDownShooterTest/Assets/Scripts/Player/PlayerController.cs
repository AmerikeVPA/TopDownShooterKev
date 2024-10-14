using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(EconomySystem))]
public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;

    private Vector3 _mouseCoordinates;
    private Camera _mainCamera;
    private Transform _spriteTransform;
    private Rigidbody2D _rigidbody2D;
    private HealthSystem _healthSystem;
    private GunSystem _gunSystem;
    private EconomySystem _economySystem;

    #region InputSystem
    private Vector2 _movementInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    public void OnBasicShot(InputAction.CallbackContext context)
    {
        if (context.action.triggered) { Shoot(true); }
    }
    public void OnSpecialShot(InputAction.CallbackContext context)
    {
        if (context.action.triggered) { Shoot(false); }
    }
    #endregion
    private void Awake()
    {
        _mainCamera = GetComponentInChildren<Camera>();
        _spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _healthSystem = GetComponent<HealthSystem>();
        _gunSystem = GetComponent<GunSystem>();
        _economySystem = GetComponent<EconomySystem>();

        _gunSystem.SetGuns();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            _healthSystem.TakeDamage(collision.transform.GetComponent<Bullet>()._bulletDamage);
            collision.transform.GetComponent<Bullet>().TurnBulletOff();
        }
        if (collision.CompareTag("Coin"))
        {
            _economySystem.AddCoin();
            collision.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        _rigidbody2D.velocity = new Vector2(_movementInput.x, _movementInput.y) * speed;
    }
    private void Rotate()
    {
        _mouseCoordinates = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerToMouseDirection = (_mouseCoordinates - transform.position).normalized;

        _spriteTransform.up = playerToMouseDirection;
    }
    private void Shoot(bool useBasicAmmo)
    {
        _gunSystem._mouseCoordinates = _mouseCoordinates;
        if (useBasicAmmo) 
        {
            _gunSystem.ShootBasic();
            return;
        }
        _gunSystem.ShootSpecial();
    }
}
