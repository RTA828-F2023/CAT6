using TMPro;
using UnityEngine;
using UnityEngine.Video;

using System.Collections;
using System.Collections.Specialized;

public class WavesController : MonoBehaviour
{
    [Header("Wave Stats")]
    [SerializeField] private float initialWait; // Delay before starting the first wave
    [SerializeField] private float waveCount; // Maximum number of waves
    [SerializeField] private float waveDuration; // How long a wave lasts
    [SerializeField] private float waveDelay; // Duration between each wave
    [SerializeField] private int enemiesPerSpawner; // How many enemies spawn per spawner

    [Header("UI References")]
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text wavesCountText;
    [SerializeField] private TMP_Text timerText;

    [Header("UI Wave Banner")]
    [SerializeField] private VideoClip[] bannerVideos;
    [SerializeField] private VideoPlayer bannerVideoPlayer;
    [SerializeField] private GameObject bannerVideoTexture;

    [SerializeField] private GameObject bannerObject;
    [SerializeField] private TextMeshProUGUI bannerText;
    [SerializeField] private float timeToMiddle;
    [SerializeField] private float timeToEnd;
    private AudioSource startSoundEffect;
    private Vector2 startPosition;
    private RectTransform bannerRectTransform;


    [Header("Flashing Lights")]
    [SerializeField] public FlashingLight[] flashingLights;

    [Header("Spawners")]
    public EnemySpawner enemySpawner_topleft;
    public EnemySpawner enemySpawner_topright; 
    public EnemySpawner enemySpawner_bottomleft; 
    public EnemySpawner enemySpawner_bottomright; 
    [Header("References")]
    [SerializeField] private PointSystemController pointSystem;

    public EnemySpawner enemySpawner;

    private GameObject[] enemies;

    [SerializeField] private bool _timerOn;
    private float _timeLeft;

    [SerializeField] private int _currentWave;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(initialWait);

        //Find all flashing light objects at initial start
        flashingLights = FindObjectsOfType<FlashingLight>();

        
        if (bannerObject != null ) 
        {
            bannerRectTransform = bannerObject.GetComponent<RectTransform>();
            startSoundEffect = bannerObject.GetComponent<AudioSource>();
            startPosition = bannerRectTransform.anchoredPosition;
        }
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
        
        _currentWave++;

        if (_currentWave >= 2)
        {
            pointSystem.DisplayBestPlayer();
        }
        DisplayWave();
        StartCoroutine(SpawnEnemies());

        foreach (var player in FindObjectsOfType<Player>())
        {
            player.ShuffleWeapon();
        }
    }  

    private void StopWave()
    {
        _timerOn = false;

        //TODO Only updating when the next round starts, not when all enemies defeated keep track of enemies some other way other than Find?
    }

    public IEnumerator WaitBetweenWaves()
    {
        //StopWave();
        yield return new WaitForSeconds(waveDelay);
        StartWave();
    }

    private IEnumerator SpawnEnemies() 
    {
        yield return new WaitForSeconds(3f);
        bannerVideoTexture.SetActive(false);
        enemySpawner_topleft.Spawn(enemiesPerSpawner + _currentWave);
        enemySpawner_topright.Spawn(enemiesPerSpawner + _currentWave);
        enemySpawner_bottomleft.Spawn(enemiesPerSpawner + _currentWave);
        enemySpawner_bottomright.Spawn(enemiesPerSpawner + _currentWave);
    }

    public bool ReachedMaxWaveCount()
    {
        return _currentWave >= waveCount;
    }

    public void DisplayWave() 
    {
        bannerVideoTexture.SetActive(true);
        bannerVideoPlayer.clip = bannerVideos[_currentWave - 1];
        bannerVideoPlayer.Play();
        //bannerText.text = "WAVE " + _currentWave + "!!!";
        //StartCoroutine(MoveBanner());
    }

    private IEnumerator MoveBanner()
    {
        //toggle flashing lights on
        foreach (FlashingLight light in flashingLights) {
            light.toggleFlash();
        }

        Vector2 Start = bannerRectTransform.anchoredPosition;
        Vector2 Middle = new Vector2(0, 0);
        Vector2 End = new Vector2(1400, 0);

        float timeElapsedMiddle = 0;
        float timeElapsedEnd = 0;

        startSoundEffect.Play();
        //Move to middle
        while (timeElapsedMiddle < timeToMiddle)
        {
            Vector2 newPosition = Vector2.Lerp(Start, Middle, (timeElapsedMiddle / timeToMiddle));
            bannerRectTransform.anchoredPosition = newPosition;
            timeElapsedMiddle += Time.deltaTime;
            yield return new WaitForSeconds(0.0f);
        }
        //Wait in middle
        bannerRectTransform.anchoredPosition = Middle;
        yield return new WaitForSeconds(1.0f);

        //Move off screen
        while (timeElapsedEnd < timeToEnd)
        {
            Vector2 newPosition = Vector2.Lerp(Middle, End, (timeElapsedEnd / timeToEnd));
            bannerRectTransform.anchoredPosition = newPosition;
            timeElapsedEnd += Time.deltaTime;
            yield return new WaitForSeconds(0.0f);
        }
        bannerRectTransform.anchoredPosition = startPosition;

        //toggle flashing lights off
        foreach (FlashingLight light in flashingLights) {
            light.toggleFlash();
        }
    }
}