using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform planet;


    private float cameraRotation = 45;
    private float rotationSpeed = 0.75f;
    private float currentAngle;
    bool canRotate = false;
    int rotationDirection;
    float targetRotation;

    // FIRE
    float fireTimer;
    [Header("Firing stuff")]
    public float fireDelay = 1;
    public Transform bulletSpawnPoint;
    public GameObject bulletPF;
    public AudioClip fireSFX;
    [Header("Rotation whoosh")]
    [SerializeField] private AudioClip rotationSFX;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

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

            transform.RotateAround(planet.position, Vector3.forward, 1);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(planet.position, Vector3.forward, -1);
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
        if (Input.GetKeyDown(KeyCode.Space))
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

}
