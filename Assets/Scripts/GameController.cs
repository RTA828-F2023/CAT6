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

    [SerializeField] private Transform winnerDisplay;

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
        if (!_gameInProgress || SceneManager.GetActiveScene().name.Equals("Home")) StartCoroutine(LoadLevel());
    }

    private void ExitOnPerformed(InputAction.CallbackContext context)
    {
        if (!_gameInProgress || SceneManager.GetActiveScene().name.Equals("Home")) Application.Quit();
    }

    #endregion

    public IEnumerator CheckWinCondition()
    {
        // Has to wait til the next frame so that the player is fully destroyed
        yield return new WaitForEndOfFrame();

        // Check for last-man-standing -> winner
        var playerAliveCount = 0;
        var winner = _players[0];
        foreach (var player in _players)
        {
            if (!player.isDead)
            {
                playerAliveCount++;
                winner = player;
            }
        }
        // If more than 1 player is alive then win condition is not met -> return
        if (playerAliveCount > 1) yield break;

        // Update game state
        _gameInProgress = false;
        winner.isControllable = false;

        // Wait a bit then update the UI
        yield return new WaitForSeconds(0.5f);
        winnerDisplay.GetComponentInChildren<TMP_Text>().SetText($"Player {winner.type} wins!");
        winnerDisplay.gameObject.SetActive(true);
        _depthOfField.active = true;

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
