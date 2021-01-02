using UnityEngine;
using System.Collections.Generic;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class LightFlicker : MonoBehaviour
{
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new  Light light;
    //public Light light1;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0.7f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1.3f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 35;

                                                                                         // Formerly for working with emission intensity
                                                                                         // public Material emissionIntensity;


    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;
    //float lastSum1 = 0;

    //                                                                                  float elastSum = 0;


    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light>();
        }
        //if (light1 == null)
        //{
        //    light1 = GetComponent<Light>();
        //}

//                                                                  Needed for working with emission      
//                                                                       emissionIntensity.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (light == null)
            return;
        //if (light1 == null)
        //    return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        float newVal1 = Random.Range(minIntensity, maxIntensity);

        smoothQueue.Enqueue(newVal);
        lastSum += newVal;
        //smoothQueue.Enqueue(newVal1);
        //lastSum1 += newVal1;

        // elastSum += (newVal + 2.4f);
        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;
        //light1.intensity = lastSum1 / (float)smoothQueue.Count;


                                                                // Formerly to changed emission intensity
                                                                //        emissionIntensity.SetColor("_EmissionColor", new Color(0.8f, 0.675f, 0.38f, 1.0f) * (elastSum / (float)smoothQueue.Count));
    }

}