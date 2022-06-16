using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int planetHealth;
    public int PlanetHealth {
        get { return planetHealth; }
        set { planetHealth = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        planetHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if ( planetHealth <= 0)
        {
            print("Planet Dead");
        }
    }
}
