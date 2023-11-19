using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerType type;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    public float walkForce;
    public int _currentHealth;

    [Header("References")]
    [SerializeField] private Transform healthDisplay;

    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private Transform scoreBoard;
    private Image[] _heartIcons;

    [SerializeField] private Weapon[] weaponPrefabs;

    [SerializeField] private AudioSource walkAudio;

    private bool _isWalking;
    private Vector2 _currentDirection = Vector2.up;

    private static readonly int WalkFrontAnimationBool = Animator.StringToHash("isWalkingFront");
    private static readonly int WalkBackAnimationBool = Animator.StringToHash("isWalkingBack");
    private static readonly int WalkSideAnimationBool = Animator.StringToHash("isWalkingSide");

    private Weapon _weapon;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();

        // Handle player input based on player type
        switch (type)
        {
            case PlayerType.One:
                _inputManager.Player1.Joystick.performed += WalkOnPerformed;
                _inputManager.Player1.Joystick.canceled += WalkOnCanceled;
                _inputManager.Player1.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Two:
                _inputManager.Player2.Joystick.performed += WalkOnPerformed;
                _inputManager.Player2.Joystick.canceled += WalkOnCanceled;
                _inputManager.Player2.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Three:
                _inputManager.Player3.Joystick.performed += WalkOnPerformed;
                _inputManager.Player3.Joystick.canceled += WalkOnCanceled;
                _inputManager.Player3.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Four:
                _inputManager.Player4.Joystick.performed += WalkOnPerformed;
                _inputManager.Player4.Joystick.canceled += WalkOnCanceled;
                _inputManager.Player4.Btn1.performed += FireOnPerformed;
                break;

            default:
                break;
        }

        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _heartIcons = healthDisplay.GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        ShuffleWeapon();
    }

    private void Update()
    {
        _weapon.transform.up = _currentDirection;
    }

    private void FixedUpdate()
    {
        if (_isWalking) _rigidbody.AddForce(_currentDirection * walkForce, ForceMode2D.Force);
    }

    #endregion

    #region Input Handlers

    private void WalkOnPerformed(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            Walk(context.ReadValue<Vector2>().normalized);
        }
    }

    private void WalkOnCanceled(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            Stop();
        }
    }

    private void FireOnPerformed(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            Fire();
        }
    }

    #endregion

    private void SetFlip(bool isFlipped)
    {
        transform.localScale = new Vector2(isFlipped ? -1f : 1f, 1f);
    }

    #region Movement Methods

    private void Walk(Vector2 direction)
    {
        // Update walk state & direction
        _currentDirection = direction;
        _isWalking = true;

        SetFlip(_currentDirection.x < 0f);

        // Play walk animation
        _animator.SetBool(WalkFrontAnimationBool, false);
        _animator.SetBool(WalkBackAnimationBool, false);
        _animator.SetBool(WalkSideAnimationBool, false);
        if (direction.y > 0f) _animator.SetBool(WalkBackAnimationBool, true);
        else if (direction.y < 0f) _animator.SetBool(WalkFrontAnimationBool, true);
        else _animator.SetBool(WalkSideAnimationBool, true);

        // Play walk audio
        walkAudio.Play();
    }

    private void Stop()
    {
        // Update walk state & player velocity
        _isWalking = false;

        // Stop walk animation
        _animator.SetBool(WalkFrontAnimationBool, false);
        _animator.SetBool(WalkBackAnimationBool, false);
        _animator.SetBool(WalkSideAnimationBool, false);

        // Stop walk audio
        walkAudio.Stop();
    }

    #endregion

    #region Fire Methods

    private void Fire()
    {
        _weapon?.Fire();
    }

    #endregion

    public void KnockBack(Vector2 direction, float force)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void UpdateHealthDisplay()
    {
        // Update the visibility of the heart icons on the UI
        for (int i = 0; i < _currentHealth; i++) _heartIcons[i + 1].gameObject.SetActive(true);
        for (int i = _currentHealth; i < maxHealth; i++) _heartIcons[i + 1].gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth--;
        UpdateHealthDisplay();

        CameraShaker.Instance.Shake(CameraShakeMode.Normal);
        if (_currentHealth <= 0) Die();
    }

    public void UpdateScore(int score)
    {
        var scoreController = scoreBoard.GetComponent<PointSystemController>();
        scoreController.UpdatePlayerScore(type, score);

    }

    private void Die()
    {
        CameraShaker.Instance.Shake(CameraShakeMode.Normal);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Check if all players are dead -> game over
        GameController.Instance.StartCoroutine(GameController.Instance.CheckLoseCondition());
        Destroy(gameObject);
    }

    public void ShuffleWeapon()
    {
        if (_weapon) Destroy(_weapon.gameObject);

        _weapon = Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Length)], transform.position, Quaternion.identity);
        _weapon.transform.SetParent(transform);
    }
}
