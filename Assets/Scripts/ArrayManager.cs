using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayManager : MonoBehaviour
{

    //[SerializeField] private BaseRingArray[] enemyRings;
    [Header("INVADER & DEFENCE")]
    public float radius;

    [Header("INVADER ONLY = Firing delay")]
    public float fireDelay = 1;

    [Header("INVADER ONLY = Direction")]
    public int invaderDirection;


    ///////////////////////

    public float GetFireDelay()
    {
        return fireDelay;
    }

    public int GetInvaderDirection()
    {
        return invaderDirection;
    }




}
