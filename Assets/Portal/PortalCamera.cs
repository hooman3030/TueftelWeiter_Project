using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{    
    public Transform playerCamera;
    public Transform vDisplay;
    public Transform otherVDisplay;


    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetVDisplay = playerCamera.position - otherVDisplay.position;
        transform.position = vDisplay.position + playerOffsetVDisplay;
    }
}
