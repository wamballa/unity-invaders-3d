using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionHandler : MonoBehaviour
{

    public RingArray ringArray;

    // Start is called before the first frame update
    void Start()
    {
        ringArray = transform.parent.GetComponent<RingArray>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Bullet collided with " + collision.transform.name);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ringArray.RemoveEnemy(gameObject);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        //if (other.CompareTag("Enemy2")) Destroy(other.gameObject);
        //print("BUllet triggered " + other.name);
    }
}

// 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    