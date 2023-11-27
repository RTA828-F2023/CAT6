using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstructionsController : MonoBehaviour
{
    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();
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

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneLoader.Instance.Load("TestLevel2");
    }

    #endregion

    private void SkipOnPerformed(InputAction.CallbackContext context)
    {
        SceneLoader.Instance.Load("TestLevel2");
    }
}
