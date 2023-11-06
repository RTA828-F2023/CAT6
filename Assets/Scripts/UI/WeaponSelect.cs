using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WeaponSelect : MonoBehaviour
{
    private InputManager _inputManager;
    public TextMeshProUGUI next;

    public TextMeshProUGUI P1W1;
    public TextMeshProUGUI P2W1;
    public TextMeshProUGUI P3W1;
    public TextMeshProUGUI P4W1;

    public TextMeshProUGUI P1W2;
    public TextMeshProUGUI P2W2;
    public TextMeshProUGUI P3W2;
    public TextMeshProUGUI P4W2;

    public TextMeshProUGUI P1W3;
    public TextMeshProUGUI P2W3;
    public TextMeshProUGUI P3W3;
    public TextMeshProUGUI P4W3;

    private int[] players = {0,0,0,0};

    //timer to say how many seconds left
    private float timer = 20.0f;

    public GameObject weaponSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelectMenu.SetActive(true);

        P1W1.text = "";
        P1W1.text = "";
        P3W1.text = "";
        P4W1.text = "";

        P1W2.text = "";
        P2W2.text = "";
        P3W2.text = "";
        P4W2.text = "";


        P1W3.text = "";
        P2W3.text = "";
        P3W3.text = "";
        P4W3.text = "";
        

        if(PlayerPrefs.GetInt("p1") != 0)
        {
            DrawPins(0);
        }

        if(PlayerPrefs.GetInt("p2") != 0)
        {
            DrawPins(1);
        }
        
        if(PlayerPrefs.GetInt("p3") != 0)
        {
            DrawPins(2);
        }
        
        if(PlayerPrefs.GetInt("p4") != 0)
        {
            DrawPins(3);
        }

    }

    // Update is called once per frame
    void Update()
    {
        next.text = "Next Wave In... " + (int)timer;
        timer -= Time.deltaTime;

        if(timer <= 1.95f)
        {
            // save all player and character details in PlayerPrefs
            PlayerPrefs.SetInt("p1W", players[0]);
            PlayerPrefs.SetInt("p2W", players[1]);
            PlayerPrefs.SetInt("p3W", players[2]);
            PlayerPrefs.SetInt("p4W", players[3]);

            weaponSelectMenu.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _inputManager = new InputManager();

        //Check if player one pressed, joystick, button 1, button 2
        if(PlayerPrefs.GetInt("p1") != 0)
        {
            _inputManager.Player1.Joystick.performed += P1Nav;
        }

        if(PlayerPrefs.GetInt("p2") != 0)
        {
            //Check if player two pressed, joystick, button 1, button 2
            _inputManager.Player2.Joystick.performed += P2Nav;
        }
        
        if(PlayerPrefs.GetInt("p3") != 0)
        {
            //Check if player three pressed, joystick, button 1, button 2 
            _inputManager.Player3.Joystick.performed += P3Nav;
        }
        
        if(PlayerPrefs.GetInt("p4") != 0)
        {
            //Check if player four pressed, joystick, button 1, button 2 
            _inputManager.Player4.Joystick.performed += P4Nav;
        }
        

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
        //call navigate subprogram to let player navigate
        nav(context, 0);
    }

    //pre: get context from input action
    //post: none
    //desc: Let player two navigate the characters
    private void P2Nav(InputAction.CallbackContext context)
    {
        //call navigate subprogram to let player navigate
        nav(context, 1);
    }

    //pre: get context from input action
    //post: none
    //desc: Let player three navigate the characters
    private void P3Nav(InputAction.CallbackContext context)
    {
        //call navigate subprogram to let player navigate
        nav(context, 2);
    }

    //pre: get context from input action
    //post: none
    //desc: Let player four navigate the characters
    private void P4Nav(InputAction.CallbackContext context)
    {
        //call navigate subprogram to let player navigate
        nav(context, 3);
    }

    private void nav(InputAction.CallbackContext context, int p)
    {
        //go left or right depending on player joystick move
        if(context.ReadValue<Vector2>().x < 0)
        {
            //navigate left and get next character
            PlayerNav(p, "left");
            DrawPins(p);
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
            //navigate right and get next character
            PlayerNav(p, "right");
            DrawPins(p);
        }
    }
    

    private void DrawPins(int p)
    {

        //depending on what player is currently accesing subprogram grant them the saved string
        if(p == 0)
        {
            if(players[p] == 0)
            {
                P1W1.text = "P1";
                P1W2.text = "";
                P1W3.text = "";
            }
            if(players[p] == 1)
            {
                P1W1.text = "";
                P1W2.text = "P1";
                P1W3.text = "";
            }
            if(players[p] == 2)
            {
                P1W1.text = "";
                P1W2.text = "";
                P1W3.text = "P1";
            }
        }
        if(p == 1)
        {
            if(players[p] == 0)
            {
                P2W1.text = "P2";
                P2W2.text = "";
                P2W3.text = "";
            }
            if(players[p] == 1)
            {
                P2W1.text = "";
                P2W2.text = "P2";
                P2W3.text = "";
            }
            if(players[p] == 2)
            {
                P2W1.text = "";
                P2W2.text = "";
                P2W3.text = "P2";
            }
        }
        if(p == 2)
        {
            if(players[p] == 0)
            {
                P3W1.text = "P3";
                P3W2.text = "";
                P3W3.text = "";
            }
            if(players[p] == 1)
            {
                P3W1.text = "";
                P3W2.text = "P3";
                P3W3.text = "";
            }
            if(players[p] == 2)
            {
                P3W1.text = "";
                P3W2.text = "";
                P3W3.text = "P3";
            }
        }
        if(p == 3)
        {
            if(players[p] == 0)
            {
                P4W1.text = "P4";
                P4W2.text = "";
                P4W3.text = "";
            }
            if(players[p] == 1)
            {
                P4W1.text = "";
                P4W2.text = "P4";
                P4W3.text = "";
            }
            if(players[p] == 2)
            {
                P4W1.text = "";
                P4W2.text = "";
                P4W3.text = "P4";
            }
        }
    }

    //pre: get player, get navigational movement
    //post: none
    //desc: let player navigate left and right between weapon options
    private void PlayerNav(int p, string nav)
    {
        //if player navigating left do code, else do code for right
        if (nav == "left")
        {
            //check if current player is not set to character 1 
            if(players[p] != 0)
            {
                //set player character to next choice
                players[p] -= 1;
            }
        }
        else 
        {
            //check if current player is not set to character 5
            if(players[p] != 2)
            {
                //set player character to next choice
                players[p] += 1;
            }
        }
    }
}
