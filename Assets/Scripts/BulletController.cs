using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    Rigidbody rb;
    private float speed = 3f;

    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * Speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Invader"))
        {
            print("Hit Invader");
            other.GetComponentInParent<BaseRingArray>().RemoveUnit(other.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        //if (other.transform.CompareTag("Defence"))
        //{
        //    print("Hit Defence");
        //    Destroy(gameObject);
        //}
    }
}
