using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameInterfaceController : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] private GameObject displaySprite;
    [SerializeField] private GameObject playerSprite;

    [SerializeField] Sprite _newSprite;
    // Start is called before the first frame update
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
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }

}
