using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavesController : MonoBehaviour {
    GameObject[] enemies;
    public int enemyCount = 5;
    public int waves = 0; 
    public TMP_Text enemyCountText;
    public TMP_Text wavesCountText;
    public WavesTimer WavesTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // enemyCountText.text = "Enemies: " + enemies.Length.ToString();

        if (enemies.Length <= 0 && !(WavesTimer.TimerOn)) {
            waves += 1;
            //WavesTimer.TimerOn = true;
        }

        // wavesCountText.text = "Waves: " + waves;
    }
}