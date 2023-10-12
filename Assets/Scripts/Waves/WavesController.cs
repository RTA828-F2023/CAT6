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

    public EnemySpawner enemySpawner;

    private GameObject[] enemies;

    private bool _timerOn;
    private float _timeLeft;

    private int _currentWave;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(initialWait);
        StartWave();
    }

    private void Update()
    {
        // enemies = GameObject.FindGameObjectsWithTag("Enemy"); // TODO: This function impacts performance in Update
        // enemyCountText.text = "Enemies: " + enemies.Length.ToString();

        // wavesCountText.text = "Waves: " + _currentWave;

        if (_timerOn && _currentWave < waveCount)
        {
            if (_timeLeft > 0)
            {
                // timerText.enabled = true;
                _timeLeft -= Time.deltaTime;

                // Consider spawning enemies here
            }
            else
            {
                // timerText.enabled = false;
                enemySpawner.Spawn(enemyCountPerWave);
                _currentWave++;
                _timeLeft = waveDuration;

                StartCoroutine(WaitBetweenWaves());
            }

            UpdateTimer(_timeLeft);
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // timerText.text = "Wave starts in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartWave()
    {
        _timerOn = true;
    }

    private void StopWave()
    {
        _timerOn = false;
    }

    private IEnumerator WaitBetweenWaves()
    {
        StopWave();
        yield return new WaitForSeconds(waveDelay);
        StartWave();
    }

    public bool ReachedMaxWaveCount()
    {
        return _currentWave >= waveCount;
    }
}