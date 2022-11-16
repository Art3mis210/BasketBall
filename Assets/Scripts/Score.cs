using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int CurrentScore;
    [SerializeField] int HighScore;

    void Start()
    {
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore",0);
        GameManager.instance.UpdateHighScoreInUI(HighScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (GameManager.instance.canScoreBeIncreased && GameManager.instance.isBallInBasket)
            {
                GameManager.instance.canScoreBeIncreased = false;
                CurrentScore++;
                GameManager.instance.UpdateScoreInUI(CurrentScore);
                if (CurrentScore > HighScore)
                {
                    HighScore = CurrentScore;
                    GameManager.instance.UpdateHighScoreInUI(HighScore);
                }
            }
        }
    }

    public void ChangeScore(int NewScore)
    {
        CurrentScore = NewScore;
    }
}
