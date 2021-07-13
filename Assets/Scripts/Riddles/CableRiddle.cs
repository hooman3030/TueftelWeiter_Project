using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace CableRiddle
{
    public class CableRiddle : Riddle
    {
        // Reference to the cat sticker on the back of the riddle
        public GameObject catSticker;

        // List of all cables in the scene (including inactive cables)
        private List<Cable> cables;


        // Riddle constraints
        private int cableCount = 0;
        private int redCount = 0;
        private int blueCount = 0;
        private int greenCount = 0;
        private int yellowCount = 0;
        private bool showCat = false;
        private int[] possibleCableAmount = { 3, 5 };

        // The keys for the settings (same as display text)
        private string cableCountKey = "Anzahl Kabel";
        private string catKey = "Katze?";
        private string redCountKey = "Anzahl rote";
        private string yellowCountKey = "Anzahl gelbe";
        private string greenCountKey = "Anzahl grüne";
        private string blueCountKey = "Anzahl blaue";


        // Start is called before the first frame update
        new void Start()
        {
            base.Start();

            // Set up the riddle

            cables = GetComponentsInChildren<Cable>().ToList<Cable>();

            // Randomly display 3 or 5 cables
            cableCount = possibleCableAmount[UnityEngine.Random.Range(0, 2)];
            List<Cable> cablesShuffled = cables.OrderBy(x => UnityEngine.Random.value).ToList(); // shuffle cables to randomly display the first x

            for(int i = 0; i < (cables.Count - cableCount); i++)
            {
                cablesShuffled[i].gameObject.SetActive(false);
            }


            // Count colors of active cables
            foreach(Cable cable in cables)
            {
                if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Red))
                {
                    redCount++;
                }
                else if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Blue))
                {
                    blueCount++;
                }
                else if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Green))
                {
                    greenCount++;
                }
                else if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Yellow))
                {
                    yellowCount++;
                }
            }


            // Randomly display the cat sticker
            showCat = UnityEngine.Random.value > 0.5;
            catSticker.SetActive(showCat);

            // Setup solution settings
            solution.text = "...";
            MarkSolution();
        }

        /// <summary>
        /// Return all cables in the scene.
        /// </summary>
        /// <returns>List of all cables in the scene</returns>
        public List<Cable> GetCables()
        {
            return cables;
        }

        /// <summary>
        /// Based on the random setup of the riddle, go through the setup and riddle rules to find the correct cable to cut.
        /// </summary>
        private void MarkSolution()
        {
            if(cableCount == 3)
            {
                // Information to solve the riddle with 3 cables are: cable count, red cables, yellow cables
                correctInfoAmount = 1; // Initially only the amount of cables is relevant
                
                if(redCount == 0)
                {
                    // Cut 3. cable
                    GetActiveCable(2).SetCorrect(true);
                }
                else if (yellowCount > 1)
                {
                    correctInfoAmount = 3; // At this point additionally the amount of red and yellow cables is important
                    // Cut 1. cable
                    GetActiveCable(0).SetCorrect(true);
                }
                else if (yellowCount == 1)
                {
                    // Cut the yellow cable
                    foreach(Cable cable in cables)
                    {
                        if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Yellow))
                        {
                            cable.SetCorrect(true);
                            break;
                        }
                    }
                }
                else
                {
                    // Cut 2. cable
                    GetActiveCable(1).SetCorrect(true);
                    correctInfoAmount = 3;
                }
            }
            else if (cableCount == 5)
            {
                // Information to solve the riddle are: cable count, red cables, yellow cables, green cables and cat
                correctInfoAmount = 1; // Initially only the amount of cables is relevant
                if (redCount == 1 && showCat)
                {
                    // Cut 4. cable
                    GetActiveCable(3).SetCorrect(true);

                    correctInfoAmount = 3; // At this point additionally the amount of red cables and the status of the cat is important
                }
                else if (yellowCount == 1 && redCount > 0)
                {
                    // Cut 1. cable
                    GetActiveCable(0).SetCorrect(true);

                    correctInfoAmount = 4; // At this point additionally the amount of yellow cables is important 
                }
                else if (yellowCount == 0)
                {
                    // Cut 3. cable
                    GetActiveCable(0).SetCorrect(true);

                }
                else if (greenCount == 1 && !showCat)
                {
                    // Cut green cable
                    foreach (Cable cable in cables)
                    {
                        if (cable.gameObject.activeSelf && cable.GetColor().Equals(CableColor.Green))
                        {
                            cable.SetCorrect(true);
                            break;
                        }
                    }

                    correctInfoAmount = 5; // At this point additionally the amount of green cables is important 
                }
                else
                {
                    // Cut 2. cable
                    GetActiveCable(1).SetCorrect(true);
                    correctInfoAmount = 5;
                }
            }

        }

        /// <summary>
        /// Helper-function that counts the active cables to return the one at the given index.
        /// </summary>
        /// <param name="index">Index of the active cable to return</param>
        /// <returns>The active cable at the given index</returns>
        private Cable GetActiveCable(int index)
        {
            int counter = 0;

            foreach (Cable cable in cables)
            {
                if (cable.gameObject.activeSelf)
                {
                    if(counter == index)
                    {
                        return cable;
                    }
                    else
                    {
                        // Only count active cables
                        counter++;
                    }
                }
            }

            return null;
        }

        public override void EvaluateSettings(Dictionary<string, string> settings)
        {
            // Similarly to the MarkSolution()-function, go through the given settings and set the solution text according to the riddle rules

            // Get all settings that might be relevant
            string cableCountValue = "-1";
            bool submittedCableCount = settings.TryGetValue(cableCountKey, out cableCountValue);

            string redCountvalue = "-1";
            bool submittedRedCount = settings.TryGetValue(redCountKey, out redCountvalue);

            string yellowCountvalue = "-1";
            bool submittedYellowCount = settings.TryGetValue(yellowCountKey, out yellowCountvalue);

            string greenCountvalue = "-1";
            bool submittedGreenCount = settings.TryGetValue(greenCountKey, out greenCountvalue);

            string showCatValue = "-1";
            bool submittedShowCat = settings.TryGetValue(catKey, out showCatValue);

            // Evaluate cable count
            if (submittedCableCount && Int32.Parse(cableCountValue) == 3)
            {
                // Continue with logic for 3 cables

                if(!submittedRedCount || Int32.Parse(redCountvalue) == 0)
                {
                    solution.text = "schneide 3. Kabel";
                }
                else if(submittedYellowCount && Int32.Parse(yellowCountvalue) > 1)
                {
                    solution.text = "schneide 1. Kabel";
                }
                else if (submittedYellowCount && Int32.Parse(yellowCountvalue) == 1)
                {
                    solution.text = "schneide gelbes Kabel";
                }
                else
                {
                    solution.text = "schneide 2. Kabel";
                }
            }
            else if (submittedCableCount && Int32.Parse(cableCountValue) == 5)
            {
                // Continue with logic for 5 cables

                if (submittedRedCount && Int32.Parse(redCountvalue) == 1 && submittedShowCat && Int32.Parse(showCatValue) == 1)
                {
                    solution.text = "schneide 4. Kabel";
                }
                else if (submittedYellowCount && Int32.Parse(yellowCountvalue) == 1 && submittedRedCount && Int32.Parse(redCountvalue) > 0)
                {
                    solution.text = "schneide 1. Kabel";
                }
                else if (!submittedYellowCount || Int32.Parse(yellowCountvalue) == 0)
                {
                    solution.text = "schneide 3. Kabel";
                }
                else if (submittedGreenCount && Int32.Parse(greenCountvalue) == 1 && (!submittedShowCat || Int32.Parse(showCatValue) == 1))
                {
                    solution.text = "schneide grünes Kabel";
                }
                else
                {
                    solution.text = "schneide 2. Kabel";
                }
            }
            else
            {
                // Information for evaluation are missing
                solution.text = "unzureichende Infos";
            }
        }

        /// <summary>
        /// When a cable is cut, the riddle sets the solution text accordingly and invokes the related event.
        /// </summary>
        /// <param name="correct">Whether the correct cable was cut or not</param>
        public void Solve(bool correct)
        {
            solution.text = correct ? "Korrekt!" : "Falsch!";
            OnSolved.Invoke();
        }

    }
}