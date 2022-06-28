using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayManager : MonoBehaviour
{

    [SerializeField] private RingArray[] enemyRings;
    public float radius;

    [Header("Firing delay")]
    public float fireDelay = 1;

public float GetFireDelay()
    {
        return fireDelay;
    }
}
