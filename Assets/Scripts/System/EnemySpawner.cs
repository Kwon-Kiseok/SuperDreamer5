using UnityEngine;
using Zenject;
using System.Text;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _spawnTransform;
    // Spawn Interval Time은 웨이브 진행 시간 / 총 몬스터 수로 결정
    [SerializeField] private float _spawnIntervalTime = 1.5f;
    [SerializeField] private float _initialDelayTime = 5f;

    private bool _isSpawning = false;
    public bool IsSpawning => _isSpawning;

    private bool _isSpawnBoss = false;
    public bool IsSpawnBoss => _isSpawnBoss;

    private EnemyDatabase _enemyDatabase;
    private CancellationTokenSource _cts;

    [Inject]
    public void Inject(EnemyDatabase enemyDatabase)
    {
        _enemyDatabase = enemyDatabase;
    }

    void Start()
    {
        _enemyDatabase.Load();

        _cts = new CancellationTokenSource();
        StartSpawning(_cts.Token).Forget();
    }

    public void SetBossSpawn(bool newState)
    {
        _isSpawnBoss = newState;
    }

    public void SetSpawningState(bool newSpawningState)
    {
        _isSpawning = newSpawningState;
    }

    private async UniTask StartSpawning(CancellationToken token)
    {
        await UniTask.Delay((int)(_initialDelayTime * 1000), cancellationToken: token);
        SetSpawningState(true);

        while (!token.IsCancellationRequested)
        {
            if(IsSpawning)
            {
                await UniTask.Delay((int)(_spawnIntervalTime * 1000), cancellationToken: token);

                if(IsSpawnBoss)
                {
                    SpawnBoss();
                }
                else
                {
                    SpawnWaveEnemy();
                }
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    private void SpawnWaveEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        enemy.transform.SetParent(_spawnTransform);
        enemy.GetComponent<Enemy>().SetTarget(_targetPoint);
        enemy.GetComponent<Enemy>().SetInitData(_enemyDatabase.GetStatData(1001));
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(_bossPrefab, _spawnPoint.position, Quaternion.identity);
        boss.transform.SetParent(_spawnTransform);
        boss.GetComponent<Enemy>().SetTarget(_targetPoint);
    }

    void Oestroy()
    {
        _cts?.Cancel();
    }
}
