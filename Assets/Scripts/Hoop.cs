using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] public bool LerpEnabled;
    [SerializeField] public float LerpSpeed;
    int LerpDirection;
  

    [SerializeField] Transform LeftPoint;
    [SerializeField] Transform RightPoint;

    Vector3 HoopStartPos;
    Vector3 LeftPointStartPos;
    Vector3 RightPointStartPos;

    float LerpValue;

    void Start()
    {
        LerpValue = 0.5f;
        LerpDirection = 1;
        HoopStartPos = transform.position;
        LeftPointStartPos = LeftPoint.position;
        RightPointStartPos = RightPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(LerpEnabled)
        {
            if (LerpDirection == 1)
            {
                if (LerpValue < 1)
                    LerpValue += Time.deltaTime * LerpSpeed;
                else
                    LerpDirection = -1;
            }
            else
            {
                if (LerpValue > 0)
                    LerpValue -= Time.deltaTime * LerpSpeed;
                else
                    LerpDirection = 1;
            }

            transform.position = Vector3.Lerp(LeftPoint.position, RightPoint.position, LerpValue);
        }
    }
    public void ResetHoop()
    {
        LerpEnabled = false;
        LerpValue = 0.5f;
        LerpSpeed = 0;
        LeftPoint.position = LeftPointStartPos;
        RightPoint.position = RightPointStartPos;
        transform.position = HoopStartPos;
    }
    public void ChangePointsHeight()
    {
        LeftPoint.position = new Vector3(LeftPoint.position.x,1+Random.Range(-0.25f, 0.25f), LeftPoint.position.z);
        RightPoint.position = new Vector3(RightPoint.position.x, 1+Random.Range(-0.25f, 0.25f), RightPoint.position.z);
    }
}
