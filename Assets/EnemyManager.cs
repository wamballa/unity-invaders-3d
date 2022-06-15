using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private RingArray ring1;

    // Start is called before the first frame update
    void Start()
    {
        ring1.CanFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
