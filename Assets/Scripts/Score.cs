using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int CurrentScore;
    [SerializeField] int HighScore;
    public bool HighScoreBeat;

    void Start()
    {
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore",0);
        GameManager.instance.UpdateHighScoreInUI(HighScore);
        HighScoreBeat = false;
    }

    private void OnTriggerEnter(Collider other)         //Trigger to detect that ball has passed through the basket and increases the score
    {
        if (other.gameObject.tag == "Ball")
        {
            if (GameManager.instance.canScoreBeIncreased && GameManager.instance.isBallInBasket)       
            {
                GameManager.instance.canScoreBeIncreased = false;
                CurrentScore++;
                GameManager.instance.UpdateScoreInUI(CurrentScore);
                GameManager.instance.GoalEffect.Play();                                     //plays particle effect for goal
                AudioManager.Instance.PlayAudio(AudioManager.Instance.audioClips[0]);       //plays goal sound

                if (CurrentScore > HighScore)
                {
                    HighScore = CurrentScore;
                    GameManager.instance.UpdateHighScoreInUI(HighScore);
                    if(!HighScoreBeat)                  //Play high score sound when high score is beaten
                    {
                        HighScoreBeat = true;
                        AudioManager.Instance.PlayAudio(AudioManager.Instance.audioClips[2]);
                    }
                }
            }
        }
    }

    public void ChangeScore(int NewScore)           //called to change score from other scripts
    {
        CurrentScore = NewScore;
    }
}
