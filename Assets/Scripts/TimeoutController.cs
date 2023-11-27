using UnityEngine;
using UnityEngine.InputSystem;

public class TimeoutController : MonoBehaviour
{
    private const float TimeoutDuration = 30f;
    private float _timer;

    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();
        _inputManager.Game.Any.performed += AnyOnPerformed;
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer >= TimeoutDuration) 
        {
            _timer = 0f;
            SceneLoader.Instance.Load("MainMenu");
        }
    }

    #endregion

    #region Input Handlers

    private void AnyOnPerformed(InputAction.CallbackContext context)
    {
        _timer = 0f;
    }

    #endregion
}
