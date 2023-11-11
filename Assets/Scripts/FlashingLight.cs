using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FlashingLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Light2D redLight;
    private float timer = 0f;
    private float duration = 2f;
    void Start()
    {
        redLight = gameObject.transform.Find("Light2D").GetComponent<Light2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration) 
        {
            timer = 0f;
        }

        if (redLight != null) 
        {
            redLight.intensity = Mathf.Lerp(0, 10, (Mathf.Sin(timer/duration * 2 * Mathf.PI) + 1) / 2);
        }
        
    }
}
