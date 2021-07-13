using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int goal = 100;
    public static int score = 0;
    public Text gameStatus;

    public AudioSource audioSource;

    public AudioClip SFXexplosion;
    public AudioClip SFXwin;


    public void checkScore() 
    {
        if(score > goal)
        {
            gameOver();
        } 
        else if(score == goal)
        {
            gameWon();
        }
    }


    public void gameOver()
    {
        gameStatus.color = Color.red;
        gameStatus.text = "the bomb has exploded!";

        audioSource.PlayOneShot(SFXexplosion);
    }


    public void gameWon()
    {
        gameStatus.color = Color.green;
        gameStatus.text = "You successfully defused the bomb!";

        audioSource.PlayOneShot(SFXwin);
    }
}
