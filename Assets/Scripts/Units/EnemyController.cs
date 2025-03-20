using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Move,
        Attack,
        Die
    }

    private EnemyState _currentState;
    private Enemy _enemyData;

    private Transform _targetTransform;
    private CancellationTokenSource _cts;

    private int _currentHp;

    [SerializeField] private float _attackInterval = 2f;
    [SerializeField] private float _attackRange = 0.25f;
    [SerializeField] private float _deathDelay = 1f;

    private bool _isAttacking = false;

    public void Initialize(Enemy enemy)
    {
        _enemyData = enemy;
        _currentHp = enemy.StatData.maxHp;
        _enemyData.unitRenderer.HPBarUI.UpdateHealth(_currentHp, enemy.StatData.maxHp);

        _cts = new CancellationTokenSource();
        ChangeState(EnemyState.Move);
    }

    void ChangeState(EnemyState newState)
    {
        _currentState = newState;

        switch(_currentState)
        {
            case EnemyState.Move:
                MoveToTarget().Forget();
                break;
            case EnemyState.Attack:
                DoAttack().Forget();
                break;
            case EnemyState.Die:
                Die().Forget();
                break;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _targetTransform = newTarget;
    }

    private async UniTask MoveToTarget()
    {
        while(_currentState == EnemyState.Move)
        {
            if(_targetTransform == null)
            {
                return;
            }

            Vector3 direction = (_targetTransform.position - transform.position).normalized;
            transform.position += direction * _enemyData.StatData.speed * Time.deltaTime;

            if(Vector3.Distance(transform.position, _targetTransform.position) <= _attackRange)
            {
                ChangeState(EnemyState.Attack);
                return;
            }

            await UniTask.Yield();
        }
    }

    private async UniTask DoAttack()
    {
        _isAttacking = true;

        while(_currentState == EnemyState.Attack)
        {
            // attack logic

            await UniTask.Delay((int)(_attackInterval * 1000), cancellationToken: _cts.Token);
        }
    }

    public void TakeDamage(int damage)
    {
        if(_currentState == EnemyState.Die)
        {
            return;
        }

        int finalDamage = Mathf.Max(0, damage - _enemyData.StatData.def);
        _currentHp -= finalDamage;
        _enemyData.unitRenderer.HPBarUI.UpdateHealth(_currentHp, _enemyData.StatData.maxHp);

        if (_currentHp <= 0)
        {
            ChangeState(EnemyState.Die);
        }
    }

    private async UniTask Die()
    {
        _isAttacking = false;
        _enemyData.SetAlive(false);

        CurrenyManager.Instance?.AddGold(_enemyData.StatData.huntGold);

        await UniTask.Delay((int)(_deathDelay * 1000), cancellationToken: _cts.Token);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spell spell = collision.GetComponent<Spell>();
        if(spell != null)
        {
            TakeDamage(spell.StatData.damage);
        }
    }

    void Oestroy()
    {
        _cts?.Cancel();        
    }

}
