using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameInterfaceController : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] private GameObject displaySprite;
    [SerializeField] private GameObject playerSprite;
    
    [Header("ColourUI")]
    [SerializeField] private GameObject displayFrame;
    [SerializeField] private GameObject[] heartIcons;

    //Coloured Sprites
    [Header("Red")]
    [SerializeField] private Sprite redFrame;
    [SerializeField] private Sprite redHeart;
    
    [Header("Purple")]
    [SerializeField] private Sprite purpleFrame;
    [SerializeField] private Sprite purpleHeart;
    
    [Header("Blue")]
    [SerializeField] private Sprite blueFrame;
    [SerializeField] private Sprite blueHeart;
    
    [Header("Green")]
    [SerializeField] private Sprite greenFrame;
    [SerializeField] private Sprite greenHeart;
    
    [Header("White")]
    [SerializeField] private Sprite whiteFrame;
    [SerializeField] private Sprite whiteHeart;

    private Sprite _newSprite;
    //Change character icon
    public void SetDisplay()
    {
        if (playerSprite.activeSelf)
        {
            //Find New Sprite
            if (playerSprite != null)
            {
                var spriteObject = playerSprite.transform.Find("Sprite").gameObject;
                _newSprite = spriteObject.transform.GetComponent<SpriteRenderer>().sprite;
            }

            //Assign New Sprite
            if (displaySprite != null)
            {
                var image = displaySprite.GetComponent<Image>();
                if (image != null && _newSprite != null)
                {
                    image.sprite = _newSprite;
                }
            }
            SetFrameAndHealthColour();
        }
        else 
        {
            gameObject.SetActive(false);
        }

    }

    private void SetFrameAndHealthColour() 
    {
        var playerChosen = PlayerPrefs.GetInt("p" + ((int)playerType + 1));
        switch (playerChosen) 
        {
            //Lell0
            case 1:
                displayFrame.GetComponent<Image>().sprite = blueFrame;
                foreach (var heart in heartIcons) 
                {
                    heart.GetComponent<Image>().sprite = blueHeart;
                }
                break;
            //Macho
            case 2:
                displayFrame.GetComponent<Image>().sprite = redFrame;
                foreach (var heart in heartIcons)
                {
                    heart.GetComponent<Image>().sprite = redHeart;
                }
                break;
            //Eepy
            case 3:
                displayFrame.GetComponent<Image>().sprite = purpleFrame;
                foreach (var heart in heartIcons)
                {
                    heart.GetComponent<Image>().sprite = purpleHeart;
                }
                break;
            //Ruuki
            case 4:
                displayFrame.GetComponent<Image>().sprite = greenFrame;
                foreach (var heart in heartIcons)
                {
                    heart.GetComponent<Image>().sprite = greenHeart;
                }
                break;
            //Billi
            case 5:
                displayFrame.GetComponent<Image>().sprite = whiteFrame;
                foreach (var heart in heartIcons)
                {
                    heart.GetComponent<Image>().sprite = whiteHeart;
                }
                break;

        }
    
    }

}
