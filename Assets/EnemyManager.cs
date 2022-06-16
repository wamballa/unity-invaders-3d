using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private RingArray[] enemyRings;

    // Start is called before the first frame update
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
