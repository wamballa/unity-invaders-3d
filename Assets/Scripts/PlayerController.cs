using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Initiating stuff

    [Header("Player stuff")]
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject explosionPF;
    [SerializeField] private AudioClip explosionSFX;
    [SerializeField] private GameObject playerPF;
    [SerializeField] private float speed;
    private bool isPlayerAlive = true;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private int lives = 3;

    // Explosion stuff
    private float explosionTimer;
    private float explosionDelay = .1f;
    private int explosionCounter = 0;
    private int maxExplosions = 10;
    [Header("Player explosion stuff")]
    [SerializeField] private GameObject[] explosionSpawnPoints;
    [SerializeField] private TMP_Text gameOverText;



    // Movement stuff
    private float cameraRotation = 45;
    private float rotationSpeed = 0.75f;
    private float currentAngle;
    bool canRotate = false;
    int rotationDirection;
    float targetRotation;


    // FIRE
    float fireTimer;
    [Header("Firing stuff")]
    public float fireDelay = .5f;
    public Transform bulletSpawnPoint;
    public GameObject bulletPF;
    public AudioClip fireSFX;
    [Header("Rotation stuff")]
    [SerializeField] private AudioClip rotationSFX;
    [SerializeField] private Transform planet;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        livesText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive) HandleInput();
        HandleLivesText();
        HandlePlayerDeath();

    }
    private void HandleLivesText()
    {
        livesText.text = lives.ToString();
    }
    public void PlayerIsDead()
    {
        print("PLAYER DEAD");
        playerPF.SetActive(false);
        isPlayerAlive = false;

    }

    private void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        if (canRotate)
        {
            if (rotationDirection == 1)
            {
                //currentAngle++;
                currentAngle = currentAngle + rotationSpeed;
                Vector3 dir = new Vector3(0, 1, 0) * rotationDirection;
                //planet.Rotate(dir * 1f);
                planet.Rotate(dir * rotationSpeed);
                if (currentAngle >= targetRotation)
                {
                    canRotate = false;
                }
            }
            else if (rotationDirection == -1)
            {
                //currentAngle--;
                currentAngle = currentAngle - rotationSpeed;
                Vector3 dir = new Vector3(0, 1, 0) * rotationDirection;
                //planet.Rotate(dir * 1f);
                planet.Rotate(dir * rotationSpeed);
                if (currentAngle <= targetRotation)
                {
                    canRotate = false;
                }
            }
        }
    }
    void HandleInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.RotateAround(planet.position, Vector3.forward, 1 * speed);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(planet.position, Vector3.forward, -1 * speed);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            //StartCoroutine(RotateMe(Vector3.up * cameraRotation, 1));
            if (!canRotate)
            {
                audioSource.PlayOneShot(rotationSFX);
                targetRotation -= cameraRotation;
                rotationDirection = -1;
                canRotate = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (!canRotate)
            {
                audioSource.PlayOneShot(rotationSFX);
                targetRotation += cameraRotation;
                rotationDirection = 1;
                canRotate = true;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time > fireTimer)
        {
            audioSource.PlayOneShot(fireSFX);
            GameObject bullet = Instantiate(bulletPF,
                bulletSpawnPoint.position,
                transform.rotation);
            fireTimer = Time.time + fireDelay;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {

            //print("Player hit by bullet");
            if (lives > 1)
            {
                lives--;
                audioSource.PlayOneShot(explosionSFX);
                GameObject go = Instantiate(
                    explosionPF,
                    transform.position,
                    transform.rotation);
                Destroy(go, 2f);
                // Clear all bullets
                GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
                foreach (GameObject g in enemyBullets)
                {
                    Destroy(g);
                }

                transform.position = startPosition;
                transform.rotation = startRotation;
            }
            else
            {
                isPlayerAlive = false;
                //GameObject go = Instantiate(
                //    explosionPF,
                //    transform.position,
                //    transform.rotation);
                //Destroy(go, 2f);
                //playerPF.SetActive(false);
                //StartCoroutine(GameOver());
            }
        }
    }

    void HandlePlayerDeath()
    {
        if (!isPlayerAlive)
        {
            // Tween GameOver Text
            float n = gameOverText.fontSize;


            if (Time.time > explosionTimer)
            {
                if (explosionCounter <= maxExplosions)
                {
                    print("PLAYER DEAD do explosion " + explosionCounter);
                    playerPF.SetActive(false);
                    explosionCounter++;
                    explosionTimer = Time.time + explosionDelay;
                    foreach (GameObject explosion in explosionSpawnPoints)
                    {
                        GameObject go = Instantiate(explosionPF, explosion.transform.position, Quaternion.identity);
                        Destroy(go, 2f);
                    }
                }
                else
                {
                    StartCoroutine(GameOver());
                }
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
