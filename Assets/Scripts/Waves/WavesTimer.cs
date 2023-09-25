using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;

public class WavesTimer : MonoBehaviour {

    public float TimeLeft;
    public bool TimerOn = false;
    public TMP_Text TimerText;

    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start() {
        //TimerOn = true;
    }

    // Update is called once per frame
    void Update() {

/*         enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0) {
            TimerOn = false;
        } */

        if (TimerOn) {
            if (TimeLeft > 0) {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = "Wave starts in: " + string.Format("{0:00}:{1:00}", minutes, seconds) + TimerOn.ToString();
    }

    public static implicit operator WavesTimer(bool v)
    {
        throw new NotImplementedException();
    }
}
