using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerType type;

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] private float walkForce;
    [SerializeField] private float fireForce;
    [SerializeField] private float fireRecoveryTime;

    private bool _canFire = true;
    private int _currentHealth;

    [Header("References")]
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform healthDisplay;
    [SerializeField] private Shuriken shurikenPrefab;
    [SerializeField] private ParticleSystem explosionPrefab;

    private Image[] _heartIcons;

    private bool _isWalking;
    private Vector2 _currentDirection = Vector2.up;

    private static readonly int WalkAnimationBool = Animator.StringToHash("isWalking");

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
            case PlayerType.Blue:
                _inputManager.PlayerBlue.Joystick.performed += WalkOnPerformed;
                _inputManager.PlayerBlue.Joystick.canceled += WalkOnCanceled;
                _inputManager.PlayerBlue.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Pink:
                _inputManager.PlayerPink.Joystick.performed += WalkOnPerformed;
                _inputManager.PlayerPink.Joystick.canceled += WalkOnCanceled;
                _inputManager.PlayerPink.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Yellow:
                _inputManager.PlayerYellow.Joystick.performed += WalkOnPerformed;
                _inputManager.PlayerYellow.Joystick.canceled += WalkOnCanceled;
                _inputManager.PlayerYellow.Btn1.performed += FireOnPerformed;
                break;

            case PlayerType.Green:
                _inputManager.PlayerGreen.Joystick.performed += WalkOnPerformed;
                _inputManager.PlayerGreen.Joystick.canceled += WalkOnCanceled;
                _inputManager.PlayerGreen.Btn1.performed += FireOnPerformed;
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
    }

    private void Update()
    {
        arrow.up = _currentDirection;
    }

    private void FixedUpdate()
    {
        // if (_isWalking) _rigidbody.velocity = _walkDirection * speed;
        if (_isWalking) _rigidbody.AddForce(_currentDirection * walkForce, ForceMode2D.Force);
    }

    #endregion

    #region Input Handlers

    private void WalkOnPerformed(InputAction.CallbackContext context)
    {
        Walk(context.ReadValue<Vector2>().normalized);
    }

    private void WalkOnCanceled(InputAction.CallbackContext context)
    {
        Stop();
    }

    private void FireOnPerformed(InputAction.CallbackContext context)
    {
        Fire();
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

        // Play walk animation
        _animator.SetBool(WalkAnimationBool, true);
        SetFlip(_currentDirection.x < 0f);
    }

    private void Stop()
    {
        // Update walk state & player velocity
        _isWalking = false;
        // _rigidbody.velocity = Vector2.zero;

        // Stop walk animation
        _animator.SetBool(WalkAnimationBool, false);
    }

    #endregion

    #region Fire Methods

    private void Fire()
    {
        if (!_canFire) return;

        var shuriken = Instantiate(shurikenPrefab, firePoint.position, Quaternion.identity);
        shuriken.Init(_currentDirection, fireForce);

        // Down time before player can fire again
        _canFire = false;
        StartCoroutine(RecoverFire(fireRecoveryTime));
    }

    private IEnumerator RecoverFire(float time)
    {
        yield return new WaitForSeconds(time);
        _canFire = true;
    }

    #endregion

    public void KnockBack(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void UpdateHealthDisplay()
    {
        // Update the visibility of the heart icons on the UI
        for (int i = 0; i < _currentHealth; i++) _heartIcons[i + 1].gameObject.SetActive(true);
        for (int i = _currentHealth; i < maxHealth; i++) _heartIcons[i + 1].gameObject.SetActive(false);
    }

    private void TakeDamage(int damage)
    {
        _currentHealth--;
        UpdateHealthDisplay();

        CameraShaker.Instance.Shake(CameraShakeMode.Light);
    }

    private void Die()
    {
        Destroy(gameObject);
        CameraShaker.Instance.Shake(CameraShakeMode.Normal);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // TODO: Check if all players are dead -> game over
        GameController.Instance.StartCoroutine(GameController.Instance.CheckPlayerCount());
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //If collides with an Enemy
        if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("EnemyProjectile"))
        {
            _currentHealth--;
            UpdateHealthDisplay();

            if (_currentHealth > 0) Reset();
            else Die();
        }
    }

}
