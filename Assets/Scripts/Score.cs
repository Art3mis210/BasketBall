using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int CurrentScore;
    [SerializeField] int HighScore;
    public bool HighScoreBeat;
    public bool HighScoreBeatComplete;

    void Start()
    {
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt("HighScore",0);
        GameManager.instance.UpdateHighScoreInUI(HighScore);
        HighScoreBeat = false;
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
                GameManager.instance.GoalEffect.Play();
                AudioManager.Instance.PlayAudio(AudioManager.Instance.audioClips[0]);
                if (CurrentScore > HighScore)
                {
                    HighScore = CurrentScore;
                    GameManager.instance.UpdateHighScoreInUI(HighScore);
                    if(!HighScoreBeat)
                    {
                        HighScoreBeat = true;
                        AudioManager.Instance.PlayAudio(AudioManager.Instance.audioClips[2]);
                    }
                }
                if (HighScoreBeatComplete|| !HighScoreBeat)
                {
                    
                }
            }
        }
    }

    public void ChangeScore(int NewScore)
    {
        CurrentScore = NewScore;
    }
}
