using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour
{
    int maxHealth = 5;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health--;
            //print("Name / health " + gameObject.name + " / " + health);
            if (health == 0)
            {
                // delete defence from ring array
                transform.GetComponentInParent<RingArray>().RemoveEnemy(other.gameObject);
                //// delete defence
                Destroy(gameObject);


            }
        }
    }
}
