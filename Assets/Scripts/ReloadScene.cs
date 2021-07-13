using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void reloadScene()
    {
       SceneManager.LoadScene("Hologram");
       ScoreManager.score = 0;
       Debug.Log("button pressed");
    }
}
