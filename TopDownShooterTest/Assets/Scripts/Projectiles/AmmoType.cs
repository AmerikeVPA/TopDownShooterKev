using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Data/AmmoTypes")]
public class AmmoType : ScriptableObject
{
    [Header("Ammo attributes")]

    [Space(5), Header("Visuals and differentiation")]
    [Tooltip("The name for this type of bullet, it will be used to name the corresponding GameObjects according to this string.")]
    public string bulletType;
    public string bulletTag;
    [Tooltip("The sprite that is going to be used for this type of bullet.")]
    public Sprite bulletSprite;

    [Space(5), Header("Movement related attributes")]
    public bool stopAtTarget;
    public bool infiniteAmmo;
    public int ammoPoolSize;
    [Tooltip("The time this type of bullet can be active in scene after being shot.")]
    public float bulletLifetime;
    [Tooltip("The speed at which this type of bullet will move through the scene.")]
    public float bulletSpeed;
    public float bulletDamage;
    public float fireRate;

    public float explosionRadious;
    public GameObject explosionPrefab;
}
