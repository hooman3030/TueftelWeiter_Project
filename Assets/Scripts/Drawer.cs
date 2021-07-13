using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    private bool isOpen = false;

    public Material MledOff;
    public Material MledOn;
    public Renderer rend;

    void Start() 
    {
        rend.enabled = true;
        rend.sharedMaterial = MledOff;
    }

    public void playAnimation() 
    {
        isOpen = !isOpen;
        GetComponent<Animator>().SetBool("open", isOpen);

        if(rend.sharedMaterial == MledOn)
        {
            rend.sharedMaterial = MledOff;
        } else
        {
            rend.sharedMaterial = MledOn;
        }
        
    }
}
