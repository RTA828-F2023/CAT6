using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsController : MonoBehaviour
{
    private InputManager _inputManager;

    #region Unity Events

    private void OnEnable()
    {
        _inputManager = new InputManager();
        _inputManager.Game.Skip.performed += SkipOnPerformed;
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(30f);
        SceneLoader.Instance.Load("MainMenu");
    }

    #endregion

    private void SkipOnPerformed(InputAction.CallbackContext context)
    {
        SceneLoader.Instance.Load("MainMenu");
    }
}
