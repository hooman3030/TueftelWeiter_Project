using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour
{
    public Color activeColor;

    private Material ledMaterial;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        ledMaterial = GetComponent<Renderer>().materials[1];
        defaultColor = ledMaterial.color;
    }

    public void SetActiveColor()
    {
        ledMaterial.color = activeColor;
    }

    public void ResetColor()
    {
        ledMaterial.color = defaultColor;
    }
}
