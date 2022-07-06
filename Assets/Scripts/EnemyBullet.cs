using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public float speed = .1f;
    [SerializeField] private GameObject explosion;

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

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.CompareTag("Defence") )
        //{
        //    //print("Hit defence");

        //}
        if (other.transform.CompareTag("Planet"))
        {
            //print(">>> " + transform.name + " " + other.name);
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.HandlePlanetHealth();
            GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(exp, 2f);
            Destroy(gameObject);
        }

    }

}
