using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;

public class WavesTimer : MonoBehaviour {

    public float TimeLeft = 10;
    public bool TimerOn = false;
    public TMP_Text TimerText;

    public GameObject[] enemies;

    public EnemySpawn enemySpawn;

    // Start is called before the first frame update
    void Start() {
        //TimerOn = true;
    }

    // Update is called once per frame
    void Update() {

        if (TimerOn) {
            if (TimeLeft > 0) {
                TimerText.enabled = true;
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else {
                TimerText.enabled = false;
                TimeLeft = 0;
                TimerOn = false;
                updateTimer(TimeLeft);
                enemySpawn.spawnEnemies();
                TimeLeft = 10;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = "Wave starts in: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static implicit operator WavesTimer(bool v)
    {
        throw new NotImplementedException();
    }
}
