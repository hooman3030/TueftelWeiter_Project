using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CableRiddle;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    [Tooltip("Reference to the own LED. If null, tries to set automatically.")]
    public LED ownLED;

    [Tooltip("The value that is set when clicking this LED, should ideally be the same as the value in the corresponding display.")]
    public int value;

    [Tooltip("List of all other LEDs that are disabled when clicking on the own LED.")]
    public List<LED> otherLEDs;

    [Tooltip("The display with the description of the setting.")]
    public Text display;

    // The lever that should receive the setting as key-value pair for the dictionary that stores the settings.
    private Lever lever;


    // Start is called before the first frame update
    void Start()
    {
        lever = FindObjectOfType<Lever>();

        // If not manually set, try to find the own LED
        if (!ownLED)
        {
            ownLED = GetComponentInChildren<LED>();
        }

        ownLED.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnClick);

        lever.onLeverPushed.AddListener(ResetCheckBoxes);
    }

    /// <summary>
    /// Called every time the LED of a checkbox is clicked. This is set in the editor.
    /// </summary>
    public void OnClick(SelectEnterEventArgs args)
    {
        Debug.Log("checkbox click");
        // Send the corresponding key and value to the lever to store it
        lever.AddSetting(display.text, value.ToString());

        // Make sure that only the current LED of the checkbox group is active
        ResetCheckBoxes();
        ownLED.SetActiveColor();
    }

    /// <summary>
    /// Set all LEDs to inactive.
    /// </summary>
    private void ResetCheckBoxes()
    {
        foreach(LED button in otherLEDs)
        {
            button.ResetColor();
        }
    }
}
