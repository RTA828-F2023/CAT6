using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointSystemController : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField] private TextMeshProUGUI[] scoreTextObjects;

    Dictionary<PlayerType,TextMeshProUGUI> scoreTexts = new Dictionary<PlayerType, TextMeshProUGUI>();
    Dictionary<PlayerType, int> playerScores = new Dictionary<PlayerType, int>();
    // Start is called before the first frame update
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        for ( int i = 0; i < players.Length; i++) 
        {
            var playerType = players[i].transform.GetComponent<Player>().type;
            playerScores[playerType] = 0;

            scoreTextObjects[i].enabled = true;
            scoreTextObjects[i].text = "Score: " + playerScores[playerType];

            scoreTexts[playerType] = scoreTextObjects[i];
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

}
