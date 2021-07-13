using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FormRIddle : Riddle
{
    private List<FormRiddleObject> formRiddleObjects;
    private int redCount = 0;
    private bool hasSphere = false;

    public override void EvaluateSettings(Dictionary<string, string> settings)
    {
        foreach(KeyValuePair<string, string> setting in settings)
        {
            Debug.Log(setting.Key + " : " + setting.Value);
        }

        string getSphere = "";
        bool submittedSphere = settings.TryGetValue("Kugel?", out getSphere);

        string redValue = "";
        bool submittedRedCount = settings.TryGetValue("Anzahl rote", out redValue);

        if (submittedSphere && getSphere.Equals("1"))
        {
            if (submittedRedCount && redValue.Equals("1"))
            {
                // sphere
                solution.text = "Kugel";
            }
            else if (submittedRedCount && redValue.Equals("2"))
            {
                // cylinder
                solution.text = "Cylinder";
            }
            else
            {
                // cube
                solution.text = "Würfel";
            }
        }
        else if (submittedSphere && getSphere.Equals("0"))
        {
            if (submittedRedCount && (redValue.Equals("1") || redValue.Equals("2")))
            {
                // cylinder
                solution.text = "Cylinder";
            }
            else
            {
                // cube
                solution.text = "Würfel";
            }
        }
        else
        {
            solution.text = "Info fehlt";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        formRiddleObjects = GetComponentsInChildren<FormRiddleObject>().ToList();

        foreach(FormRiddleObject formObject in formRiddleObjects)
        {
            if (formObject.form.Equals(Form.Sphere))
            {
                hasSphere = Random.Range(0, 2) == 1;
                formObject.gameObject.SetActive(hasSphere);
            }

            if(formObject.GetFormColor().Equals(FormColor.Red) && formObject.isActiveAndEnabled)
            {
                redCount++;
            }
        }

        MarkAsCorrect();
    }

    private void MarkAsCorrect()
    {
        if (hasSphere)
        {
            if(redCount == 1)
            {
                // sphere
                GetObjectByForm(Form.Sphere).isCorrect = true;
            }
            else if (redCount == 2)
            {
                // cylinder
                GetObjectByForm(Form.Cylinder).isCorrect = true;
            }
            else
            {
                // cube
                GetObjectByForm(Form.Cube).isCorrect = true;
            }
        }
        else
        {
            if (redCount == 1 || redCount == 2)
            {
                // cylinder
                GetObjectByForm(Form.Cylinder).isCorrect = true;
            }
            else
            {
                // cube
                GetObjectByForm(Form.Cube).isCorrect = true;
            }
        }
    }

    private FormRiddleObject GetObjectByForm(Form form)
    {
        foreach(FormRiddleObject formObject in formRiddleObjects)
        {
            if (formObject.form.Equals(form))
            {
                return formObject;
            }
        }

        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Solved(bool isCorrect)
    {
        if (isCorrect)
        {
            solution.text = "Richtig";
        }
        else
        {
            solution.text = "Falsch";
        }

        OnSolved.Invoke();
    }
}
