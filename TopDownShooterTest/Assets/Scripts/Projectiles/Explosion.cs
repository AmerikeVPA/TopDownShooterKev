using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Explosion : MonoBehaviour
{
    public float damage;
    private float _radious;
    private CircleCollider2D _circleCollider;

    private void OnEnable()
    {
        StartCoroutine(TurnExplosionOff());
    }
    public void RecieveData(float blastradious, float damage)
    {
        _radious = blastradious;
        this.damage = damage;
        _circleCollider = GetComponent<CircleCollider2D>();

        _circleCollider.radius = _radious;
    }
    private IEnumerator TurnExplosionOff()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
