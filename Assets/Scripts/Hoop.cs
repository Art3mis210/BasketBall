using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] Cloth clothSimulation;

    [SerializeField] public bool LerpEnabled;
    [SerializeField] public bool HeightChangeEnabled;
    [SerializeField] public float LerpSpeed;
    int LerpDirection;
    float StartingScale;
  

    [SerializeField] Transform LeftPoint;
    [SerializeField] Transform RightPoint;

    Vector3 HoopStartPos;
    Vector3 LeftPointStartPos;
    Vector3 RightPointStartPos;

    Vector3 HeightChangePoint;
    Vector3 NewHeightPoint;

    float LerpValue;

    void Start()
    {
        LerpValue = 0.5f;
        LerpDirection = 1;
        HoopStartPos = transform.position;
        LeftPointStartPos = LeftPoint.position;
        RightPointStartPos = RightPoint.position;
        StartingScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()           
    {
        if(LerpEnabled)
        {
            MoveHoopBetweenAB();
        }
        if(HeightChangeEnabled)
        {
            ChangeHoopHeight();
        }
    }
    void MoveHoopBetweenAB()        //Lerps the hoop between two points A and B
    {
        if (LerpDirection == 1)
        {
                if (LerpValue< 1)
                    LerpValue += Time.deltaTime* LerpSpeed;
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
    public void ResetHoop()         //Resets hoop when the game resets
    {
        LerpEnabled = false;
        HeightChangeEnabled = false;
        if(transform.position!=HoopStartPos)
            clothSimulation.enabled = false;
        LerpValue = 0.5f;
        LerpSpeed = 0;
        transform.localScale = new Vector3(StartingScale, StartingScale, StartingScale);
        LeftPoint.position = LeftPointStartPos;
        RightPoint.position = RightPointStartPos;
        transform.position = HoopStartPos;
        if(!clothSimulation.enabled)
            clothSimulation.enabled = true;
    }
    public void ChangePointsHeight()        //Changes height of the points A and B when score is a multiple of 10
    {
        LerpEnabled = false;
        LeftPoint.position = new Vector3(LeftPoint.position.x,1+Random.Range(-0.25f, 0.25f), LeftPoint.position.z);
        RightPoint.position = new Vector3(RightPoint.position.x, 1+Random.Range(-0.25f, 0.25f), RightPoint.position.z);
        LerpValue = 0;
        HeightChangePoint = transform.position;
        if (LerpDirection == 1)
            NewHeightPoint = RightPoint.position;
        else
            NewHeightPoint = LeftPoint.position;
        HeightChangeEnabled = true;
    }

    public void ChangeHoopHeight()
    {

        if (LerpValue < 1)
        {
            LerpValue += Time.deltaTime * LerpSpeed;
            transform.position = Vector3.Lerp(HeightChangePoint, NewHeightPoint, LerpValue);
        }
        else
        {
            HeightChangeEnabled = false;
            LerpEnabled = true;
            if (LerpDirection == 1)
            {
                LerpValue = 1;
            }
            else
            {
                LerpValue = 0;
            }
        }
        
    }
}
