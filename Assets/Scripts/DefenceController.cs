using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour
{
    int maxHealth = 5;
    int health;

    bool isDead = false;

    IAmInTheWay inTheWay;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        inTheWay = GetComponent<IAmInTheWay>();
        if (inTheWay == null) print("ERROR: IAmInTheWay missing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")
            && !isDead
            && inTheWay.GetIsSolid())
        {
            Destroy(other.gameObject);
            health--;
            //print("Name / health " + gameObject.name + " / " + health);
            if (health == 0)
            {
                isDead = true;
                // delete defence from ring array
                transform.GetComponentInParent<RingArray>().RemoveEnemy(
                    gameObject);
                //// delete defence
                Destroy(gameObject);


            }
        }
    }
}
