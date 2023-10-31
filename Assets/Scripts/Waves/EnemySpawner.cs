using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;

    private float _timeUntilSpawn;

    private void Start()
    {
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        /*         if (enemies.Length < 5) {
                    _timeUntilSpawn -= Time.deltaTime;
                    Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                     if (_timeUntilSpawn <= 0) {
                        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                        SetTimeUntilSpawn();
                    }
                } */
    }

    public void Spawn(int enemyCount) // TODO: Spawn position other than transform.position
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position, Quaternion.identity).transform.SetParent(transform);
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}