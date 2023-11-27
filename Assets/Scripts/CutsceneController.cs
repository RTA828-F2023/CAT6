using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private GameObject skipText;
    [SerializeField] private GameObject cutSceneTexture;
    [SerializeField] public VideoPlayer cutScenePlayer;
    [SerializeField] public VideoClip[] cutScenes;

    private List<int> _playerChoices = new List<int>() { 1, 2, 3, 4, 5 };
    private bool _canSkip;

    private InputManager _inputManager;
    private int _kidnapped;

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
        getCutScene();
        skipText.SetActive(false);
        cutScenePlayer.Play();
        cutSceneTexture.SetActive(true);


        yield return new WaitForSecondsRealtime(70f);
        cutSceneTexture.SetActive(false);
        SceneLoader.Instance.Load("InstructionScreen");
    }

    #endregion

    private void SkipOnPerformed(InputAction.CallbackContext context)
    {
        if (_canSkip) SceneLoader.Instance.Load("InstructionScreen");
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

    private void getCutScene()
    {

        _playerChoices.Remove(PlayerPrefs.GetInt("p1"));
        _playerChoices.Remove(PlayerPrefs.GetInt("p2"));
        _playerChoices.Remove(PlayerPrefs.GetInt("p3"));
        _playerChoices.Remove(PlayerPrefs.GetInt("p4"));

        int kidnap = Random.Range(0, _playerChoices.Count);
        _kidnapped = _playerChoices[kidnap];

        //1 -> macho, 2 -> lello, 3 -> eepy,4 -> ruki 5, -> billi
        cutScenePlayer.clip = cutScenes[_kidnapped - 1];
        PlayerPrefs.SetInt("KidnappedTakoyu", _kidnapped);
    }
}
