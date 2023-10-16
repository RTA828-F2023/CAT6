using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SelectController : MonoBehaviour
{
    private InputManager _inputManager;
    public TextMeshProUGUI p1text;
    public TextMeshProUGUI p2text;
    public TextMeshProUGUI p3text;
    public TextMeshProUGUI p4text;

    public TextMeshProUGUI p1textOK;
    public TextMeshProUGUI p2textOK;
    public TextMeshProUGUI p3textOK;
    public TextMeshProUGUI p4textOK;

    public TextMeshProUGUI ready;

    //array will keep track of what player is playing, and players choosen character
    private int[] players = {0,0,0,0};

    //constants of all playable character names
    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    //keep track of players taken action
    private int[] pActions = {0,0,0,0};

    //constants saying what number means what action taken by player
    private const int NO_ACTIONS = 0;
    private const int LOCKED_IN = 1;
    private const int CHAR_SELECTED = 2;

    //array to keep track of characters
    private int[] characters = new int[5];

    //TO DO:
    //GET READY TIMER THING TO WORK SO WHEN TIME UP START GAME 
    

    // Start is called before the first frame update
    void Start()
    {
        p1text.text = "";
        p2text.text = "";
        p3text.text = "";
        p4text.text = "";

        p1textOK.text = "";
        p2textOK.text = "";
        p3textOK.text = "";
        p4textOK.text = "";

        ready.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        //Check if player one pressed, select, joystick, or button 1 
        _inputManager.PlayerBlue.Joystick.performed += P1Nav;
        _inputManager.PlayerBlue.Btn1.performed += P1Lock;
        _inputManager.PlayerBlue.Btn2.performed += P1UnLock;

        //Check if player two pressed, select, joystick, or button 1 
        _inputManager.PlayerPink.Joystick.performed += P2Nav;
        _inputManager.PlayerPink.Btn1.performed += P2Lock;
        _inputManager.PlayerPink.Btn2.performed += P2UnLock;

        //Check if player three pressed, select, joystick, or button 1 
        _inputManager.PlayerYellow.Joystick.performed += P3Nav;
        _inputManager.PlayerYellow.Btn1.performed += P3Lock;
        _inputManager.PlayerYellow.Btn2.performed += P3UnLock;
        //Check if player four pressed, select, joystick, or button 1 
        _inputManager.PlayerGreen.Joystick.performed += P4Nav;
        _inputManager.PlayerGreen.Btn1.performed += P4Lock;
        _inputManager.PlayerGreen.Btn2.performed += P4UnLock;

        //enable checking keyinputs
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        //disable checking keyinputs
        _inputManager.Disable();
    }


    private void P1Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[0] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            if(context.ReadValue<Vector2>().x != 1)
            {
                Debug.Log("Left");
            }
            else
            {
                Debug.Log("Right");
            }
        }

        
    }

    private void P2Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[1] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            if(context.ReadValue<Vector2>().x != 1)
            {

                
            }
            else
            {

            }
        }

        
    }
    private void P3Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[2] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            if(context.ReadValue<Vector2>().x != 1)
            {
                
            }
            else
            {

            }
        }

        
    }

    private void P4Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[3] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            if(context.ReadValue<Vector2>().x != 1)
            {
                
            }
            else
            {

            }
        }
        
    }

    private void P1Lock(InputAction.CallbackContext context)
    {
        if( pActions[0] == NO_ACTIONS)
        {
            Debug.Log("P1 locked in");
            p1text.text = "P1";
            pActions[0] = LOCKED_IN;
        }
        else if( pActions[0] == LOCKED_IN)
        {
            Debug.Log("P1 selected char");
            p1textOK.text = "ok";   
            pActions[0] = CHAR_SELECTED;
        }
    }

    private void P2Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[1] == NO_ACTIONS)
        {
            Debug.Log("P2 locked in");
            p2text.text = "P2";
            pActions[1] = LOCKED_IN;
        }
        //only do this code if player is locked in
        else if( pActions[1] == LOCKED_IN)
        {
            Debug.Log("P2 selected char");
            p2textOK.text = "ok";
            pActions[1] = CHAR_SELECTED;
        }
    }

    private void P3Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[2] == NO_ACTIONS)
        {
            Debug.Log("P3 locked in");
            p3text.text = "P3";
            pActions[2] = LOCKED_IN;
        }
        //only do this code if player is locked in
        else if( pActions[2] == LOCKED_IN)
        {
            Debug.Log("P3 selected char");
            p3textOK.text = "ok";
            pActions[2] = CHAR_SELECTED;
        }
    }

    private void P4Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[3] == NO_ACTIONS)
        {
            Debug.Log("P4 locked in");
            p4text.text = "P4";
            pActions[3] = LOCKED_IN;
        }
        //only do this code if player is locked in
        else if (pActions[3] == LOCKED_IN)
        {
            Debug.Log("P4 selected char");
            p4textOK.text = "ok";
            pActions[3] = CHAR_SELECTED;
        }

    }

    private void P1UnLock(InputAction.CallbackContext context)
    {
        if( pActions[0] == LOCKED_IN)
        {
            Debug.Log("P1 UNlocked in");
            p1text.text = "";
            pActions[0] = NO_ACTIONS;
        }
        else if( pActions[0] == CHAR_SELECTED)
        {
            Debug.Log("P1 UN - selected char");
            p1textOK.text = "";   
            pActions[0] = LOCKED_IN;
        }
    }

    private void P2UnLock(InputAction.CallbackContext context)
    {
        if( pActions[1] == LOCKED_IN)
        {
            Debug.Log("P2 UN - locked in");
            p2text.text = "";
            pActions[1] = NO_ACTIONS;
        }
        else if(pActions[1] == CHAR_SELECTED)
        {
            Debug.Log("P2 UN - selected char");
            p2textOK.text = "";   
            pActions[1] = LOCKED_IN;
        }
    }

    private void P3UnLock(InputAction.CallbackContext context)
    {
        if( pActions[2] == LOCKED_IN)
        {
            Debug.Log("P3 UN - locked in");
            p3text.text = "";
            pActions[2] = NO_ACTIONS;
        }
        else if(pActions[2] == CHAR_SELECTED)
        {
            Debug.Log("P3 UN - selected char");
            p3textOK.text = "";   
            pActions[2] = LOCKED_IN;
        }
    }

    private void P4UnLock(InputAction.CallbackContext context)
    {
        if( pActions[3] == LOCKED_IN)
        {
            Debug.Log("P4 UN - locked in");
            p4text.text = "";
            pActions[3] = NO_ACTIONS;
        }
        else if(pActions[3] == CHAR_SELECTED)
        {
            Debug.Log("P4 UN - selected char");
            p4textOK.text = "";   
            pActions[3] = LOCKED_IN;
        }
    }
}
