using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTable _enemyTable;

    [SerializeField] private float _minSpawnInterval = 0.5f;
    [SerializeField] private float _maxSpawnInterval = 2f;
    private float _spawnInterval = 1f;

    private float _timer = 0f;

    private void Update()
    {
        TrySpawnEnemy();
    }

    private void TrySpawnEnemy()
    {
        _timer += Time.deltaTime;

        if(_timer >= _spawnInterval)
        {
            SpawnEnemy();
            SetRandomRespawnTime();
            _timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        EnemyTable.EnemyData enemyData = GetRandomEnemy();
        Instantiate(enemyData.EnemyPrefab, transform.position, Quaternion.identity);
    }

    private EnemyTable.EnemyData GetRandomEnemy()
    {
        if (_enemyTable == null) return default;
        if (_enemyTable.enemys.Length == 0) return default;

        float randomValue = Random.value;
        float cumulative = 0f;

        foreach (var enemy in _enemyTable.enemys)
        {
            cumulative += enemy.SpawnChance;

            if(randomValue <= cumulative)
            {
                return enemy;
            }
        }
        return _enemyTable.enemys[^1];
    }

    private void SetRandomRespawnTime()
    {
        _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }
}
