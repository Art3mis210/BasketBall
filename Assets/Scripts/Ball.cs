using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Ball Physics
    [SerializeField] float ForwardForce;
    Rigidbody rb;

    Vector3 InputForce;
    #endregion

    #region Player Input
    Touch touch;
    Vector3 TouchStart;
    Vector3 TouchEnd;
    #endregion

    #region Ball Behaviour
    bool Throwable;
    bool BallTouched;
    float SpawnLocationXCoordinate;
    #endregion

    RaycastHit hit;
    Ray ray;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Throwable = true;
        BallTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Throwable)                              // True when ball is at throwing point
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)            //Touch started
                {
                    TouchStart = touch.position;
                    ray = Camera.main.ScreenPointToRay(TouchStart);     
                    if(Physics.Raycast(ray,out hit,20))             //Checks whether touch input start position is on the ball
                    {
                        if (hit.transform.tag == "Ball")
                            BallTouched = true;
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (BallTouched)
                    {
                        TouchEnd = touch.position;
                        
                        ray = Camera.main.ScreenPointToRay(TouchEnd);
                        if (Physics.Raycast(ray, out hit, 20))      //Checks whether touch input end position is not on the ball
                        {
                            if (hit.transform.tag != "Ball")
                                BallTouched = false;
                        }
                        else
                            BallTouched = false;

                        if (TouchEnd.y > TouchStart.y && !BallTouched)      // Throw ball only if y coordinate of touch end is higher than touch start y coordinate
                        {
                            InputForce.y = TouchEnd.y - TouchStart.y;
                            InputForce.x = TouchEnd.x - TouchStart.x;
                            ThrowBall();
                        }
                    }
                }

            }
        }
    }

    void ThrowBall()
    {
        Throwable = false;                 
        rb.isKinematic = false;             

        InputForce.x = (InputForce.x) / (4);   //ball at centre throwing point

        if (SpawnLocationXCoordinate == 0.25)  //ball is at right throwing point
        {
            InputForce.x += 25;                 //Added to adjust for accuracy while throwing
        }
        else if (SpawnLocationXCoordinate == -0.25) //ball is at left throwing point
        {
            InputForce.x -= 25;                //Added to adjust for accuracy while throwing
        }
        
        InputForce.z = ForwardForce;
        InputForce.y = (2) * Mathf.Sqrt(2*9.8f*InputForce.y); //Force required to reach desired height sqrt(2gh)
        rb.AddForce(InputForce);    //Adding force to the ball
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground")      
        {
            if (transform.position.x < -0.2)            
                SpawnLocationXCoordinate = -0.25f;
            else if (transform.position.x > 0.2)
                SpawnLocationXCoordinate = 0.25f;
            else
                SpawnLocationXCoordinate = 0; 
            transform.position = new Vector3(SpawnLocationXCoordinate, 1.27f, 0);   //Returning ball to the nearest throw point 
            Throwable = true;
            rb.isKinematic = true;
            GameManager.instance.BallReset();           //Game reset function
        }
    }
}
