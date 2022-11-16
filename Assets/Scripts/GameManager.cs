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

    public bool canScoreBeIncreased;
    public bool isBallInBasket;

    #region UI
    [SerializeField] Text ScoreText;
    [SerializeField] Text HighScoreText;
    #endregion

    private void Awake()
    {
        instance = this;
        canScoreBeIncreased = true;
    }
    public void BallReset()
    {
        if(canScoreBeIncreased)
        {
            ResetScore();
            isBallInBasket = false;
        }
        else
        {
            canScoreBeIncreased = true;
            isBallInBasket = false;
        }
        
    }
    public void UpdateScoreInUI(int Score)
    {
        ScoreText.text = Score.ToString();
        if (Score % 5 == 0)
        {
            if (hoop.LerpEnabled == false)
                hoop.LerpEnabled = true;
            hoop.LerpSpeed += 0.02f;
        }
        if (Score % 10 == 0)
        {
            hoop.ChangePointsHeight();
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
