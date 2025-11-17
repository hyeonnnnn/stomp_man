using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

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
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    private void SetRandomRespawnTime()
    {
        _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }
}
