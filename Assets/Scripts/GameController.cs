using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    #region Singleton

    private static GameController _gameControllerInstance;

    public GameObject weaponSelectMenu;

    public static GameController Instance
    {
        get
        {
            if (_gameControllerInstance == null) _gameControllerInstance = FindObjectOfType<GameController>();
            return _gameControllerInstance;
        }
    }

    #endregion

    public GameState State { get; set; } = GameState.InProgress;

    [SerializeField] private GameObject levelCompleteMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject inGameInterface;
    [SerializeField] private GameObject matchScores;


    private VolumeProfile _volumeProfile;
    private DepthOfField _depthOfField;

    private bool isDoneViewingScore = false;
    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();

        // Handle game select input
        _inputManager.Game.Select.performed += SelectOnPerformed;
        _inputManager.Game.Exit.performed += ExitOnPerformed;
        _inputManager.Game.Any.performed += AnyOnPerformed;

        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _volumeProfile = FindObjectOfType<Volume>().profile;
        _volumeProfile.TryGet(out _depthOfField);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        levelCompleteMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    #endregion

    #region Input Handlers

    private void SelectOnPerformed(InputAction.CallbackContext context)
    {
    }

    private void ExitOnPerformed(InputAction.CallbackContext context)
    {
    }

    private void AnyOnPerformed(InputAction.CallbackContext context)
    {
        // Placeholder code
        // TODO: Removed after implementing the level complete and game over menu
        if (State == GameState.Completed || State == GameState.GameOver)
        {
            if (!isDoneViewingScore) isDoneViewingScore = true;
            else
                SceneLoader.Instance.Load("MainMenu");
        }
    }

    #endregion

    private void GameOver()
    {
        SetDepthOfField(true);
        matchScores.SetActive(true);
        GetComponent<ScoreBoard>().DrawScores();
        State = GameState.GameOver;
        Time.timeScale = 0f;

        if (isDoneViewingScore == true)
        {
            matchScores.SetActive(false);
            gameOverMenu.SetActive(true);
            inGameInterface.SetActive(false);

        }
    }

    private void LevelCompleted()
    {
        SetDepthOfField(true);
        matchScores.SetActive(true);
        GetComponent<ScoreBoard>().DrawScores();
        State = GameState.Completed;
        Time.timeScale = 0f;

        if (isDoneViewingScore == true)
        {
            matchScores.SetActive(false);
            levelCompleteMenu.SetActive(true);
            inGameInterface.SetActive(false);

        }
        //TODO Return the highest score here? Maybe when there is a score screen?
    }

    public IEnumerator CheckWaveEnd()
    {
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Enemy>().Length == 0 && FindObjectOfType<WavesController>().GetTimer())
        {
            yield return new WaitForSeconds(FindObjectOfType<WavesController>().GetWaveDelay());
            FindObjectOfType<WavesController>().StartWave();
        }
    }

    public IEnumerator CheckLoseCondition()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Player>().Length == 0)
        {
            yield return new WaitForSeconds(0.5f);
            GameOver();
        }
    }

    public IEnumerator CheckWinCondition()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Enemy>().Length == 0 && FindObjectOfType<WavesController>().ReachedMaxWaveCount())
        {
            yield return new WaitForSeconds(0.5f);
            LevelCompleted();
        }
    }

    public void SetDepthOfField(bool value)
    {
        _depthOfField.active = value;
    }
}
