using UnityEngine;

public class Enemy : Units
{
    [SerializeField] private EnemyController _enemyController;

    void Start()
    {
        if(_enemyController != null)
        {
            _enemyController.Initialize(this);
        }   
    }

    public void SetTarget(Transform newTarget)
    {
        _enemyController.SetTarget(newTarget);
    }
}
