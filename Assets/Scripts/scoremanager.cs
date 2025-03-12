using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int maxScore = 5;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    //we will call this method from our target script
    // whenever the player collides or shoots a target a point will be added
    public void AddPoint()
    {
        score++;

        if (score == maxScore)
        {
            scoreText.text = "You won!";
            SceneManager.LoadScene("GameWin");
        }
        else
        {
            scoreText.text = "Score: " + score;
        }
    }
}