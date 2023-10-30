//Author: Victoria Ouryvski, Duong Vu
//Project Name: CAT6
//File Name: SelectController.cs
//Creation Date: Oct 24, 2023
//Modified Date: Oct 30, 2023
//Description: File that draws characters that are selected to player that selects them
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterDisabler : MonoBehaviour
{

    //make variables of each player
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    //make variables that hold all character animations
    public AnimatorOverrideController lelloAnimator;
    public AnimatorOverrideController machoAnimator;
    public AnimatorOverrideController eepyAnimator;
    public AnimatorOverrideController ruukiAnimator;
    public AnimatorOverrideController billiAnimator;

    //make constants for all characters
    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    //make constants for all players
    private const int P1 = 0;
    private const int P2 = 1;
    private const int P3 = 2;
    private const int P4 = 3;

    private void Start()
    {
        //get character selection information
        SetChar(P1, PlayerPrefs.GetInt("p1"));
        SetChar(P2, PlayerPrefs.GetInt("p2"));
        SetChar(P3, PlayerPrefs.GetInt("p3"));
        SetChar(P4, PlayerPrefs.GetInt("p4"));
    }

    //pre: get player, and get players character
    //post: none
    //desc: set players selected character on screen
    private void SetChar(int p, int character)
    {
        //if player is p1
        if (p == P1)
        {
            //switch depending on choosen character
            switch (character)
            {
                case 0:
                    //deactivate character
                    p1.SetActive(false);
                    break;

                case 1:
                    //set player 1 to lello
                    p1.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    //set player 1 to macho
                    p1.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    //set player 1 to eepy
                    p1.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    //set player 1 to ruuki
                    p1.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    //set player 1 to billi
                    p1.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        //if player is p2
        else if (p == P2)
        {
            //switch depending on choosen character
            switch (character)
            {
                case 0:
                    //deactivate character
                    p2.SetActive(false);
                    break;

                case 1:
                    //set player 2 to lello
                    p2.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    //set player 2 to macho
                    p2.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    //set player 2 to eepy
                    p2.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    //set player 2 to ruuki
                    p2.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    //set player 2 to billi
                    p2.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        //if player is p3
        else if (p == P3)
        {
            //switch depending on choosen character
            switch (character)
            {
                case 0:
                    //deactivate character
                    p3.SetActive(false);
                    break;

                case 1:
                    //set player 3 to lello
                    p3.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    //set player 3 to macho
                    p3.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    //set player 3 to eepy
                    p3.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    //set player 3 to ruuki
                    p3.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    //set player 3 to billi
                    p3.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        //if player is p4
        else if (p == P4)
        {
            //switch depending on choosen character
            switch (character)
            {
                case 0:
                    //deactivate character
                    p4.SetActive(false);
                    break;

                case 1:
                    //set player 4 to lello
                    p4.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    //set player 4 to macho
                    p4.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    //set player 4 to eepy 
                    p4.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    //set player 4 to ruuki
                    p4.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    //set player 4 to billi
                    p4.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
    }
}
