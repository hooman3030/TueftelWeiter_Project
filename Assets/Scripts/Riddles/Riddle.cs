using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class Riddle : MonoBehaviour
{
    // The textfield that displays the solution of the riddle
    protected Text solution;

    // Event that is invoked when the riddle is solved (for an example, see Solved(bool correct) method in CableRiddle).
    // For example, score is listening to this to compute the score.
    public UnityEvent OnSolved = new UnityEvent();

    // Amount of information that need to be given to solve the riddle
    protected int correctInfoAmount;

    /// <summary>
    /// Evaluates the information that are set in the control panel to display a solution for the riddle.
    /// </summary>
    /// <param name="settings">dictionary with the given information (key is the text in the display, value is the value of the setting)</param>
    public abstract void EvaluateSettings(Dictionary<string, string> settings);


    protected void Start()
    {
        solution = GameObject.FindGameObjectWithTag("SolutionDisplay").GetComponent<Text>();

        // Find riddle box and add it as parent for correct rotation behavior

        //riddleBox = GameObject.FindGameObjectWithTag("RiddleBox");
    }

    protected void Update()
    {
        
    }

    /// <summary>
    /// Returns the minimum amount of information necessary to solve the riddle.
    /// </summary>
    /// <returns></returns>
    public int GetCorrectInfoAmount()
    {
        return correctInfoAmount;
    }

    public Text GetSolution()
    {
        return solution;
    }
}
