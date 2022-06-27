using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform planet;
    public Transform bulletSpawnPoint;
    public GameObject bulletPF;

    private float cameraRotation = 45;
    private float rotationSpeed = 0.5f;

    private float currentAngle;

    //private bool hasRotated = false;
    bool canRotate = false;
    int rotationDirection;

    Vector3 byAngle;
    Quaternion fromAngle;
    Quaternion toAngle;

    float targetRotation;

    // Start is called before the first frame update
    void Start()
    {

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
                targetRotation -= cameraRotation;
                rotationDirection = -1;
                canRotate = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (!canRotate)
            {
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
        GameObject bullet = Instantiate(bulletPF, bulletSpawnPoint.position, transform.rotation);
    }

}
