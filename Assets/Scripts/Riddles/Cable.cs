using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

namespace CableRiddle
{
    /// <summary>
    /// Enums for all colors a cable can have
    /// </summary>
    public enum CableColor
    {
        Blue,
        Red,
        Yellow,
        Green
    }

    [RequireComponent(typeof(XRSimpleInteractable))]
    public class Cable : MonoBehaviour
    {
        // The gameobject of the cut cable that is displayed after an intact cable is cut
        public GameObject cutCable;

        // The mesh renderer of the current gameobject to e.g. set its visibility or get its materials
        private Renderer meshRenderer;

        // The mesh renderer of the corresponding cut cable to e.g. set its visibility or get its materials
        private Renderer cutCableMeshRenderer;

        // Reference to the cable riddle module
        private CableRiddle riddleModule;

        // All colors a cable can have
        private Color CABLE_BLUE = new Color(0.3617538f, 0.5389251f, 0.8584906f);
        private Color CABLE_RED = new Color(0.9433962f, 0.2720885f, 0.2017324f);
        private Color CABLE_YELLOW = new Color(1, 0.8186345f, 0.3647798f);
        private Color CABLE_GREEN = new Color(0.304082f, 0.7735849f, 0.305768f);

        // The own cable color
        private CableColor cableColor;

        // Whether this is the cable that has to be cut to solve the riddle correctly
        private bool isCorrect = false;

        // Whether this cable got cut
        private bool isCut = false;

        // Start is called before the first frame update
        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            cutCableMeshRenderer = cutCable.GetComponent<MeshRenderer>();

            // Randomly assign a color to the cable
            List<Color> colors = new List<Color>();
            colors.Add(CABLE_BLUE);
            colors.Add(CABLE_RED);
            colors.Add(CABLE_YELLOW);
            colors.Add(CABLE_GREEN);
            SetColor(colors.ElementAt(Random.Range(0, colors.Count)));

            riddleModule = GetComponentInParent<CableRiddle>();

            GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnClick);
        }

        /// <summary>
        /// Color the material of the cable and store the color as an enum.
        /// </summary>
        /// <param name="color">The color to set</param>
        private void SetColor(Color color)
        {
            meshRenderer.material.color = color;
            cutCableMeshRenderer.material.color = color;

            if (color.Equals(CABLE_BLUE))
            {
                cableColor = CableColor.Blue;
            } 
            else if (color.Equals(CABLE_GREEN))
            {
                cableColor = CableColor.Green;
            }
            else if (color.Equals(CABLE_RED))
            {
                cableColor = CableColor.Red;
            }
            else if (color.Equals(CABLE_YELLOW))
            {
                cableColor = CableColor.Yellow;
            }
        }

        /// <summary>
        /// A function that is called when the cable is clicked on. This is assigned to XR Click Interactable in the editor.
        /// </summary>
        public void OnClick(SelectEnterEventArgs args)
        {
            Cut();
            riddleModule.Solve(isCorrect);
        }

        /// <summary>
        /// Set the visibility of the cables to visually show the cut.
        /// </summary>
        private void Cut()
        {
            cutCableMeshRenderer.enabled = true;
            meshRenderer.enabled = false;
        }

        /// <summary>
        /// Return the cable color as enum.
        /// </summary>
        /// <returns>The cable color as enum</returns>
        public CableColor GetColor()
        {
            return cableColor;
        }

        public void SetCorrect(bool val)
        {
            isCorrect = val;
        }

        public bool GetIsCut()
        {
            return isCut;
        }
    }
}