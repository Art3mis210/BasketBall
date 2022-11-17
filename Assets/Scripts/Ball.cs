using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Ball Physics
    [SerializeField] Vector3 Force;
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
        if (Throwable)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    TouchStart = touch.position;
                    ray = Camera.main.ScreenPointToRay(TouchStart);
                    if(Physics.Raycast(ray,out hit,20))
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
                        if (Physics.Raycast(ray, out hit, 20))
                        {
                            if (hit.transform.tag != "Ball")
                                BallTouched = false;
                        }
                        else
                            BallTouched = false;

                        if (TouchEnd.y > TouchStart.y && !BallTouched)
                        {
                            InputForce = TouchEnd - TouchStart;
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

        if (SpawnLocationXCoordinate == 0)
            InputForce.x = (InputForce.x) / (4);
        else if (SpawnLocationXCoordinate == 0.25)
        {
            if(InputForce.x < 0)
                InputForce.x = (InputForce.x) / (16);
            else
                InputForce.x = (InputForce.x) / (2f);
        }
        else if (SpawnLocationXCoordinate == -0.25)
        {
            if (InputForce.x > 0)
                InputForce.x = (InputForce.x) / (16);
            else
                InputForce.x = (InputForce.x) / (2f);
        }
        InputForce.z = Force.z;
        InputForce.y = (2) * Mathf.Sqrt(2*9.8f*InputForce.y);
        rb.AddForce(InputForce);
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
                SpawnLocationXCoordinate = 0; ;
            transform.position = new Vector3(SpawnLocationXCoordinate, 1.27f, 0);
            Throwable = true;
            rb.isKinematic = true;
            GameManager.instance.BallReset();
        }
    }
}
