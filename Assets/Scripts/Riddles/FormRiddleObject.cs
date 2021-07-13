using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Form
{
    Sphere,
    Cube,
    Cylinder
}

public enum FormColor
{
    Red,
    Blue
}

public class FormRiddleObject : MonoBehaviour
{
    [System.NonSerialized]
    public bool isCorrect = false;
    [Tooltip("hier ist ein Tooltip")]
    public Form form;
    public Material redMaterial;
    public Material blueMaterial;

    private FormRIddle formRiddle;

    private FormColor color;


    // Start is called before the first frame update
    void Awake()
    {
        

        int colorIndex = Random.Range(0, 2);

        if(colorIndex == 0)
        {
            // red color
            color = FormColor.Red;
            GetComponent<Renderer>().material.color = redMaterial.color;
        }
        else
        {
            // blue color
            color = FormColor.Blue;
            GetComponent<Renderer>().material.color = blueMaterial.color;
        }
    }

    private void Start()
    {
        formRiddle = GetComponentInParent<FormRIddle>();// FindObjectOfType<FormRIddle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " has collision with " + other.gameObject.name);
        formRiddle.Solved(isCorrect);
    }

    public FormColor GetFormColor()
    {
        return color;
    }

}
