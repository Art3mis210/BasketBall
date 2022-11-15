using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Ball Physics
    [SerializeField] Vector3 Force;
    Rigidbody rb;
    float ForceY;
    float ForceHeightAdder;
    #endregion

    #region Player Input
    Touch touch;
    Vector3 TouchStart;
    Vector3 TouchEnd;
    #endregion


    bool Throwable;
    bool BallTouched;

    RaycastHit hit;
    Ray ray;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Throwable = true;
        BallTouched = false;
        ForceHeightAdder = 0;
        ForceY=Force.y;
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
                if (touch.phase == TouchPhase.Moved)
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
                            Force.x = (TouchEnd.x - TouchStart.x);
                         //   ForceHeightAdder = (TouchEnd.y - TouchStart.y)/100;
                          //  Force.y = ForceY + ForceHeightAdder;
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
        rb.AddForce(Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            transform.position = new Vector3(0, 1.27f, 0);
            Throwable = true;
            rb.isKinematic = true;
            GameManager.instance.BallReset();
        }
    }
}
