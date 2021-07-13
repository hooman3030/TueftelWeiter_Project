using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class Button : MonoBehaviour
{
    // Enum of the directions a button can face to
    public enum Directions
    {
        UP,
        DOWN
    }

    [Tooltip("The direction of this button")]
    public Directions direction = Directions.UP;

    [Tooltip("The value displayed for this button")]
    public Text value;

    [Tooltip("The value that is displayed at the start")]
    public int startValue;

    [Tooltip("The maximal value of this button")]
    public int maxValue;

    [Tooltip("The minimal value of this button")]
    public int minValue;

    [Tooltip("The display corresponding to this button, showing the name of the setting")]
    public Text display;

    // Stores the current value after the button is pressed
    private int currentValue;

    // The lever that should receive the setting as key-value pair for the dictionary that stores the settings.
    private Lever lever;

    // Start is called before the first frame update
    void Start()
    {
        value.text = startValue.ToString();
        lever = FindObjectOfType<Lever>();

        lever.onLeverPushed.AddListener(ResetValue);
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnClick);
    }

    /// <summary>
    /// Called every time the button is clicked. This is set in the editor.
    /// </summary>
    public void OnClick(SelectEnterEventArgs args)
    {
        // increment or decrement the current value, based on the direction of the button
        currentValue = Int32.Parse(value.text);
        switch (direction)
        {
            case Directions.UP:
                if (currentValue < maxValue) 
                {
                    currentValue++;
                }
                break;
            case Directions.DOWN:
                if (currentValue > minValue)
                {
                    currentValue--;
                }
                break;
        }

        value.text = currentValue.ToString();

        lever.AddSetting(display.text, value.text);
    }

    /// <summary>
    /// Resets the value to the start value.
    /// </summary>
    private void ResetValue()
    {
        currentValue = startValue;
        value.text = startValue.ToString();
    }
}
