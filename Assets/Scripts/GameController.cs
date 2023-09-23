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

    private MainCamera _mainCamera;

    private VolumeProfile _volumeProfile;
    private DepthOfField _depthOfField;

    private bool _gameInProgress = true;

    private Player[] _players;

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

        _players = FindObjectsOfType<Player>();
    }

    private void Start()
    {
        _depthOfField.active = false;
    }

    #endregion

    #region Unity Events

    private void SelectOnPerformed(InputAction.CallbackContext context)
    {
        if (!_gameInProgress) StartCoroutine(LoadLevel());
    }

    private void ExitOnPerformed(InputAction.CallbackContext context)
    {
        if (!_gameInProgress) Application.Quit();
    }

    #endregion

    private void GameOver()
    {
        _gameInProgress = false;

        // TODO: Show game over screen
        _depthOfField.active = true;
    }

    private void RoundOver()
    {
        // TODO: Load the next round
    }

    private IEnumerator CheckPlayerCount()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Player>().Length == 0) GameOver();
    }

    private IEnumerator CheckEnemyCount()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        // if (FindObjectsOfType<Enemy>().Length == 0) RoundOver();
    }

    // Load a new level
    private IEnumerator LoadLevel()
    {
        _mainCamera.Outro();
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
    }

    // Load a map layout for the level
    private void LoadMap()
    {

    }
}
