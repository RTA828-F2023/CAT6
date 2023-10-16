//Author: Victoria Ouryvski 
//Project Name: CAT6
//File Name: UIControlller.cs
//Creation Date: Oct 2, 2023
//Modified Date: Oct 10, 2023
//Description: Script to change scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    //get scene that will be changed to
    public string sceneName; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pre: none
    //post: none
    //desc: changes scnee to main menu
    public void ChangeToMenu()
    {
        //change scene to given scene
        SceneManager.LoadScene(sceneName);
    }
}
