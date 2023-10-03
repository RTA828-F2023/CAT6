using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIControlller : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeBtn;
    public GameObject menuBtn;
    private InputManager _inputManager;

    public GameObject firstPauseButton; 

    public TextMeshProUGUI ptext;
    
    public static bool isPaused = false;

    //when game start by default set pausemenu on not active
    void Start()
    {
        pauseMenu.SetActive(false);
        //ptext = FindObjectOfType<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        //when player presses pause
        _inputManager.PlayerBlue.Select.performed += P1PauseOnPerformed;
        _inputManager.PlayerPink.Select.performed += P2PauseOnPerformed;
        _inputManager.PlayerYellow.Select.performed += P3PauseOnPerformed;
        _inputManager.PlayerGreen.Select.performed += P4PauseOnPerformed; 

        //when player scrolls down
        _inputManager.PlayerYellow.Joystick.performed += P3Navigation;
        _inputManager.PlayerGreen.Joystick.performed += P4Navigation;

        //_inputManager.PlayerBlue.Select.performed += 

        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }


    private void PauseStates()
    {
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void P1Confirm(InputAction.CallbackContext context)
    {

    }

    private void P1PauseOnPerformed(InputAction.CallbackContext context)
    {
        PauseStates();
        ptext.text = "P1 Paused Game";
        Debug.Log("P1!");
    }
    private void P2PauseOnPerformed(InputAction.CallbackContext context)
    {
        PauseStates();
        ptext.text = "P2 Paused Game";
        Debug.Log("P2!");
    }
    private void P3PauseOnPerformed(InputAction.CallbackContext context)
    {
        PauseStates();
        ptext.text = "P3 Paused Game";
        Debug.Log("P3!");
    }

    private void P4PauseOnPerformed(InputAction.CallbackContext context)
    {
        PauseStates();
        ptext.text = "P4 Paused Game";
        Debug.Log("P4!");
    }

    private void P3Navigation(InputAction.CallbackContext context)
    {

        if(context.ReadValue<Vector2>().y != 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(menuBtn);
        } 
        else 
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeBtn);
        }
        

        Debug.Log(context.ReadValue<Vector2>().y);
        Debug.Log("P3, pressed");
    }

    private void P4Navigation(InputAction.CallbackContext context)
    {

        if(context.ReadValue<Vector2>().y != 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(menuBtn);
        } 
        else 
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeBtn);
        }
        

        Debug.Log(context.ReadValue<Vector2>().y);
        Debug.Log("P3, pressed");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseButton);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }
    
}
