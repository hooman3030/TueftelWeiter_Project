using UnityEngine;

public class Target : MonoBehaviour
{
    public static int shapePoints;

    public static int colorPoints;

    private int allPoints;
    private Material mat;


    // calculates points
    public void Start()
    {
        // colorPoints
        mat = GetComponent<Renderer>().sharedMaterial;

        if(mat.name == "HologramBlue")
        {
            colorPoints = 5;
        } 
        else if(mat.name == "HologramGreen")
        {
            colorPoints = 10;
        }
        else if(mat.name == "HologramOrange")
        {
            colorPoints = 15;
        }
        else
        {
            colorPoints = 0;
        }

        Debug.Log(name);
        // shapePoints
        if(name == "Target1(Clone)")
        {
            shapePoints = 5;
        }
        else if(name == "Target2(Clone)")
        {
            shapePoints = 10;
        }
        else if (name == "Target3(Clone)")
        {
            shapePoints = 15;
        }

        allPoints = shapePoints + colorPoints;
    }

    // adds score, destroys object
    public void getShot()
    {
        ScoreManager.score += allPoints;
        Destroy(gameObject);
    }
}
