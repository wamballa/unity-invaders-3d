using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBase
{
    [SerializeField] private GameObject bulletPF;
    [SerializeField] private Transform bulletSpawnPoint;

    IAmInTheWay inTheWay;
    bool isDead = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        inTheWay = transform.GetComponent<IAmInTheWay>();
        if (inTheWay == null) print("ERROR: enemy has no IAmInTheWay");
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleFiring()
    {
        audioSource.PlayOneShot(fireSFX);
        Instantiate(bulletPF, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            isDead = true;
            Destroy(other.gameObject);
            if (inTheWay.GetIsSolid())
            {
                // ADD SCORE TO PLAYER
                transform.GetComponentInParent<RingArray>().RemoveEnemy(
    gameObject);
                Destroy(gameObject);
            }
        }
    }

}
