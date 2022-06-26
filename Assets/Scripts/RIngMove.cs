using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RIngMove : MonoBehaviour
{

    public float rotateDuration = 2f;
    public float pauseDelay = 2f;

    float timer1;
    float timer2;

    bool canRotate = true;

    private int rotationDirection;

    // Start is called before the first frame update
    void Start()
    {

        rotationDirection = (Random.Range(0, 10) > 5) ? 1 : -1;

        timer1 = Time.time + rotateDuration;
    }

    void FixedUpdate()
    {

        if (canRotate)
        {
            if (Time.time > timer1)
            {
                canRotate = false;
                timer2 = Time.time + pauseDelay;
            }
            transform.RotateAround(transform.position, transform.forward* rotationDirection, 0.5f);
        }
        else
        {
            if (Time.time > timer2)
            {
                canRotate = true;
                timer1 = Time.time + rotateDuration;
            }
        }



    }
}
