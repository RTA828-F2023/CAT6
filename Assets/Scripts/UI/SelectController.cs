using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectController : MonoBehaviour
{

    private InputManager _inputManager;
    public TextMeshProUGUI p1text;
    public TextMeshProUGUI p2text;
    public TextMeshProUGUI p3text;
    public TextMeshProUGUI p4text;

    public TextMeshProUGUI p1CurrChoice;
    public TextMeshProUGUI p2CurrChoice;
    public TextMeshProUGUI p3CurrChoice;
    public TextMeshProUGUI p4CurrChoice;

    public TextMeshProUGUI p1textOK;
    public TextMeshProUGUI p2textOK;
    public TextMeshProUGUI p3textOK;
    public TextMeshProUGUI p4textOK;

    public TextMeshProUGUI ready;

    //array will keep track of what player is playing, and players choosen character
    private int[] players = {0,0,0,0};

    //                               lello, macho, eepy, ruuki, billi
    private bool[] isCharSelected = {false, false, false, false, false};

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

    private float timer = 5.0f;
    

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

        StartCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsReady())
        {
            ready.text = "Ready in... " + (int)timer;
            timer -= Time.deltaTime;

            if(timer <= 1.95f)
            {
                PlayerPrefs.SetInt("p1", players[0]);
                PlayerPrefs.SetInt("p2", players[1]);
                PlayerPrefs.SetInt("p3", players[2]);
                PlayerPrefs.SetInt("p4", players[3]);
                
                SceneManager.LoadScene("TestLevel");
            }
        }
        else 
        {
            ready.text = "";
            timer = 5.0f;
        }
        
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
            nav(context, 0);
        }
    }

    private void P2Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[1] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            nav(context, 1);
        }
    }
    private void P3Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[2] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            nav(context, 2);
        }
    }

    private void P4Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if( pActions[3] == LOCKED_IN)
        {
            //if player used joystick left, else player moved joystick to the right
            nav(context, 3);
        }
    }

    private void P1Lock(InputAction.CallbackContext context)
    {
        if( pActions[0] == NO_ACTIONS)
        {
            Debug.Log("P1 locked in");
            p1text.text = "P1";
            PLock_NoAction(context, 0);
        }
        else if( pActions[0] == LOCKED_IN)
        {
            Debug.Log("P1 selected char");
            p1textOK.text = "ok";   
            PLock_LockedIn(context, 0);
        }
    }

    private void P2Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[1] == NO_ACTIONS)
        {
            Debug.Log("P2 locked in");
            p2text.text = "P2";
            PLock_NoAction(context, 1);
        }
        //only do this code if player is locked in
        else if( pActions[1] == LOCKED_IN)
        {
            Debug.Log("P2 selected char");
            p2textOK.text = "ok";
            PLock_LockedIn(context, 1);
        }
    }

    private void P3Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[2] == NO_ACTIONS)
        {
            Debug.Log("P3 locked in");
            p3text.text = "P3";
            PLock_NoAction(context, 2);
        }
        //only do this code if player is locked in
        else if( pActions[2] == LOCKED_IN)
        {
            Debug.Log("P3 selected char");
            p3textOK.text = "ok";
            PLock_LockedIn(context, 2);
        }
    }

    private void P4Lock(InputAction.CallbackContext context)
    {
        //only do this code if player has not done anything before
        if( pActions[3] == NO_ACTIONS)
        {
            Debug.Log("P4 locked in");
            p4text.text = "P4";
            PLock_NoAction(context, 3);
        }
        //only do this code if player is locked in
        else if (pActions[3] == LOCKED_IN)
        {
            Debug.Log("P4 selected char");
            p4textOK.text = "ok";
            PLock_LockedIn(context, 3);
        }

    }

    private void P1UnLock(InputAction.CallbackContext context)
    {
        if( pActions[0] == LOCKED_IN)
        {
            Debug.Log("P1 UNlocked in");
            p1text.text = "";
            PUnLock_LockedIn(context, 0);
        }
        else if( pActions[0] == CHAR_SELECTED)
        {
            Debug.Log("P1 UN - selected char");
            p1textOK.text = "";   
            PUnLock_CharSelect(context, 0);
        }
    }

    private void P2UnLock(InputAction.CallbackContext context)
    {
        if( pActions[1] == LOCKED_IN)
        {
            Debug.Log("P2 UN - locked in");
            p2text.text = "";
            PUnLock_LockedIn(context, 1);
        }
        else if(pActions[1] == CHAR_SELECTED)
        {
            Debug.Log("P2 UN - selected char");
            p2textOK.text = "";   
            PUnLock_CharSelect(context, 1);
        }
    }

    private void P3UnLock(InputAction.CallbackContext context)
    {
        if( pActions[2] == LOCKED_IN)
        {
            Debug.Log("P3 UN - locked in");
            p3text.text = "";
            PUnLock_LockedIn(context, 2);
        }
        else if(pActions[2] == CHAR_SELECTED)
        {
            Debug.Log("P3 UN - selected char");
            p3textOK.text = "";   
            PUnLock_CharSelect(context, 2);
        }
    }

    private void P4UnLock(InputAction.CallbackContext context)
    {
        if( pActions[3] == LOCKED_IN)
        {
            Debug.Log("P4 UN - locked in");
            p4text.text = "";
            PUnLock_LockedIn(context, 3);
        }
        else if(pActions[3] == CHAR_SELECTED)
        {
            Debug.Log("P4 UN - selected char");
            p4textOK.text = "";   
            PUnLock_CharSelect(context, 3);
        }
    }

    private void nav(InputAction.CallbackContext context, int p)
    {
        if(context.ReadValue<Vector2>().x < 0)
        {
            PlayerNav(p, "left");
            GetNextChar(p);
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
            PlayerNav(p, "right");
            GetNextChar(p);
        }
    }

    private void PLock_NoAction(InputAction.CallbackContext context, int p)
    {
        pActions[p] = LOCKED_IN;
        players[p] = StartAt(1,p);

        Debug.Log( "start char " + players[p]);
        GetNextChar(p);
    }

    private void PLock_LockedIn(InputAction.CallbackContext context, int p)
    {
        pActions[p] = CHAR_SELECTED;

        isCharSelected[players[p] - 1] = true;
        CheckSameChar(p);
    }

    private void PUnLock_LockedIn(InputAction.CallbackContext context, int p)
    {
        pActions[p] = NO_ACTIONS;
    }

    private void PUnLock_CharSelect(InputAction.CallbackContext context, int p)
    {
        pActions[p] = LOCKED_IN;
        isCharSelected[players[p] - 1] = false;
    }

    private void GetNextChar( int p)
    {
        string charTxt = "";
        //check if player is currently not selected
        //if character player wants to choose is already selected, skip to next one
            
        switch (players[p])
            {
            case LELLO:
                charTxt = "lello";
                break;
            case MACHO:
                charTxt = "macho";
                break;
            case EEPY:
                charTxt = "eepy";
                break;
            case RUUKI:
                charTxt = "ruuki";
                break;
            case BILLI:
                charTxt = "billi";
                break;
            default:
                charTxt = "default";
                break;
            }

        if( p == 0)
        {
            p1CurrChoice.text = charTxt;
        }
        if( p == 1)
        {
            p2CurrChoice.text = charTxt;
        }
        if( p == 2)
        {
            p3CurrChoice.text = charTxt;
        }
        if( p == 3)
        {
            p4CurrChoice.text = charTxt;
        }
    }

    private void PlayerNav(int p, string nav)
    {
        if (nav == "left")
        {
            if(players[p] != 1)
            {
                players[p] -= 1;
            }
            else
            {
                players[p] = 5;
            }
        }
        else 
        {
            if(players[p] != 5)
            {
                players[p] += 1;
                Debug.Log(players[p]);
            }
            else
            {
                players[p] = 1;
                Debug.Log(players[p]);
            }
        }

        if (isCharSelected[players[p] - 1])
        {
            PlayerNav(p, nav);
        }
    }

    private void CheckSameChar(int pChosen)
    {
        for (int i = 0; i < 4; i++)
        {
            if (players[pChosen] == players[i] && pChosen != i)
            {
                PlayerNav(i, "right");
                GetNextChar(i );
            }
        }
    }

    private int StartAt(int start, int p)
    {
        if(isCharSelected[start - 1])
        {
            return StartAt(start + 1, p);
        }

        return start;
    }

    private bool IsReady()
    {
        //check how many players are currently locked in
        //compare against how many ppl are ready(selected their char and on ok)
        //if # of player locked == # of ppl who are ready 
        //then start countdown 

        int countLockIn = 0;
        int countCharSelected = 0;

        for (int i = 0; i < 4; i++)
        {
            if(pActions[i] != NO_ACTIONS)
            {
                countLockIn += 1;
            }
            if(pActions[i] == CHAR_SELECTED)
            {
                countCharSelected += 1;
            }
        }

        if(countLockIn == countCharSelected && countLockIn >= 1)
        {

            return true;
        }

        return false;
    }

    private void StartCountDown()
    {
        

    }

}
