using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private Transform bulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleFiring()
    {
        //print("Fire! "+transform.name);
        Instantiate(bulletPF, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

}
