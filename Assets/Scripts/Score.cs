using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public bool canScoreBeIncreased;
    [SerializeField] int CurrentScore;
    [SerializeField] int HighScore;

    void Start()
    {
        CurrentScore = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            canScoreBeIncreased = false;
            CurrentScore++;
        }
    }
}
