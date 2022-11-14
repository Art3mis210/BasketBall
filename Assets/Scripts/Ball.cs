using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Vector3 Force;

    Rigidbody rb;
    bool Throwable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Throwable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Throwable)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Throwable = false;
                rb.isKinematic = false;
                rb.AddForce(Force);
                
            }
        }
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
