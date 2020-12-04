using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{

    bool shadowOn, shadowOff;
    public float fadeSpeed = 1;

    public Material original;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = original;

    }

    void Update()
    {
        if(shadowOn)
        {
            Color objectColor = rend.material.color;
            float fadeAmountr = objectColor.r + (fadeSpeed * Time.deltaTime);
            float fadeAmountg = objectColor.g + (fadeSpeed * Time.deltaTime);
            float fadeAmountb = objectColor.b + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(fadeAmountr, fadeAmountg, fadeAmountb, objectColor.a);
            this.GetComponent<Renderer>().material.color = objectColor;

            if(objectColor.r >= 1)
            {
                shadowOn = false;
            }
        }

        if (shadowOff)
        {
            Color objectColor = rend.material.color;
            float fadeAmountr = objectColor.r - (fadeSpeed * Time.deltaTime);
            float fadeAmountg = objectColor.g - (fadeSpeed * Time.deltaTime);
            float fadeAmountb = objectColor.b - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(fadeAmountr, fadeAmountg, fadeAmountb, objectColor.a);
            this.GetComponent<Renderer>().material.color = objectColor;

            if (objectColor.r <= 0)
            {
                shadowOff = false;
            }
        }
    }

    public void TurnShadowOn()
    {
        shadowOn = true;
    }

    public void TurnShadowOff()
    {
        shadowOff = true;
    }

}
