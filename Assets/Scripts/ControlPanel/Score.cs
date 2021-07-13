using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    // Reference to the text field that displays the score number
    public Text scoreText;

    // Start time of the riddle to compute how long it took to solve it
    private float startTime;

    // The lever that has all the settings inserted in the control panel.
    private Lever lever;

    // The score that will be displayed to the user
    private double score;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lever = FindObjectOfType<Lever>();

        lever.GetRiddle().OnSolved.AddListener(ComputeScore);
    }

    /// <summary>
    /// Computes the score and displays it to the user.
    /// </summary>
    public void ComputeScore()
    {

        // The time it took to solve the riddle
        float time = Time.time - startTime;

        // The amount of unnecessary extra informoation
        int unnecessaryInfo = lever.GetSettings().Count - lever.GetRiddle().GetCorrectInfoAmount();

        // Make sure that the information taken into account are always non-negative
        int infoScore = unnecessaryInfo > 0 ? unnecessaryInfo : 0;

        // Compute the score by taking the solving-time and the amount of unnecessary information into account
        score = 1 / ((1 + infoScore) * time) * 1000; // Multiply by 1000 to make the number more user-friendly

        // Set the score text accordingly
        scoreText.text = Math.Round(score).ToString(); // Round to make the number more user-friendly
    }

}
