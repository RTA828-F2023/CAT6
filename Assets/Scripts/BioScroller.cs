using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BioScroller : MonoBehaviour
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

        _inputManager.Player1.Btn1.performed += SkipOnPerformed;
        _inputManager.Player2.Btn1.performed += SkipOnPerformed;
        _inputManager.Player3.Btn1.performed += SkipOnPerformed;
        _inputManager.Player4.Btn1.performed += SkipOnPerformed;
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
            _scrollRect.horizontalScrollbar.value = Mathf.Clamp(_scrollRect.horizontalScrollbar.value + _scrollValue, -0.2f, 1.2f);
        }
    }

    #endregion

    #region Input Handlers

    private void ScrollOnPerformed(InputAction.CallbackContext context)
    {
        _scrollValue = context.ReadValue<Vector2>().x * Time.deltaTime;
    }

    private void ScrollOnCanceled(InputAction.CallbackContext context)
    {
        _scrollValue = 0f;
    }

    private void SkipOnPerformed(InputAction.CallbackContext context)
    {
        SceneLoader.Instance.Load("MainMenu");
    }


    #endregion
}
