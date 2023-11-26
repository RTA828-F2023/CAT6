using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LBScroller : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private float _scrollValue;

    private InputManager _inputManager;

    #region Unity Events

     private void OnEnable()
    {
        _inputManager = new InputManager();

        // Handle scrolling input
        _inputManager.Player1.Joystick.performed += ScrollOnPerformed;
        _inputManager.Player2.Joystick.performed += ScrollOnPerformed;
        _inputManager.Player3.Joystick.performed += ScrollOnPerformed;
        _inputManager.Player4.Joystick.performed += ScrollOnPerformed;

        _inputManager.Player1.Joystick.canceled += ScrollOnCanceled;
        _inputManager.Player2.Joystick.canceled += ScrollOnCanceled;
        _inputManager.Player3.Joystick.canceled += ScrollOnCanceled;
        _inputManager.Player4.Joystick.canceled += ScrollOnCanceled;

        _inputManager.Game.Select.performed += SelectOnPerformed;

        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (_scrollValue != 0f)
        {
            _scrollRect.verticalScrollbar.value = Mathf.Clamp(_scrollRect.verticalScrollbar.value + _scrollValue, -0.2f, 1.2f);
        }
    }

    #endregion

    #region Input Handlers

    private void ScrollOnPerformed(InputAction.CallbackContext context)
    {
        _scrollValue = context.ReadValue<Vector2>().y * Time.deltaTime;
    }

    private void ScrollOnCanceled(InputAction.CallbackContext context)
    {
        _scrollValue = 0f;
    }

    private void SelectOnPerformed(InputAction.CallbackContext context)
    {
        SceneLoader.Instance.Load("MainMenu");
    }


    #endregion
}