using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [SerializeField] private bool isSpawning;
    [SerializeField] private int currentEnemyCount;
    [SerializeField] private int maxEnemyCount;

    [SerializeField] private float _timeUntilSpawn;

    private void Start()
    {
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (isSpawning) {
            if (currentEnemyCount < maxEnemyCount) {
                if (_timeUntilSpawn <= 0) {
                    Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position, Quaternion.identity).transform.SetParent(transform);
                    currentEnemyCount++;
                    SetTimeUntilSpawn();
                }
            }
            else {
                isSpawning = false;
            }
        }
    }

    public void Spawn(int enemyCount) // TODO: Spawn position other than transform.position
    {
        isSpawning = true;
        currentEnemyCount = 0;
        maxEnemyCount = enemyCount;
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}