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
    [SerializeField] private GameObject playerPFhit;
    [SerializeField] private float speed;
    private bool isPlayerAlive = true;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private int lives = 3;
    private bool canMove = true;

    // Explosion stuff
    private float explosionTimer;
    private float explosionDelay = .1f;
    private int explosionCounter = 0;
    private int maxExplosions = 10;
    [Header("Player explosion stuff")]
    [SerializeField] private GameObject[] explosionSpawnPoints;
    [SerializeField] private GameObject gameOverTextPF;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Animation restartAnim;
    [SerializeField] private CameraShake cameraShake;
    public bool canShake = false;

    float startFontSize;
    float lerpAmount = 0.5f;
    float lerpT;



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
        restartAnim.Stop();
        audioSource = transform.GetComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        livesText.text = lives.ToString();
        startFontSize = gameOverText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!canMove) return;
        if (isPlayerAlive && canMove) HandleInput();
        HandleLivesText();
        HandlePlayerDeath();
        HandleCameraShake();
    }

    void HandleCameraShake()
    {
        //print("HANDLE SHAKING");
        if (canShake)
        {
            print("SHAKE");
            cameraShake.CameraShakes();
        }
        else
        {
            cameraShake.StopShake();
        }
    }
    private void HandleLivesText()
    {
        livesText.text = lives.ToString();
    }
    public void PlayerIsDead()
    {
        //print("PLAYER DEAD");
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
        if (!canMove) return;
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
                KillInvaderBullets();
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
                KillInvaderBullets();
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

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    isPlayerAlive = false;
        //}
    }
    void KillInvaderBullets()
    {
        GameObject[] invaderBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject g in invaderBullets)
        {
            Destroy(g);
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
            Destroy(bullet, 10);
            fireTimer = Time.time + fireDelay;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (lives > 1)
            {
                lives--;
                audioSource.PlayOneShot(explosionSFX);
                // create explosion
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
                //transform.position = startPosition;
                //transform.rotation = startRotation;
                print("PLAYER HIT");
                canShake = true;
                StartCoroutine(RestartPlayer());

            }
            else
            {
                isPlayerAlive = false;
            }
        }
    }

    IEnumerator RestartPlayer()
    {
        print("RESTART PLAYER");
        canShake = true;
        playerPF.SetActive(false);
        playerPFhit.SetActive(true);
        canMove = false;
        // disable collsions
        transform.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(2);
        print("YIELD DONE");
        // stop shaking
        canShake = false;
        // enable collsions
        transform.GetComponent<BoxCollider>().enabled = true;
        playerPF.SetActive(true);
        playerPFhit.SetActive(false);
        canMove = true;
        KillInvaderBullets();
        //transform.position = startPosition;
        //transform.rotation = startRotation;
    }


    void HandlePlayerDeath()
    {
        if (!isPlayerAlive)
        {
            // make text visible
            gameOverTextPF.SetActive(true);
            gameOverText.fontSize = Mathf.Lerp(startFontSize, 150, lerpT);
            lerpT += lerpAmount * Time.deltaTime;

            if (Time.time > explosionTimer)
            {
                audioSource.PlayOneShot(explosionSFX);
                if (explosionCounter <= maxExplosions)
                {
                    //print("PLAYER DEAD do explosion " + explosionCounter);
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
        yield return new WaitForSeconds(4);
        //SimpleSceneFader.ChangeSceneWithFade("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
