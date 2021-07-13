using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class Lever : MonoBehaviour
{
    //[Tooltip("Reference to the currently active riddle module")]
    private Riddle riddle;

    [Tooltip("Event that is invoked when the lever is pulled down")]
    public UnityEvent onLeverPulled = new UnityEvent();

    [Tooltip("Event that is invoked when the lever is pussed up")]
    public UnityEvent onLeverPushed = new UnityEvent();

    // The initial rotation of the lever to reset it later
    private Quaternion initRotation;

    // The current state of the lever
    private bool isLevered = false;

    // Dictionary with all received settings. The key is the text in the display next to the setting, the value is the setting itself.
    private Dictionary<string, string> settings = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        initRotation = gameObject.transform.rotation;

        if (!riddle)
        {
            riddle = FindObjectOfType<Riddle>();
        }

        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnClick);
    }

    /// <summary>
    /// This is called every time the user clicks on the lever.
    /// </summary>
    public void OnClick(SelectEnterEventArgs arg)
    {
        if (!isLevered)
        {
            // Lever is pulled and the settings have to be evaluated
            gameObject.transform.Rotate(new Vector3(90, 0, 0));

            PrintSettings(); // For debug purposes

            riddle.EvaluateSettings(settings);
            isLevered = true;
            
            onLeverPulled.Invoke();
        }
        else
        {
            // Lever is pushed and the settings have to be cleared
            isLevered = false;
            gameObject.transform.rotation = initRotation;

            settings.Clear();
            riddle.GetSolution().text = "...";

            onLeverPushed.Invoke();
        }
    }

    /// <summary>
    /// Add a setting when it is not in the dictionary yet, otherwise update the value.
    /// </summary>
    /// <param name="key">The key of the setting (the text in the corresponding display)</param>
    /// <param name="value">The value of the setting</param>
    public void AddSetting(string key, string value)
    {
        settings[key] = value;
    }

    /// <summary>
    /// Debug function for printing the final settings
    /// </summary>
    private void PrintSettings()
    {        
        foreach(KeyValuePair<string, string> pair in settings)
        {
            Debug.Log(pair.Key + " : " + pair.Value);
        }
    }

    public Dictionary<string, string> GetSettings()
    {
        return settings;
    }

    public Riddle GetRiddle ()
    {
        if (!riddle) 
        {
            riddle = FindObjectOfType<Riddle>();
        }
        return riddle;
    }
}
