using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;
 
    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;
    public int enemyCount;
    public GameObject[] enemies;

    public WavesTimer WavesTimer;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
        //initial spawn of enemies
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
/*         if (enemies.Length < 5) {
            _timeUntilSpawn -= Time.deltaTime;
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

             if (_timeUntilSpawn <= 0) {
                Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        } */

        if (enemies.Length <= 0) {
            WavesTimer.TimerOn = true;
        }

    }

    public void spawnEnemies() {
        for (int i = 0; i < enemyCount; i++) {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void SetTimeUntilSpawn() {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}