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
    public GameObject exitBtn;
    public GameObject startBtn;
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        //let defauly selected button be start button 
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startBtn);
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
        if(context.ReadValue<Vector2>().y < 0)
        {
            //select menu button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(exitBtn);
        } 
        //else 
        else if (context.ReadValue<Vector2>().y > 0)
        {
            //select resume button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startBtn);
        }
    }

    //pre: get context from input action
    //post: none
    //desc: when player presses ok button click currently selected button
    private void Confirm(InputAction.CallbackContext context)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }
}
