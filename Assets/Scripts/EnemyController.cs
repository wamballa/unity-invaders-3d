using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBase
{
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameManager gameManager;

    IAmInTheWay inTheWay;
    bool isDead = false;

    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inTheWay = transform.GetComponent<IAmInTheWay>();
        if (inTheWay == null) print("ERROR: enemy has no IAmInTheWay");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleFiring();
        }
    }

    public void HandleFiring()
    {
        //Instantiate(bulletPF, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        GameObject bullet =  Instantiate(
            bulletPF, 
            bulletSpawnPoint.position, 
            transform.rotation);
        bullet.transform.LookAt(Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            if (inTheWay.GetIsSolid())
            {
                //print("Impact");
                gameManager.SetScore();
                isDead = true;
                Destroy(other.gameObject);
                // Explode
                ExplodeMe();
                // ADD SCORE TO PLAYER
                transform.GetComponentInParent<BaseRingArray>()
                    .RemoveUnit(gameObject);
                Destroy(gameObject);
            }
        }
    }

}
