using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform planet;
    public Transform bulletSpawnPoint;
    public GameObject bulletPF;

    private float cameraRotation = 45;

    private bool hasRotated = false;
    bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    float rotationT;
    float timer;
    float strikeSpeed;

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        //print("Can Rotate");
        canRotate = false;
        var fromAngle = planet.rotation;
        var toAngle = Quaternion.Euler(planet.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            planet.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
            canRotate = true;
        }
    }
    void HandleInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow)) {

            transform.RotateAround(planet.position, Vector3.forward, 1);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(planet.position, Vector3.forward, -1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (canRotate)
            {
                //print("rotate false");
                StartCoroutine(RotateMe(Vector3.up * cameraRotation, 1));
                // rotate player
                //planet.RotateAround(planet.position, Vector3.up, 90);
                //hasRotated = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //print("rotate false");
            StartCoroutine(RotateMe(Vector3.up * -cameraRotation, 1));
            // rotate player
            //planet.RotateAround(planet.position, Vector3.up, 90);
            //hasRotated = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPF, bulletSpawnPoint.position, transform.rotation) ;
    }

}
