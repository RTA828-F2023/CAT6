using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;

public class Leaderboard : MonoBehaviour
{

    public TextMeshProUGUI[] scoresText;
    // Start is called before the first frame update
    void Start()
    {
        //If no leaderboard file exists, create it
        String LeaderboardFileName = "InkywayLeaderboard.txt";
        if (!File.Exists(LeaderboardFileName))
        {
            File.Create(LeaderboardFileName).Close();
        }

        // Read the contents of the leaderboard file
        List<string> leaderboardLines = File.ReadAllLines(LeaderboardFileName).ToList();

        // Top 10 lines of the leaderboard - or fewer, if there are not yet 10 scores.
        int numLines = 10;
        if (leaderboardLines.Count < numLines) numLines = leaderboardLines.Count;
        for (int i = 0; i < numLines; i++)
        {
            int existingScore = int.Parse(leaderboardLines[i]);
            scoresText[i].text = existingScore.ToString();
        }
        //Set the rest of the lines to empty strings
        if (numLines < 10)
        {
            for (int i = numLines; i < 10; i++)
            {
                scoresText[i].text = "";
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
