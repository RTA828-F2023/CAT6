using TMPro;
using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class WavesController : MonoBehaviour
{
    [Header("Wave Stats")]
    [SerializeField] private float initialWait; // Delay before starting the first wave
    [SerializeField] private float waveCount; // Maximum number of waves
    [SerializeField] private float waveDuration; // How long a wave lasts
    [SerializeField] private float waveDelay; // Duration between each wave
    [SerializeField] private int enemyCountPerWave; // How many enemies to spawn per wave

    [Header("UI References")]
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text wavesCountText;
    [SerializeField] private TMP_Text timerText;

    public EnemySpawner enemySpawner_topleft;
    public EnemySpawner enemySpawner_topright; 
    public EnemySpawner enemySpawner_bottomleft; 
    public EnemySpawner enemySpawner_bottomright; 
    private GameObject[] enemies;

    [SerializeField] private bool _timerOn;
    private float _timeLeft;

    [SerializeField] private int _currentWave;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(initialWait);
        StartWave();
    }

    private void Update()
    {
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // timerText.text = "Wave starts in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool GetTimer() {
        return _timerOn;
    }

    public float GetWaveDelay() {
        return waveDelay;
    }

    public void StartWave()
    {
        _timerOn = true;

        enemySpawner_topleft.Spawn(enemyCountPerWave);
        enemySpawner_topright.Spawn(enemyCountPerWave);
        enemySpawner_bottomleft.Spawn(enemyCountPerWave);
        enemySpawner_bottomright.Spawn(enemyCountPerWave);

        _currentWave++;
    }

    private void StopWave()
    {
        _timerOn = false;
    }

    public IEnumerator WaitBetweenWaves()
    {
        //StopWave();
        yield return new WaitForSeconds(waveDelay);
        StartWave();
    }

    public bool ReachedMaxWaveCount()
    {
        return _currentWave >= waveCount;
    }
}