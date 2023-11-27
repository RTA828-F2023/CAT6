using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private GameObject skipText;
    private bool _canSkip;

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
        skipText.SetActive(false);

        yield return new WaitForSecondsRealtime(30f);
        SceneLoader.Instance.Load("MainMenu");
    }

    #endregion

    private void SkipOnPerformed(InputAction.CallbackContext context)
    {
        if (_canSkip) SceneLoader.Instance.Load("MainMenu");
        else StartCoroutine(EnableSkip());
    }

    private IEnumerator EnableSkip()
    {
        _canSkip = true;
        skipText.SetActive(true);

        yield return new WaitForSeconds(3f);

        _canSkip = false;
        skipText.SetActive(false);
    }
}
