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

    private int rotationDirection = 1;

    // Start is called before the first frame update
    void Start()
    {

        rotationDirection = transform.GetComponentInParent<ArrayManager>().GetInvaderDirection();

        timer1 = Time.time + rotateDuration;
    }

    void FixedUpdate()
    {
        if (canRotate && pauseDelay == 0)
        {
            transform.RotateAround(
                transform.position,
                transform.forward * rotationDirection,
                0.5f);

        }

        if (canRotate && pauseDelay != 0)
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
