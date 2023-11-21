//Author: Victoria Ouryvski 
//Project Name: CAT6
//File Name: UIMatchScore.cs
//Creation Date: Oct 13, 2023
//Modified Date: Nov 20, 2023
//Description: File that manages all UI and code that has to do with displaying match scores
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public GameObject matchScoresUI;
    public Sprite lello;
    public Sprite macho;
    public Sprite eepy;
    public Sprite ruuki;
    public Sprite billi;

    public Image Image1st;
    public Image Image2nd;
    public Image Image3rd;
    public Image Image4th;

    //make constants for all characters
    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    public TextMeshProUGUI[] scoresText;
    public TextMeshProUGUI totalScores; 
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    public PointSystemController pointSystem;

    // Start is called before the first frame update
    void Start()
    {
         //make everything dissapear from screen when game starts
        matchScoresUI.SetActive(false);
       
        Image1st.color = Color.white;
        Image2nd.color = Color.white;
        Image3rd.color = Color.white;
        Image4th.color = Color.white;

        Image1st.enabled = false;
        Image2nd.enabled = false;
        Image3rd.enabled = false;
        Image4th.enabled = false;

        scoresText[0].text = "";
        scoresText[1].text = "";
        scoresText[2].text = "";
        scoresText[3].text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //pre: none
    //post: none
    //desc: obtains all player scores and stores them
    void getAllScores()
    {
        //add each player to dictionary and save score
        if(PlayerPrefs.GetInt("p1") != 0 )
        {
            playerScores.Add("p1", pointSystem.GetPlayerScore(PlayerType.One));
        }
        if(PlayerPrefs.GetInt("p2") != 0 )
        {
            playerScores.Add("p2", pointSystem.GetPlayerScore(PlayerType.Two));
        }
        if(PlayerPrefs.GetInt("p3") != 0 )
        {
            playerScores.Add("p3", pointSystem.GetPlayerScore(PlayerType.Three));
        }
        if(PlayerPrefs.GetInt("p4") != 0 )
        {
            playerScores.Add("p4", pointSystem.GetPlayerScore(PlayerType.Four));
        }

        //sort by smallest to highest score
        playerScores.OrderByDescending(key => key.Value);
    }

    //pre: none
    //post: none
    //desc: draws out all player score texts
    public void DrawScores()
    {
        //call get all scores to get all player scores
        getAllScores();

        //set variables
        int i = 0;
        int tScore = 0;

        //for each player listed in order from high score to low score loop
        foreach ( KeyValuePair<string, int> player in playerScores)
        {
            //update string
            scoresText[i].text = player.Value.ToString();

            //add player score to total score
            tScore = tScore + player.Value;

            //draw player and thier character
            DrawCharacters(i, player.Key);
            
            //incremenet next place
            i++;
        }

        //draw total scores
        totalScores.text = "" + tScore;
    }

    //pre: needs place and current player
    //post: none
    //desc: draws each character in thier relevant place slot
    public void DrawCharacters(int place, string player)
    {
        //depedning on place do relevant code 
        if(place == 0)
        {
            //depending on players choosen character draw them
            if(PlayerPrefs.GetInt(player) == LELLO)
            {
                Image1st.enabled = true;
                Image1st.color = Color.white;
                Image1st.sprite = lello;
            }
            if(PlayerPrefs.GetInt(player) == MACHO)
            {
                Image1st.enabled = true;
                Image1st.color = Color.white;
                Image1st.sprite = macho;   
            }
            if(PlayerPrefs.GetInt(player) == EEPY)
            {
                Image1st.enabled = true;
                Image1st.color = Color.white;
                Image1st.sprite = eepy;
            }
            if(PlayerPrefs.GetInt(player) == RUUKI)
            {
                Image1st.enabled = true;
                Image1st.color = Color.white;
                Image1st.sprite = ruuki;
            }
            if(PlayerPrefs.GetInt(player) == BILLI)
            {
                Image1st.enabled = true;
                Image1st.color = Color.white;
                Image1st.sprite = billi;
            }
        }
        if(place == 1)
        {
            //depending on players choosen character draw them
            if(PlayerPrefs.GetInt(player) == LELLO)
            {
                Image2nd.enabled = true;
                Image2nd.color = Color.white;
                Image2nd.sprite = lello;
            }
            if(PlayerPrefs.GetInt(player) == MACHO)
            {
                Image2nd.enabled = true;
                Image2nd.color = Color.white;
                Image2nd.sprite = macho;   
            }
            if(PlayerPrefs.GetInt(player) == EEPY)
            {
                Image2nd.enabled = true;
                Image2nd.color = Color.white;
                Image2nd.sprite = eepy;
            }
            if(PlayerPrefs.GetInt(player) == RUUKI)
            {
                Image2nd.enabled = true;
                Image2nd.color = Color.white;
                Image2nd.sprite = ruuki;
            }
            if(PlayerPrefs.GetInt(player) == BILLI)
            {
                Image2nd.enabled = true;
                Image2nd.color = Color.white;
                Image2nd.sprite = billi;
            }
        }
        if(place == 2)
        {
            //depending on players choosen character draw them
            if(PlayerPrefs.GetInt(player) == LELLO)
            {
                Image3rd.enabled = true;
                Image3rd.color = Color.white;
                Image3rd.sprite = lello;
            }
            if(PlayerPrefs.GetInt(player) == MACHO)
            {
                Image3rd.enabled = true;
                Image3rd.color = Color.white;
                Image3rd.sprite = macho;   
            }
            if(PlayerPrefs.GetInt(player) == EEPY)
            {
                Image3rd.enabled = true;
                Image3rd.color = Color.white;
                Image3rd.sprite = eepy;
            }
            if(PlayerPrefs.GetInt(player) == RUUKI)
            {
                Image3rd.enabled = true;
                Image3rd.color = Color.white;
                Image3rd.sprite = ruuki;
            }
            if(PlayerPrefs.GetInt(player) == BILLI)
            {
                Image3rd.enabled = true;
                Image3rd.color = Color.white;
                Image3rd.sprite = billi;
            }
        }
        if(place == 3)
        {
            //depending on players choosen character draw them
            if(PlayerPrefs.GetInt(player) == LELLO)
            {
                Image4th.enabled = true;
                Image4th.color = Color.white;
                Image4th.sprite = lello;
            }
            if(PlayerPrefs.GetInt(player) == MACHO)
            {
                Image4th.enabled = true;
                Image4th.color = Color.white;
                Image4th.sprite = macho;   
            }
            if(PlayerPrefs.GetInt(player) == EEPY)
            {
                Image4th.enabled = true;
                Image4th.color = Color.white;
                Image4th.sprite = eepy;
            }
            if(PlayerPrefs.GetInt(player) == RUUKI)
            {
                Image4th.enabled = true;
                Image4th.color = Color.white;
                Image4th.sprite = ruuki;
            }
            if(PlayerPrefs.GetInt(player) == BILLI)
            {
                Image4th.enabled = true;
                Image4th.color = Color.white;
                Image4th.sprite = billi;
            }
        }
        
    }
}
