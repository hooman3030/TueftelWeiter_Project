using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    public bool isOn = false;

    public Light powerLight;

    public Light tableLight;

    public Material MledOff;
    public Material MledOn;
    public Renderer rend;


    public void startProjector() 
    {
        isOn = !isOn;

        if(isOn )
        {
            powerLight.enabled = true;
            tableLight.intensity *= 0.3f;
            rend.sharedMaterial = MledOn;

        } else
        {
            powerLight.enabled = false;
            tableLight.intensity *= 1.7f;
            rend.sharedMaterial = MledOff;
        }
        
    }
}
