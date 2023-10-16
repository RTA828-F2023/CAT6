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

        //Check if player two pressed, select, joystick, or button 1 
        _inputManager.PlayerPink.Joystick.performed += P2Nav;
        _inputManager.PlayerPink.Btn1.performed += P2Lock;

        //Check if player three pressed, select, joystick, or button 1 
        _inputManager.PlayerYellow.Joystick.performed += P3Nav;
        _inputManager.PlayerYellow.Btn1.performed += P3Lock;

        //Check if player four pressed, select, joystick, or button 1 
        _inputManager.PlayerGreen.Joystick.performed += P4Nav;
        _inputManager.PlayerGreen.Btn1.performed += P4Lock;

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
        }
    }

    private void P2Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[1] == NO_ACTIONS)
        {

        }
        //only do this code if player is locked in
        else if( pActions[1] == LOCKED_IN)
        {

        }
    }

    private void P3Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[2] == NO_ACTIONS)
        {

        }
        //only do this code if player is locked in
        else if( pActions[2] == LOCKED_IN)
        {

        }
    }

    private void P4Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[3] == NO_ACTIONS)
        {

        }
        //only do this code if player is locked in
        else if (pActions[3] == LOCKED_IN)
        {

        }

    }
}
