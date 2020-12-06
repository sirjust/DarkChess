using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{

    bool checkerOn, checkerOff;
    public float fadeSpeed = 1;

    public Material original;
    Renderer rend;

    public Projector kingShadow;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = original;
    }

    void Update()
    {
        if(checkerOn)
        {
            Color objectColor = rend.material.color;
            float fadeAmountr = objectColor.r + (fadeSpeed * Time.deltaTime);
            float fadeAmountg = objectColor.g + (fadeSpeed * Time.deltaTime);
            float fadeAmountb = objectColor.b + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(fadeAmountr, fadeAmountg, fadeAmountb, objectColor.a);
            this.GetComponent<Renderer>().material.color = objectColor;

            if(objectColor.r >= 1)
            {
                checkerOn = false;
            }
        }

        if (checkerOff)
        {
            Color objectColor = rend.material.color;
            float fadeAmountr = objectColor.r - (fadeSpeed * Time.deltaTime);
            float fadeAmountg = objectColor.g - (fadeSpeed * Time.deltaTime);
            float fadeAmountb = objectColor.b - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(fadeAmountr, fadeAmountg, fadeAmountb, objectColor.a);
            this.GetComponent<Renderer>().material.color = objectColor;

            if (objectColor.r <= 0)
            {
                checkerOff = false;
            }
        }
    }

    public void TurnCheckerOff()
    {
        checkerOn = true;
    }

    public void TurnCheckerOn()
    {
        checkerOff = true;
    }

    public void TurnShadowOff()
    {
        kingShadow.enabled = false;
    }

    public void TurnShadowOn()
    {
        kingShadow.enabled = true;
    }

}
