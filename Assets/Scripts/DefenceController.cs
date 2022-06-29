using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour
{
    int maxHealth = 8;
    int health;

    bool isDead = false;

    IAmInTheWay inTheWay;
    ArrayManager arrayManager;

    [Header("Meshes to replace")]
    [SerializeField] private MeshFilter solid;
    [SerializeField] private MeshFilter trans;

    [Header("3 replacement solid prefabs")]
    [SerializeField] private MeshFilter[] newSolids;
    [Header("3 replacement trans prefabs")]
    [SerializeField] private MeshFilter[] newTrans;

    bool canSpawnNext = false;

    // Start is called before the first frame update
    void Start()
    { 

        health = maxHealth;
        inTheWay = GetComponent<IAmInTheWay>();
        if (inTheWay == null) print("ERROR: IAmInTheWay missing");
        arrayManager = transform.parent.GetComponentInParent<ArrayManager>();




    }

    // Update is called once per frame
    void Update()
    {
        HandlePrefabs();
    }

    void HandlePrefabs()
    {
        if (canSpawnNext)
        switch (health)
        {
            case 8:
                //arrayManager.GetDefencePrefab(1);
                    break;
            case 6:
                print("Health is 6 "+transform.name);
                canSpawnNext = false;

                    solid.sharedMesh = newSolids[0].sharedMesh;
                    trans.sharedMesh = newTrans[0].sharedMesh;


                    break;
            case 4:
                break;
            case 2:
                break;
            case 0:
                isDead = true;
                // delete defence from ring array
                transform.GetComponentInParent<BaseRingArray>().RemoveUnit(
                    gameObject);
                //// delete defence
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")
            && !isDead
            && inTheWay.GetIsSolid())
        {
            Destroy(other.gameObject);
            health--;
            canSpawnNext = true;
            //print("Name / health " + gameObject.name + " / " + health);
            //if (health == 0)
            //{
            //    isDead = true;
            //    // delete defence from ring array
            //    transform.GetComponentInParent<BaseRingArray>().RemoveUnit(
            //        gameObject);
            //    //// delete defence
            //    Destroy(gameObject);


            //}
        }
    }
}
