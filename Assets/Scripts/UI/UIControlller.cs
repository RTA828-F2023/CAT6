//Author: Victoria Ouryvski 
//Credit: Duong Vu, for help with debugging the code
//Project Name: CAT6
//File Name: UIControlller.cs
//Creation Date: Sep 25, 2023
//Modified Date: Oct 10, 2023
//Description: File that manages all UI for pausing the game, this includes detecting when player paused game, navigation in pause
//menu, and exiting the pause menu to go back to game, or main menu.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem.XInput;

public class UIControlller : MonoBehaviour
{
    public AudioSource pauseAudio;
    public AudioSource resumeAudio;
    public AudioSource menuScrollAudio;
    public AudioSource menuSelectAudio;

    //store object variable for pauseMenu panel 
    public GameObject pauseMenu;

    //store object variable for the two buttons
    public GameObject resumeBtn;
    public GameObject menuBtn;

    //get input manager
    private InputManager _inputManager;

    //variable to keep track of what player paused game, set to 0 meaning no one
    private int playerPause = 0;

    // get button that will be default selected
    public GameObject firstPauseButton;

    //get text that will say what player paused
    public TextMeshProUGUI ptext;

    //In-Game Menu GameObject
    [SerializeField] private GameObject inGameUI;

    private List<int> _inputDeviceIds;

    //when scene starts
    void Start()
    {
        _inputDeviceIds = InputSystem.devices.OfType<XInputController>().Select(controller => controller.deviceId).ToList();
        //set pause menu to not being active
        pauseMenu.SetActive(false);

        //set player pause to zero
        playerPause = 0;
    }

    //when pause panel enabled
    private void OnEnable()
    {
        _inputManager = new InputManager();

        //Check if player one pressed, select, joystick, or button 1 
        _inputManager.Player1.Select.performed += P1PauseOnPerformed;
        _inputManager.Player1.Joystick.performed += P1Nav;
        _inputManager.Player1.Btn1.performed += P1Confirm;

        //Check if player two pressed, select, joystick, or button 1 
        _inputManager.Player2.Select.performed += P2PauseOnPerformed;
        _inputManager.Player2.Joystick.performed += P2Nav;
        _inputManager.Player2.Btn1.performed += P2Confirm;

        //Check if player three pressed, select, joystick, or button 1 
        _inputManager.Player3.Select.performed += P3PauseOnPerformed;
        _inputManager.Player3.Joystick.performed += P3Nav;
        _inputManager.Player3.Btn1.performed += P3Confirm;

        //Check if player four pressed, select, joystick, or button 1 
        _inputManager.Player4.Select.performed += P4PauseOnPerformed;
        _inputManager.Player4.Joystick.performed += P4Nav;
        _inputManager.Player4.Btn1.performed += P4Confirm;

        //enable checking keyinputs
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        //disable checking keyinputs
        _inputManager.Disable();
    }

    //pre: get context from input action
    //post: none
    //desc: Let player one pause game
    private void P1PauseOnPerformed(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 0) return;
        if (context.control.device.deviceId != _inputDeviceIds[0]) return;

        //if a player currently did not press pause
        if (playerPause == 0)
        {
            //call pause game
            PauseGame();

            //set variables to say player one paused game
            ptext.text = "P1 Paused Game";
            playerPause = 1;
        }


    }
    //pre: get context from input action
    //post: none
    //desc: Let player two pause game
    private void P2PauseOnPerformed(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 1) return;
        if (context.control.device.deviceId != _inputDeviceIds[1]) return;

        //if a player currently did not press pause
        if (playerPause == 0)
        {
            //call pause game
            PauseGame();

            //set variables to say player two paused the game
            ptext.text = "P2 Paused Game";
            playerPause = 2;
        }

    }

    //pre: get context from input action 
    //post: none
    //desc: Let player three pause game
    private void P3PauseOnPerformed(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 2) return;
        if (context.control.device.deviceId != _inputDeviceIds[2]) return;

        //if a player currently did not press pause
        if (playerPause == 0)
        {
            //call pause game
            PauseGame();

            //set variables to say player three paused the game
            ptext.text = "P3 Paused Game";
            playerPause = 3;
        }
    }

    //pre: get context from input action
    //post: none
    //desc: Let player four pause the game
    private void P4PauseOnPerformed(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 3) return;
        if (context.control.device.deviceId != _inputDeviceIds[3]) return;

        //if a player currently did not press pause
        if (playerPause == 0)
        {
            //call pause game
            PauseGame();

            //set variables to say player four paused the game
            ptext.text = "P4 Paused Game";
            playerPause = 4;
        }

    }

    //pre: get context from input action
    //post: none
    //desc: let player one navigate the pause menu
    private void P1Nav(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 0) return;
        if (context.control.device.deviceId != _inputDeviceIds[0]) return;

        //make sure player one paused game
        if (playerPause == 1)
        {
            //call navigation
            Navigation(context);
        }

    }
    //pre: get context from input action
    //post: none
    //desc: let player two navigate the pause menu
    private void P2Nav(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 1) return;
        if (context.control.device.deviceId != _inputDeviceIds[1]) return;

        //make sure player two paused game
        if (playerPause == 2)
        {
            //call navigation
            Navigation(context);
        }

    }

    //pre: get context from input action
    //post: none
    //desc: let player three navigate the pause menu
    private void P3Nav(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 2) return;
        if (context.control.device.deviceId != _inputDeviceIds[2]) return;

        //make sure player three paused game
        if (playerPause == 3)
        {
            //call navigation
            Navigation(context);
        }

    }

    //pre: get context from input action
    //post: none
    //desc: let player four navigate the pause menu
    private void P4Nav(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 3) return;
        if (context.control.device.deviceId != _inputDeviceIds[3]) return;

        //make sure player four paused game
        if (playerPause == 4)
        {
            //call navigtaion
            Navigation(context);
        }

    }

    //pre: get context from input action
    //post: none
    //desc: Allows a user to navigate the pause menu, be selecting buttons, and clickin them
    private void Navigation(InputAction.CallbackContext context)
    {
        //figure out if player is moving joystick up or down
        //if(context.ReadValue<Vector2>().y != 1)
        if (context.ReadValue<Vector2>().y < 0)
        {
            //select menu button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(menuBtn);
        }
        //else 
        else if (context.ReadValue<Vector2>().y > 0)
        {
            //select resume button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeBtn);
        }

        menuScrollAudio.Play();
    }

    //pre: get conext from input action
    //none: none
    //desc: Let player one confirm their selection
    private void P1Confirm(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 0) return;
        if (context.control.device.deviceId != _inputDeviceIds[0]) return;

        //make sure player one paused game
        if (playerPause == 1)
        {
            //click the current selected button
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        menuSelectAudio.Play();
    }

    //pre: get conext from input action
    //none: none
    //desc: Let player two confirm their selection
    private void P2Confirm(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 1) return;
        if (context.control.device.deviceId != _inputDeviceIds[1]) return;

        //make sure player two paused game
        if (playerPause == 2)
        {
            //click the current selected button
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        menuSelectAudio.Play();
    }

    //pre: get conext from input action
    //none: none
    //desc: Let player three confirm their selection
    private void P3Confirm(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 2) return;
        if (context.control.device.deviceId != _inputDeviceIds[2]) return;

        //make sure player three paused game
        if (playerPause == 3)
        {
            //click the current selected button
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        menuSelectAudio.Play();
    }

    //pre: get conext from input action
    //none: none
    //desc: Let player four confirm their selection
    private void P4Confirm(InputAction.CallbackContext context)
    {
        if (_inputDeviceIds.Count <= 3) return;
        if (context.control.device.deviceId != _inputDeviceIds[3]) return;

        //make sure player four paused game
        if (playerPause == 4)
        {
            //click the current selected button
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }

        menuSelectAudio.Play();
    }

    //pre: none
    //post: none
    //desc: Pauses the game
    public void PauseGame()
    {
        //set pause menu panel to active
        pauseMenu.SetActive(true);

        //stop ingame time
        Time.timeScale = 0f;

        //make the chosen button be the pre-set selectable option
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseButton);

        inGameUI.SetActive(false);

        pauseAudio.Play();
        GameController.Instance.SetDepthOfField(true);
    }

    //pre: none
    //post: none
    //desc: Resume the game
    public void ResumeGame()
    {
        //set pause menu panel to be unactive
        pauseMenu.SetActive(false);

        //resume ingame time 
        Time.timeScale = 1f;

        //set player pause back to 0
        playerPause = 0;

        inGameUI.SetActive(true);


        resumeAudio.Play();
        GameController.Instance.SetDepthOfField(false);
    }

}
