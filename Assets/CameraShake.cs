using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform = default;
    private Vector3 _orignalPosOfCam = default;
    public float shakeFrequency = default;

    // Start is called before the first frame update
    void Start()
    {
        //When the game starts make sure to assign the origianl possition of the camera, to its current
        //position, supposedly it is where you want the camera to return after shaking.
        _orignalPosOfCam = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //Make sure to assign the value of shakeFrequency in the inspector 
        //    //or uncomment the next line to assign it here.
        //    //shakeFrequency = 0.2f;

        //    CameraShakes();
        //}
        //else if (Input.GetKeyUp(KeyCode.S))
        //{
        //    StopShake();
        //}
    }

    public  void CameraShakes()
    {
        //This moves the camera position to the random point chosen within the circle around the camera.
        //NB:Our Random.insideUnitSphere selects a random position every frame because of GetKey
        //which is called every frame, and that causes the shaking.
        cameraTransform.position = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
    }

    public void StopShake()
    {
        //Return the camera to it's original position.
        cameraTransform.position = _orignalPosOfCam;
    }
}
