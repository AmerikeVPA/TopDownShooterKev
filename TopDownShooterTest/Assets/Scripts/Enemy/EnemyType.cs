using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/EnemyType")]
public class EnemyType : ScriptableObject
{
    public float maxHealth;
    public float speed;

    public float detectionRange;
    public float followRange;

    public float damage;
    public AmmoType bulletType;
}
