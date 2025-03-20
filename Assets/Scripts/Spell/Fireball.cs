using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _explodeSplashRadius = 2f;
    public LayerMask enemyLayer;

    private Vector3 _direction;
    private int _damage;
    private bool _hasHit = false;

    public void Initialize(Vector3 direction, int damage)
    {
        _direction = direction.normalized;
        _damage = damage;

        transform.DOMove(transform.position + (direction * _maxDistance), _maxDistance / _moveSpeed)
        .SetEase(Ease.Linear)
        .OnComplete(() => Explode());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _explodeSplashRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyController>()?.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
