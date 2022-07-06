using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{

    public GameObject[] invaderGroups;
    public int[] numInvadersInRing;

    int numberOfArrays;
    

    // Start is called before the first frame update
    void Start()
    {
        int ring = 0;
        for (int i=0; i < invaderGroups.Length; i++)
        {
            // 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each invader group
        // for each ring array
        // count invaders

        // Invader Group 0
        int i = 0;
        int numRings = invaderGroups[0].GetComponentsInParent<BaseRingArray>().Length;
        foreach( BaseRingArray b in invaderGroups[0].GetComponentsInParent<BaseRingArray>())
        {
            numInvadersInRing[i] = b.GetNumberOfInvaders();
            i++;
        }
        // Invader Group 1
        i = 0;
        numRings = invaderGroups[1].GetComponentsInParent<BaseRingArray>().Length;
        foreach (BaseRingArray b in invaderGroups[1].GetComponentsInParent<BaseRingArray>())
        {
            numInvadersInRing[i] = numInvadersInRing[i]+ b.GetNumberOfInvaders();
            i++;
        }
        // Invader Group 2
        i = 0;
        numRings = invaderGroups[2].GetComponentsInParent<BaseRingArray>().Length;
        foreach (BaseRingArray b in invaderGroups[2].GetComponentsInParent<BaseRingArray>())
        {
            numInvadersInRing[i] = numInvadersInRing[i] + b.GetNumberOfInvaders();
            i++;
        }

    }
}
