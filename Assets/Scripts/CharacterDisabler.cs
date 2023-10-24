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
    

    private const int LELLO = 1;
    private const int MACHO = 2;
    private const int EEPY = 3;
    private const int RUUKI = 4;
    private const int BILLI = 5;

    private const int P1 = 0;
    private const int P2 = 1;
    private const int P3 = 2;
    private const int P4 = 3;

    // Start is called before the first frame update
    void Start()
    {
        
        //PlayerPrefs.GetInt("p2");
        //PlayerPrefs.GetInt("p3");
        //PlayerPrefs.GetInt("p4");

        SetChar(P1, PlayerPrefs.GetInt("p1"));
        SetChar(P2, PlayerPrefs.GetInt("p2"));
        SetChar(P3, PlayerPrefs.GetInt("p3"));
        SetChar(P4, PlayerPrefs.GetInt("p4"));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetChar(int p, int character)
    {
        if(character == 0 && p == P1)
        {
            p1.SetActive(false);
        }
        if(character == 0 && p == P2)
        {
            p2.SetActive(false);
        }
        if(character == 0 && p == P3)
        {
            p3.SetActive(false);
        }
        if(character == 0 && p == P4)
        {
            p4.SetActive(false);
        }

        if(character == 1 && p == P1)
        {
            p1.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
        }
        if(character == 2 && p == P1)
        {
            p1.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
        }

         if(character == 1 && p == P2)
        {
            p2.GetComponent<Animator>().runtimeAnimatorController = lelloAnimator;
        }
        if(character == 2 && p == P2)
        {
            p2.GetComponent<Animator>().runtimeAnimatorController = machoAnimator;
        }
    }
}
