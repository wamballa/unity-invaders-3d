using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RIngMove : MonoBehaviour
{

    private float delay = 0.5f;

    float timer1;
    float timer2;

    bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        timer1 = Time.time + delay;
    }

    void FixedUpdate()
    {

        if (canRotate)
        {
            if (Time.time > timer1)
            {
                canRotate = false;
                timer2 = Time.time + delay;
            }
                transform.RotateAround(transform.position, transform.up, 1f);
        }
        else
        {
            if (Time.time > timer2)
            {
                canRotate = true;
                timer1 = Time.time + delay;
            }
        }



    }
}
