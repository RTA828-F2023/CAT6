//Author: Victoria Ouryvski 
//Project Name: CAT6
//File Name: SelectController.cs
//Creation Date: Oct 16, 2023
//Modified Date: Oct 30, 2023
//Description: File that manages all UI and code that has to do with Character Select screen
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
    public AudioSource confirmAudio;
    public AudioSource menuScrollAudio;
    public AudioSource menuSelectAudio;

    private InputManager _inputManager;

    //create all variables that show case the text
    public TextMeshProUGUI p1text;
    public TextMeshProUGUI p2text;
    public TextMeshProUGUI p3text;
    public TextMeshProUGUI p4text;

    public TextMeshProUGUI p1CurrChoice;
    public TextMeshProUGUI p2CurrChoice;
    public TextMeshProUGUI p3CurrChoice;
    public TextMeshProUGUI p4CurrChoice;

    public TextMeshProUGUI p1ControlText;
    public TextMeshProUGUI p2ControlText;
    public TextMeshProUGUI p3ControlText;
    public TextMeshProUGUI p4ControlText;


    public TextMeshProUGUI ready;


    //image holders for where backdrop will be displayed
    public Image P1Image;
    public Image P2Image;
    public Image P3Image;
    public Image P4Image;

    public Image P1Char;
    public Image P2Char;
    public Image P3Char;
    public Image P4Char;

    public Image P1OKImage;
    public Image P2OKImage;
    public Image P3OKImage;
    public Image P4OKImage;

    //sprites for all locked characters 
    public Sprite leloLocked;
    public Sprite machoLocked;
    public Sprite eepyLocked;
    public Sprite ruukiLocked;
    public Sprite billiLocked;
    //sprites for all unlocked characters 
    public Sprite lello;
    public Sprite macho;
    public Sprite eepy;
    public Sprite ruuki;
    public Sprite billi;

    public Sprite P1BgLelo;
    public Sprite P1BgMacho;
    public Sprite P1BgEepy;
    public Sprite P1BgRuuki;
    public Sprite P1BgBilli;

    public Sprite P2BgLelo;
    public Sprite P2BgMacho;
    public Sprite P2BgEepy;
    public Sprite P2BgRuuki;
    public Sprite P2BgBilli;

    public Sprite P3BgLelo;
    public Sprite P3BgMacho;
    public Sprite P3BgEepy;
    public Sprite P3BgRuuki;
    public Sprite P3BgBilli;

    public Sprite P4BgLelo;
    public Sprite P4BgMacho;
    public Sprite P4BgEepy;
    public Sprite P4BgRuuki;
    public Sprite P4BgBilli;
    //sprite for ok
    public Sprite ok; 

    //array will keep track of what player is playing, and players choosen character
    private int[] players = { 0, 0, 0, 0 };

    //array keeping track what characters are selected order: lello, macho, eepy, ruuki, billi
    private bool[] isCharSelected = { false, false, false, false, false };

    //constants of all playable character names
    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    //variable to keep track of players taken action
    private int[] pActions = { 0, 0, 0, 0 };

    //constants saying what number means what action taken by player
    private const int NO_ACTIONS = 0;
    private const int LOCKED_IN = 1;
    private const int CHAR_SELECTED = 2;

    //array to keep track of characters
    private int[] characters = new int[5];

    //timer to say how many seconds left
    private float timer = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        //set all text to nothing when game starts
        p1text.text = "";
        p2text.text = "";
        p3text.text = "";
        p4text.text = "";

        p1CurrChoice.text = "";
        p2CurrChoice.text = "";
        p3CurrChoice.text = "";
        p4CurrChoice.text = "";

        P1OKImage.enabled = false;
        P2OKImage.enabled = false;
        P3OKImage.enabled = false;
        P4OKImage.enabled = false;

        ready.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        //call is ready to check if all players are ready to start game
        if (IsReady())
        {
            if (!confirmAudio.isPlaying) confirmAudio.Play();

            //make countdown text appear on screen
            ready.text = ": " + (int)timer;
            timer -= Time.deltaTime;

            //when countdown reaches zero 
            if (timer <= 1.95f)
            {
                // save all player and character details in PlayerPrefs
                PlayerPrefs.SetInt("p1", players[0]);
                PlayerPrefs.SetInt("p2", players[1]);
                PlayerPrefs.SetInt("p3", players[2]);
                PlayerPrefs.SetInt("p4", players[3]);

                //load test level
                SceneLoader.Instance.Load("InstructionScreen");
            }
        }
        else
        {
            //restart text and reset timer to 5 seconds 
            ready.text = "";
            timer = 5.0f;
        }

    }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        //Check if player one pressed, joystick, button 1, button 2
        _inputManager.Player1.Joystick.performed += P1Nav;
        _inputManager.Player1.Btn1.performed += P1Lock;
        _inputManager.Player1.Btn2.performed += P1UnLock;

        //Check if player two pressed, joystick, button 1, button 2
        _inputManager.Player2.Joystick.performed += P2Nav;
        _inputManager.Player2.Btn1.performed += P2Lock;
        _inputManager.Player2.Btn2.performed += P2UnLock;

        //Check if player three pressed, joystick, button 1, button 2 
        _inputManager.Player3.Joystick.performed += P3Nav;
        _inputManager.Player3.Btn1.performed += P3Lock;
        _inputManager.Player3.Btn2.performed += P3UnLock;

        //Check if player four pressed, joystick, button 1, button 2 
        _inputManager.Player4.Joystick.performed += P4Nav;
        _inputManager.Player4.Btn1.performed += P4Lock;
        _inputManager.Player4.Btn2.performed += P4UnLock;

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
    //desc: Let player one navigate the characters
    private void P1Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if (pActions[0] == LOCKED_IN)
        {
            //call navigate subprogram to let player navigate
            nav(context, 0);
        }
    }

    //pre: get context from input action
    //post: none
    //desc: Let player two navigate the characters
    private void P2Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if (pActions[1] == LOCKED_IN)
        {
            //call navigate subprogram to let player navigate
            nav(context, 1);
        }
    }

    //pre: get context from input action
    //post: none
    //desc: Let player three navigate the characters
    private void P3Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if (pActions[2] == LOCKED_IN)
        {
            //call navigate subprogram to let player navigate
            nav(context, 2);
        }
    }

    //pre: get context from input action
    //post: none
    //desc: Let player four navigate the characters
    private void P4Nav(InputAction.CallbackContext context)
    {
        //only do this code if player is locked in
        if (pActions[3] == LOCKED_IN)
        {
            //call navigate subprogram to let player navigate
            nav(context, 3);
        }
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player one action set next lock
    private void P1Lock(InputAction.CallbackContext context)
    {
        //if player currently had no actions do code 
        if (pActions[0] == NO_ACTIONS)
        {
            //change ui text
            Debug.Log("P1 locked in");
            p1text.text = "Player 1";
            p1ControlText.text = "Press 1 to Select!";

            //call subprogram that deals with locking when player has no actions
            PLock_NoAction(context, 0);
        }
        //if player is currently locked in do code
        else if (pActions[0] == LOCKED_IN)
        {
            //change ui text
            Debug.Log("P1 selected char");
            P1OKImage.enabled = true;
            p1ControlText.enabled = false;
            //call subprogram that deals with locking character when player already locked in
            PLock_LockedIn(context, 0);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player two action set next lock
    private void P2Lock(InputAction.CallbackContext context)
    {
        //if player currently had no actions do code
        if (pActions[1] == NO_ACTIONS)
        {
            //change ui text 
            Debug.Log("P2 locked in");
            p2text.text = "Player 2";
            p2ControlText.text = "Press 1 to Select!";

            //call subprogram that deals with locking when player has no actions 
            PLock_NoAction(context, 1);
        }
        //if player is currently locked in do code
        else if (pActions[1] == LOCKED_IN)
        {
            //change ui text 
            Debug.Log("P2 selected char");
            P2OKImage.enabled = true;
            p2ControlText.enabled = false;

            //call subprogram that deals with locking character when player already locked in
            PLock_LockedIn(context, 1);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player three action set next lock
    private void P3Lock(InputAction.CallbackContext context)
    {
        //if player currently had no actions do code 
        if (pActions[2] == NO_ACTIONS)
        {
            //change ui text
            Debug.Log("P3 locked in");
            p3text.text = "Player 3";
            p3ControlText.text = "Press 1 to Select!";

            //call subporgram that deals with locking when player has no actions
            PLock_NoAction(context, 2);
        }
        //if player is currently locked in do code 
        else if (pActions[2] == LOCKED_IN)
        {
            //change ui text 
            Debug.Log("P3 selected char");
            P3OKImage.enabled = true;
            p3ControlText.enabled = false;

            //call subprogram that deals with locking character when player already locked in 
            PLock_LockedIn(context, 2);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player four action set next lock
    private void P4Lock(InputAction.CallbackContext context)
    {
        //if player currently had no actions do code
        if (pActions[3] == NO_ACTIONS)
        {
            //change ui text 
            Debug.Log("P4 locked in");
            p4text.text = "Player 4";
            p4ControlText.text = "Press 1 to Select!";
            //call subprogram that deals with locking when player has no actions
            PLock_NoAction(context, 3);
        }
        //if player is currently locked in do code
        else if (pActions[3] == LOCKED_IN)
        {
            //change ui text
            Debug.Log("P4 selected char");
            P4OKImage.enabled = true;
            p4ControlText.enabled = false;

            //call subprogram that deals with locking character when player already locked in
            PLock_LockedIn(context, 3);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player one action unlock 
    private void P1UnLock(InputAction.CallbackContext context)
    {
        //if player currently locked in 
        if (pActions[0] == LOCKED_IN)
        {
            //change ui text 
            Debug.Log("P1 UNlocked in");
            p1text.text = "";
            p1CurrChoice.text = "";
            p1ControlText.text = "Press 1 to Join!";


            P1Char.sprite = leloLocked;
            P1Image.sprite = P1BgLelo;

            //call subprogram that deals with unlocking when player is locked in
            PUnLock_LockedIn(context, 0);
        }
        //if player already selected character
        else if (pActions[0] == CHAR_SELECTED)
        {
            //change ui text
            Debug.Log("P1 UN - selected char");
            P1OKImage.enabled = false;
            p1ControlText.enabled = true;

            //call subprogram that deals with deselecting character when player already selected a character
            PUnLock_CharSelect(context, 0);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player two action unlock 
    private void P2UnLock(InputAction.CallbackContext context)
    {
        //if player currently locked in
        if (pActions[1] == LOCKED_IN)
        {
            //change ui text
            Debug.Log("P2 UN - locked in");
            p2text.text = "";
            p2CurrChoice.text = "";
            p2ControlText.text = "Press 1 to Join!";

            P2Char.sprite = leloLocked;
            P2Image.sprite = P2BgLelo;

            //call subprogram that deals with unlocking when player is locked in 
            PUnLock_LockedIn(context, 1);
        }
        //if player already selected character 
        else if (pActions[1] == CHAR_SELECTED)
        {
            //change ui text
            Debug.Log("P2 UN - selected char");
            P2OKImage.enabled = false;
            p2ControlText.enabled = true;

            //call subprogram that deals with deselecting character when player already selected a character 
            PUnLock_CharSelect(context, 1);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player three action unlock 
    private void P3UnLock(InputAction.CallbackContext context)
    {
        //if player currently locked in
        if (pActions[2] == LOCKED_IN)
        {
            //change ui text
            Debug.Log("P3 UN - locked in");
            p3text.text = "";
            p3CurrChoice.text = "";
            p3ControlText.text = "Press 1 to Join!";

            P3Char.sprite = leloLocked;
            P3Image.sprite = P3BgLelo;

            //call subprogram that deals with unlocking when player is locked 
            PUnLock_LockedIn(context, 2);
        }
        //if player currently selected character
        else if (pActions[2] == CHAR_SELECTED)
        {
            //change ui text
            Debug.Log("P3 UN - selected char");
            P3OKImage.enabled = false;
            p3ControlText.enabled = true;

            //call subprogram that deals with desselecting character when player already selected character
            PUnLock_CharSelect(context, 2);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action
    //post: none
    //desc: Depending on current previously done player four action unlock 
    private void P4UnLock(InputAction.CallbackContext context)
    {
        //if player currently locked in 
        if (pActions[3] == LOCKED_IN)
        {
            //change ui text
            Debug.Log("P4 UN - locked in");
            p4text.text = "";
            p4CurrChoice.text = "";
            p4ControlText.text = "Press 1 to Join!";

            P4Char.sprite = leloLocked;
            P4Image.sprite = P4BgLelo;

            //call subprogram that deals with unlocking when player is locked
            PUnLock_LockedIn(context, 3);
        }
        //if player currently selected character
        else if (pActions[3] == CHAR_SELECTED)
        {
            //change ui text
            Debug.Log("P4 UN - selected char");
            P4OKImage.enabled = false;
            p4ControlText.enabled = true;

            //call subprogram that deals with deselecting character when player already selected character
            PUnLock_CharSelect(context, 3);
        }

        menuSelectAudio.Play();
    }

    //pre: get context from input action, get player that is navigating
    //post: none
    //desc: Code that related to letting players navigate between characters
    private void nav(InputAction.CallbackContext context, int p)
    {
        //go left or right depending on player joystick move
        if (context.ReadValue<Vector2>().x < 0)
        {
            //navigate left and get next character
            PlayerNav(p, "left");
            GetNextChar(p);
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
            //navigate right and get next character
            PlayerNav(p, "right");
            GetNextChar(p);
        }

        menuScrollAudio.Play();
    }

    //pre: get context from input action, get player that is locking in
    //post: none
    //desc: when player did no actions, lock player in
    private void PLock_NoAction(InputAction.CallbackContext context, int p)
    {

        //set player actions to being locked in
        pActions[p] = LOCKED_IN;

        //get next available character to set as default option for player
        players[p] = StartAt(1, p);

        //get next character for player
        GetNextChar(p);
    }

    //pre: get context from input action, get player that is locking in
    //post: none
    //desc: When player is locked in, let them select their character
    private void PLock_LockedIn(InputAction.CallbackContext context, int p)
    {
        //set player action to character selected
        pActions[p] = CHAR_SELECTED;

        //set that character player choose is now selected
        isCharSelected[players[p] - 1] = true;

        //prevent others from picking selected character
        CheckSameChar(p);
    }

    //pre: get context from input action, get player who is unlocking
    //post: none
    //desc: unlock player if they are currently locked in
    private void PUnLock_LockedIn(InputAction.CallbackContext context, int p)
    {
        //set player to doing no actions
        pActions[p] = NO_ACTIONS;
        players[p] = 0;
    }

    //pre: get context from input action, get player who is unlocking
    //post: none
    //desc: deselect current player selected character
    private void PUnLock_CharSelect(InputAction.CallbackContext context, int p)
    {
        //set player to being just locked in
        pActions[p] = LOCKED_IN;

        //players choosen character is now available to be selected by everyone
        isCharSelected[players[p] - 1] = false;
    }

    //pre: get player who accessing this subproram
    //post: none
    //desc: get and set next character
    private void GetNextChar(int p)
    {
        //create string variable to hold character string
        string charTxt = "";

        //depending on what character player is currently on, save that string name
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

        //depending on what player is currently accesing subprogram grant them the saved string
        if (p == 0)
        {
            //set text to current character text 
            p1CurrChoice.text = charTxt;

            if(charTxt == "lello" )
            {
                P1Char.sprite = lello;
                P1Image.sprite = P1BgLelo;
            }
            if(charTxt == "macho" )
            {
                P1Char.sprite = macho;
                P1Image.sprite = P1BgMacho;
            }
            if(charTxt == "eepy" )
            {
                P1Char.sprite = eepy;
                P1Image.sprite = P1BgEepy;
            }
            if(charTxt == "ruuki" )
            {
                P1Char.sprite = ruuki;
                P1Image.sprite = P1BgRuuki;
            }
            if(charTxt == "billi" )
            {
                P1Char.sprite = billi;
                P1Image.sprite = P1BgBilli;
            }
            

        }
        if (p == 1)
        {
            //set text to current character text
            p2CurrChoice.text = charTxt;

            if(charTxt == "lello" )
            {
                P2Char.sprite = lello;
                P2Image.sprite = P2BgLelo;
            }
            if(charTxt == "macho" )
            {
                P2Char.sprite = macho;
                P2Image.sprite = P2BgMacho;
            }
            if(charTxt == "eepy" )
            {
                P2Char.sprite = eepy;
                P2Image.sprite = P2BgEepy;
            }
            if(charTxt == "ruuki" )
            {
                P2Char.sprite = ruuki;
                P2Image.sprite = P2BgRuuki;
            }
            if(charTxt == "billi" )
            {
                P2Char.sprite = billi;
                P2Image.sprite = P2BgBilli;
            }
        }
        if (p == 2)
        {
            //set text to current character text
            p3CurrChoice.text = charTxt;

            if(charTxt == "lello" )
            {
                P3Char.sprite = lello;
                P3Image.sprite = P3BgLelo;
            }
            if(charTxt == "macho" )
            {
                P3Char.sprite = macho;
                P3Image.sprite = P3BgMacho;
            }
            if(charTxt == "eepy" )
            {
                P3Char.sprite = eepy;
                P3Image.sprite = P3BgEepy;
            }
            if(charTxt == "ruuki" )
            {
                P3Char.sprite = ruuki;
                P3Image.sprite = P3BgRuuki;
            }
            if(charTxt == "billi" )
            {
                P3Char.sprite = billi;
                P3Image.sprite = P3BgBilli;
            }
        }
        if (p == 3)
        {
            //set text to current character text
            p4CurrChoice.text = charTxt;

            if(charTxt == "lello" )
            {
                P4Char.sprite = lello;
                P4Image.sprite = P4BgLelo;
            }
            if(charTxt == "macho" )
            {
                P4Char.sprite = macho;
                P4Image.sprite = P4BgMacho;
            }
            if(charTxt == "eepy" )
            {
                P4Char.sprite = eepy;
                P4Image.sprite = P4BgEepy;
            }
            if(charTxt == "ruuki" )
            {
                P4Char.sprite = ruuki;
                P4Image.sprite = P4BgRuuki;
            }
            if(charTxt == "billi" )
            {
                P4Char.sprite = billi;
                P4Image.sprite = P4BgBilli;
            }
        }
    }

    //pre: get player, get navigational movement
    //post: none
    //desc: let player navigate left and right between character options
    private void PlayerNav(int p, string nav)
    {
        //if player navigating left do code, else do code for right
        if (nav == "left")
        {
            //check if current player is not set to character 1 
            if (players[p] != 1)
            {
                //set player character to next choice
                players[p] -= 1;
            }
            else
            {
                //set player character to 5
                players[p] = 5;
            }
        }
        else
        {
            //check if current player is not set to character 5
            if (players[p] != 5)
            {
                //set player character to next choice
                players[p] += 1;
            }
            else
            {
                //set player character to 1
                players[p] = 1;
            }
        }

        //if character player is on got selected do code
        if (isCharSelected[players[p] - 1])
        {
            //switch to next character
            PlayerNav(p, nav);
        }
    }

    //pre: get player who just choose chacracter
    //post: none
    //desc: check if players choosen character is displayed for someone else as well
    private void CheckSameChar(int pChosen)
    {
        //for all players do code
        for (int i = 0; i < 4; i++)
        {
            //check if players chosen character is displayed currently for somebody else
            if (players[pChosen] == players[i] && pChosen != i)
            {
                //switch player who has same character as already choosen character
                PlayerNav(i, "right");
                GetNextChar(i);
            }
        }
    }

    //pre: get what character to currently start at, get player calling subprogram
    //post: return starting character
    //desc: given starting character, check if that character is available to be selected
    private int StartAt(int start, int p)
    {
        //check if character can be selected
        if (isCharSelected[start - 1])
        {
            //call subprorgam again and check next player
            return StartAt(start + 1, p);
        }

        //return character to start at
        return start;
    }

    //pre: none
    //post: return true of false
    //desc: check if all players are in ready state
    private bool IsReady()
    {
        //keep track of locked in players and players that selected a character
        int countLockIn = 0;
        int countCharSelected = 0;

        //check all players
        for (int i = 0; i < 4; i++)
        {
            //count how many players are playing
            if (pActions[i] != NO_ACTIONS)
            {
                countLockIn += 1;
            }
            //count how many players are ready to play
            if (pActions[i] == CHAR_SELECTED)
            {
                countCharSelected += 1;
            }
        }

        //if players playing is equal to those that are ready 
        if (countLockIn == countCharSelected && countLockIn >= 1)
        {
            //return true
            return true;
        }

        //return false
        return false;
    }
}
