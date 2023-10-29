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

        //Check if player two pressed, select, joystick, or button 1 
        _inputManager.Player2.Joystick.performed += Nav;
        _inputManager.Player2.Btn1.performed += Confirm;

        //Check if player three pressed, joystick, or button 1 
        _inputManager.Player3.Joystick.performed += Nav;
        _inputManager.Player3.Btn1.performed += Confirm;

        //Check if player four pressed, joystick, or button 1 
        _inputManager.Player4.Joystick.performed += Nav;
        _inputManager.Player4.Btn1.performed += Confirm;

        _inputManager.Enable();

        Debug.Log("I'm in");

    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }

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

        Debug.Log("I'm in Nav");
    }

    private void Confirm(InputAction.CallbackContext context)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }
}
