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
    [SerializeField] private GameObject[] CrownDisplayObjects;
    [SerializeField] private VertexGradient newColorGradient;

    Dictionary<PlayerType, TextMeshProUGUI> scoreTexts = new Dictionary<PlayerType, TextMeshProUGUI>();
    Dictionary<PlayerType, GameObject> crownHUDObjects = new Dictionary<PlayerType, GameObject>();
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
            crownHUDObjects[playerType] = CrownDisplayObjects[(int)playerType];

            scoreTexts[playerType].enabled = true;
            scoreTexts[playerType].text = "" + playerScores[playerType];
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<PlayerType, TextMeshProUGUI> kvp in scoreTexts)
        {
            var newScore = playerScores[kvp.Key];
            kvp.Value.text = "" + newScore;
        }
    }

    public void UpdatePlayerScore(PlayerType playerType, int score)
    {
        playerScores[playerType] += score;
    }

    public void DisplayBestPlayer()
    {
        var greatestValue = GetHighestScore();
        foreach (KeyValuePair<PlayerType, TextMeshProUGUI> score in scoreTexts)
        {
            if (scoreTexts[score.Key] != null)
            {
                if (playerScores[score.Key] == greatestValue)
                {
                    UpdateCrown(score.Key, true);
                    scoreTexts[score.Key].colorGradient = newColorGradient;
                    scoreTexts[score.Key].enableVertexGradient = true;
                }

                else
                {
                    UpdateCrown(score.Key, false);
                    scoreTexts[score.Key].enableVertexGradient = false;
                }
            }
        }
    }

    public int GetHighestScore()
    {
        var bestPlayer = playerScores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        return playerScores[bestPlayer];
    }

    public int GetPlayerScore(PlayerType playerType) 
    {
        var score = playerScores[playerType];
        if (score != null) 
        {
            return score;
        }
        return 0;
    }

    private void UpdateCrown(PlayerType playerType, bool condition)
    {
        if (playerObjects[playerType] != null)
        {
            GameObject crownObject = playerObjects[playerType].transform.Find("Crown").gameObject;
            if (crownObject != null)
            {
                crownObject.SetActive(condition);
                crownHUDObjects[playerType].SetActive(condition);
            }
        }
    }
}