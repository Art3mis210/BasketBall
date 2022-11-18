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
    public float HoopScale;
    public ParticleSystem GoalEffect;

    #region UI
    [SerializeField] Text ScoreText;
    [SerializeField] Text HighScoreText;
    #endregion

    private void Awake()
    {
        instance = this;
        canScoreBeIncreased = true;
    }
    public void BallReset()         //Resets the game if ball didn't go into basket and resets variables for next throw
    {
        if(canScoreBeIncreased)
        {
            ResetScore();
            AudioManager.Instance.PlayAudio(AudioManager.Instance.audioClips[1]);
            isBallInBasket = false;
        }
        else
        {
            canScoreBeIncreased = true;
            isBallInBasket = false;
        }
        
    }
    public void UpdateScoreInUI(int Score)      //Changes score in the UI when ball goes into basket
    {
        ScoreText.text = Score.ToString();
        if (Score != 0)
        {
            if (Score % 5 == 0)                 //Hoop movement speed is increased at every multiple of 5
            {
                if (hoop.LerpEnabled == false)      
                    hoop.LerpEnabled = true;
                hoop.LerpSpeed += 0.02f;
            }
            if (Score % 10 == 0)                 //Hoop height is changed randomly at every multiple of 10
            {
                hoop.ChangePointsHeight();
            }
            if (Score % 20 == 0)                //Hoop size is reduced at every multiple of 20
            {
                HoopScale = hoop.transform.localScale.x;
                if (HoopScale > 225)
                    hoop.transform.localScale = new Vector3(HoopScale - 1, HoopScale - 1, HoopScale - 1);
            }
        }

    }
    public void UpdateHighScoreInUI(int HighScore)  //Changes high score in the UI when score > high score
    {
        HighScoreText.text = HighScore.ToString();
        PlayerPrefs.SetInt("HighScore", HighScore);
    }
    public void ResetScore()                    //Resets score when ball touches the ground without going into basket
    {
        score.ChangeScore(0);
        score.HighScoreBeat = false;
        UpdateScoreInUI(0);
        hoop.ResetHoop();
    }
}
