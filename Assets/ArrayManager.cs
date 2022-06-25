using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayManager : MonoBehaviour
{

    [SerializeField] private RingArray[] enemyRings;
    public float radius;

    void Start()
    {

        foreach (RingArray ring in enemyRings)
        {
            ring.CanFire = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
