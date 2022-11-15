using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get;
        set;
    }
    public Ball ball;
    public Score score;
    public Hoop hoop;

    #region UI
    [SerializeField] Text ScoreText;
    [SerializeField] Text HighScoreText;
    #endregion

    private void Start()
    {
        instance = this;
    }
    public void BallReset()
    {
        if(score.canScoreBeIncreased)
        {
            ResetScore();
        }
        else
        {
            score.canScoreBeIncreased = true;
        }
        
    }
    public void UpdateScoreInUI(int Score)
    {
        ScoreText.text = Score.ToString();
        if (Score % 10 == 0)
        {
            if (hoop.LerpEnabled == false)
                hoop.LerpEnabled = true;
            hoop.LerpSpeed += 0.05f;
        }
    }
    public void UpdateHighScoreInUI(int HighScore)
    {
        HighScoreText.text = HighScore.ToString();
        PlayerPrefs.SetInt("HighScore", HighScore);
    }
    public void ResetScore()
    {
        score.ChangeScore(0);
        UpdateScoreInUI(0);
        hoop.ResetHoop();
    }
}
