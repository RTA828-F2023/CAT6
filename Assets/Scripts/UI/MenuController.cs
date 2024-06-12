//Author: Victoria Ouryvski 
//Project Name: CAT6
//File Name: MenuController.cs
//Creation Date: Oct 9, 2023
//Modified Date: Oct 30, 2023
//Description: File that manages all UI for the menu 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject[] btns;
    private int _btnIndex;

    public AudioSource menuScrollAudio;
    public AudioSource menuSelectAudio;

    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        //let defauly selected button be start button 
        SelectBtn(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        //Check if player one pressed, joystick, or button 1 
        _inputManager.Player1.Joystick.performed += Nav;
        _inputManager.Player1.Btn1.performed += Confirm;

        //Check if player two pressed, joystick, or button 1 
        _inputManager.Player2.Joystick.performed += Nav;
        _inputManager.Player2.Btn1.performed += Confirm;

        //Check if player three pressed, joystick, or button 1 
        _inputManager.Player3.Joystick.performed += Nav;
        _inputManager.Player3.Btn1.performed += Confirm;

        //Check if player four pressed, joystick, or button 1 
        _inputManager.Player4.Joystick.performed += Nav;
        _inputManager.Player4.Btn1.performed += Confirm;

        _inputManager.Enable();

    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

    //pre: get context from input action
    //post: none
    //desc: Let all players navigate the main menu screen
    private void Nav(InputAction.CallbackContext context)
    {
        //if(context.ReadValue<Vector2>().y != 1)
        if (context.ReadValue<Vector2>().y > 0)
        {
            SelectBtn((_btnIndex + 1 > btns.Length - 1) ? 0 : _btnIndex + 1);
        }
        //else 
        else if (context.ReadValue<Vector2>().y < 0)
        {
            SelectBtn((_btnIndex - 1 < 0) ? btns.Length - 1 : _btnIndex - 1);
        }
        menuScrollAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: when player presses ok button click currently selected button
    private void Confirm(InputAction.CallbackContext context)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        menuSelectAudio.Play();
    }

    private void SelectBtn(int index)
    {
        _btnIndex = index;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btns[_btnIndex]);
    }
}
