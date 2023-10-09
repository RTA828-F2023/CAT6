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
    // Start is called before the first frame update
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        var playerssorted = players.OrderBy(x => x.transform.GetComponent<Player>().type).ToList();

        foreach (GameObject player in players) 
        {
            var playerType = player.transform.GetComponent<Player>().type;
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

}
