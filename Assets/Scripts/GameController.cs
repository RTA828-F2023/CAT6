using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Singleton

    private static GameController _gameControllerInstance;

    public static GameController Instance
    {
        get
        {
            if (_gameControllerInstance == null) _gameControllerInstance = FindObjectOfType<GameController>();
            return _gameControllerInstance;
        }
    }

    #endregion

    [SerializeField] private GameObject levelCompleteMenu;
    [SerializeField] private GameObject gameOverMenu;

    private MainCamera _mainCamera;

    private VolumeProfile _volumeProfile;
    private DepthOfField _depthOfField;

    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();

        // Handle game select input
        _inputManager.Game.Select.performed += SelectOnPerformed;
        _inputManager.Game.Exit.performed += ExitOnPerformed;

        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _mainCamera = Camera.main.GetComponent<MainCamera>();

        _volumeProfile = FindObjectOfType<Volume>().profile;
        _volumeProfile.TryGet(out _depthOfField);
    }

    private void Start()
    {
        levelCompleteMenu.SetActive(false);
        // gameOverMenu.SetActive(false);
    }

    #endregion

    #region Input Handlers

    private void SelectOnPerformed(InputAction.CallbackContext context)
    {
    }

    private void ExitOnPerformed(InputAction.CallbackContext context)
    {
    }

    #endregion

    private void GameOver()
    {
        _depthOfField.active = true;
        // gameOverMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    private void LevelCompleted()
    {
        _depthOfField.active = true;
        levelCompleteMenu.SetActive(true);

        Time.timeScale = 0f;
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

    #region Level Loading Methods

    // Load a new level
    private IEnumerator LoadLevelCoroutine(string levelName)
    {
        _mainCamera.Outro();
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelCoroutine(levelName));
    }

    #endregion

    public void SetDepthOfField(bool value)
    {
        _depthOfField.active = value;
    }

    // Load a map layout for the level
    private void LoadMap()
    {

    }
}
