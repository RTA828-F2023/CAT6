using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointSystemController : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField] private TextMeshProUGUI[] scoreTextObjects;

    Dictionary<PlayerType,TextMeshProUGUI> scoreTexts = new Dictionary<PlayerType, TextMeshProUGUI>();
    Dictionary<PlayerType, int> playerScores = new Dictionary<PlayerType, int>();
    Dictionary<PlayerType, GameObject> playerObjects = new Dictionary<PlayerType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) 
        {
            var playerType = player.transform.GetComponent<Player>().type;
            playerObjects[playerType] = player;
            playerScores[playerType] = 0;

            scoreTexts[playerType] = scoreTextObjects[(int)playerType];

            scoreTexts[playerType].enabled = true;
            scoreTexts[playerType].text = "Score: " + playerScores[playerType];
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( KeyValuePair<PlayerType,TextMeshProUGUI> kvp in scoreTexts) 
        {
            var newScore = playerScores[kvp.Key];
            kvp.Value.text = "Score:" + newScore;
        }
    }

    public void UpdatePlayerScore(PlayerType playerType,int score) 
    {
        playerScores[playerType] += score;
    }

    public void DisplayBestPlayer()
    {
        //TODO: Show which player currently has the highest score
        //How do we show it? highlight biggest score (Edit scoreboard text?)
        //Display on player? (Edit the player or show/hide some element on player?)
        var bestPlayer = playerScores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        var greatestValue = GetHighestScore();

        foreach (KeyValuePair<PlayerType, TextMeshProUGUI> score in scoreTexts)
        {
            if (score.Key != bestPlayer)
            {
                UpdateCrown(score.Key, false);
                VertexGradient whiteGradient = scoreTexts[score.Key].colorGradient;
                whiteGradient.topLeft = Color.white;
                whiteGradient.topRight = Color.white;
                whiteGradient.bottomLeft = Color.white;
                whiteGradient.bottomRight = Color.white;
                scoreTexts[score.Key].colorGradient = whiteGradient;
            }
        }

        VertexGradient gradient = scoreTexts[bestPlayer].colorGradient;
        gradient.topLeft = new Color(0, 92, 200, 255);
        gradient.topRight = new Color(0, 92, 200, 255);
        gradient.bottomLeft = new Color(205, 200, 240, 255);
        gradient.bottomRight = new Color(205, 200, 240, 255);
        scoreTexts[bestPlayer].colorGradient = gradient;

        UpdateCrown(bestPlayer,true);
    }

    private int GetHighestScore() 
    {
        var bestPlayer = playerScores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        return playerScores[bestPlayer];
    }

    private void UpdateCrown(PlayerType playerType,bool condition)
    {
        GameObject crownObject = playerObjects[playerType].transform.Find("Crown").gameObject;
        if (crownObject != null) 
        {
            crownObject.SetActive(condition);
        }

    }
}
