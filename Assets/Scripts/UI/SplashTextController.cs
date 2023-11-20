using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SplashText : MonoBehaviour
{
    [SerializeField] private TextAsset _splashTextFile;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI shadowText;

    private string _splashText;
    private string[] _splashTextLines;

    void Start() 
    {
        
        _splashText = _splashTextFile.text;
        _splashTextLines = _splashText.Split('\n');
        chooseRandomLine();

    }

    void chooseRandomLine() 
    {
        var random = new System.Random();

        int splashTextSize = _splashTextLines.Length;
        int lineNumber = random.Next(splashTextSize);
        if (mainText != null)
        {
            mainText.text = _splashTextLines[lineNumber];
        }

        if (shadowText != null)
        {
            shadowText.text = _splashTextLines[lineNumber];
        }
    }

}
