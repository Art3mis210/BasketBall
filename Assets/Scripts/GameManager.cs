using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get;
        set;
    }
    public Ball ball;
    public Score score;

    private void Start()
    {
        instance = this;
    }
    public void BallReset()
    {
        score.canScoreBeIncreased = true;
    }
}
