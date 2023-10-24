using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterDisabler : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public AnimatorOverrideController lelloAnimator;
    public AnimatorOverrideController machoAnimator;
    public AnimatorOverrideController eepyAnimator;
    public AnimatorOverrideController ruukiAnimator;
    public AnimatorOverrideController billiAnimator;

    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    private const int P1 = 0;
    private const int P2 = 1;
    private const int P3 = 2;
    private const int P4 = 3;

    private void Start()
    {
        SetChar(P1, PlayerPrefs.GetInt("p1"));
        SetChar(P2, PlayerPrefs.GetInt("p2"));
        SetChar(P3, PlayerPrefs.GetInt("p3"));
        SetChar(P4, PlayerPrefs.GetInt("p4"));

    }

    private void SetChar(int p, int character)
    {
        if (p == P1)
        {
            switch (character)
            {
                case 0:
                    p1.SetActive(false);
                    break;

                case 1:
                    p1.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    p1.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    p1.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    p1.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    p1.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        else if (p == P2)
        {
            switch (character)
            {
                case 0:
                    p2.SetActive(false);
                    break;

                case 1:
                    p2.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    p2.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    p2.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    p2.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    p2.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        else if (p == P3)
        {
            switch (character)
            {
                case 0:
                    p3.SetActive(false);
                    break;

                case 1:
                    p3.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    p3.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    p3.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    p3.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    p3.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
        else if (p == P4)
        {
            switch (character)
            {
                case 0:
                    p4.SetActive(false);
                    break;

                case 1:
                    p4.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
                    break;

                case 2:
                    p4.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
                    break;

                case 3:
                    p4.GetComponent<Animator>().runtimeAnimatorController = eepyAnimator;
                    break;

                case 4:
                    p4.GetComponent<Animator>().runtimeAnimatorController = ruukiAnimator;
                    break;

                case 5:
                    p4.GetComponent<Animator>().runtimeAnimatorController = billiAnimator;
                    break;

                default:
                    break;
            }
        }
    }
}
