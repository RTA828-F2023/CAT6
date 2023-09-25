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
        _depthOfField.active = false;
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
        // TODO: Show game over screen
    }

    private void LevelCompleted()
    {
        _depthOfField.active = true;
        // TODO: Show level complete screen
    }

    public IEnumerator CheckPlayerCount()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Player>().Length == 0) GameOver();
    }

    public IEnumerator CheckEnemyCount()
    {
        // Have to wait til next frame so that game objects have been fully destroyed
        yield return new WaitForEndOfFrame();
        if (FindObjectsOfType<Enemy>().Length == 0) LevelCompleted();
    }

    // Load a new level
    private IEnumerator LoadLevel(string levelName)
    {
        _mainCamera.Outro();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    // Load a map layout for the level
    private void LoadMap()
    {

    }
}
