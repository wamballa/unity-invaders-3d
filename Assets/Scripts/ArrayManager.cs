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

    public float GetFireDelay()
    {
        return fireDelay;
    }

    public int GetInvaderDirection()
    {
        return invaderDirection;
    }

    //public GameObject GetDefencePrefab(int n)
    //{
    //    switch (n)
    //    {
    //        case 1:
    //            return defenceState1;

    //        case 2:
    //            return defenceState2;

    //        case 3:
    //            return defenceState3;

    //        case 4:
    //            return defenceState4;

    //        default:
    //            print("ERROR: Array Manager Switch Error");
    //            return null;
    //    }
    //}
}
