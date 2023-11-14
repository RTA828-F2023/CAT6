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
    public Sprite Billi;

    public Image Image1st;
    public Image Image2nd;
    public Image Image3rd;
    public Image Image4th;

    public TextMeshProUGUI[] scoresText;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    public PointSystemController pointSystem;

    // Start is called before the first frame update
    void Start()
    {
        matchScoresUI.SetActive(false);
        //matchScoresUI.SetActive(false);
        Image1st.color = Color.white;
        Image2nd.color = Color.white;
        Image3rd.color = Color.white;
        Image4th.color = Color.white;

        scoresText[0].text = "";
        scoresText[1].text = "";
        scoresText[2].text = "";
        scoresText[3].text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //get scores of players
    void getAllScores()
    {
        //add each player to dictionary and say score

        if(PlayerPrefs.GetInt("p1") != 0 )
        {
            playerScores.Add("P1", pointSystem.GetPlayerScore(PlayerType.One));
        }
        if(PlayerPrefs.GetInt("p2") != 0 )
        {
            playerScores.Add("P2", pointSystem.GetPlayerScore(PlayerType.Two));
        }
        if(PlayerPrefs.GetInt("p3") != 0 )
        {
            playerScores.Add("P3", pointSystem.GetPlayerScore(PlayerType.Three));
        }
        if(PlayerPrefs.GetInt("p4") != 0 )
        {
            playerScores.Add("P4", pointSystem.GetPlayerScore(PlayerType.Four));
        }

        //sort by smallest to highest score
        playerScores.OrderByDescending(key => key.Value);
    }

    public void DrawScores()
    {
        getAllScores();

        int i = 0;

        foreach ( KeyValuePair<string, int> player in playerScores)
        {
            scoresText[i].text = player.Value.ToString();
            i++;
        }
    }
}
