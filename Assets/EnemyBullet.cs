using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) print("ERROR: no rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed * transform.forward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Defence"))
    //    {
    //        print("Hit defence");
    //    }
    //    Destroy(gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Defence"))
        {
            print("Hit defence");
        }
        Destroy(gameObject);
    }

}
