using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceArray : MonoBehaviour
{
    public enum Array
    {
        one,
        two,
        three,
        four
    }
    public Array arrayID;

    [SerializeField] private GameObject defencePF;
    [SerializeField] private int numberOfDefences;
    [SerializeField] private float radius;
    [SerializeField] private Transform centreTransform;



    [HideInInspector]
    public List<GameObject> defences;

    // Start is called before the first frame update
    void Start()
    {
        defences = new List<GameObject>();
        CreateUnitAroundPoint(numberOfDefences, centreTransform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {

        HandleTransparency();



    }

    private void HandleTransparency()
    {
        //float angle;
        //Vector3 axis;
        //transform.rotation.ToAngleAxis(out angle, out axis);

        //int angleInt = Mathf.RoundToInt(angle);

        float localRotation = transform.rotation.eulerAngles.y;
        //float localRotation = transform.localRotation.eulerAngles.y;
        int angleInt = Mathf.RoundToInt(localRotation);

        if (arrayID == Array.one)
        {
            print(">Defence array " + arrayID + " / " + angleInt);
        }
        if (arrayID == Array.two)
        {
            print(">Defence array " + arrayID + " / " + angleInt);
        }

        //print(">Ring " + arrayID + " / " + angleInt);

        if (angleInt % 90 != 0 || angleInt == 0 || angleInt == 180)
        {
            foreach (GameObject go in defences)
            {
                IAmInTheWay[] allChildren = go.GetComponentsInChildren<IAmInTheWay>();
                if (allChildren.Length != 0)
                    foreach (IAmInTheWay child in allChildren)
                    {
                        child.ShowTransparent();
                    }
            }
        }
        else
        {
            foreach (GameObject go in defences)
            {
                IAmInTheWay[] allChildren = go.GetComponentsInChildren<IAmInTheWay>();

                if (allChildren.Length != 0)
                    foreach (IAmInTheWay child in allChildren)
                    {
                        child.ShowSolid();
                    }
            }
        }
    }

    private void CreateUnitAroundPoint(int num, Vector3 point, float radius)
    {
        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            float radians = i * 2 * Mathf.PI / num; // no overlap
            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            ///* Now spawn */
            //float angle = 0.0f;
            //Vector3 axis = Vector3.zero;
            //transform.rotation.ToAngleAxis(out angle, out axis);

            GameObject defence = Instantiate(
                defencePF,
                spawnPos,
                transform.rotation);

            defence.name = arrayID + "DefenceArray " + i;

            defences.Add(defence);

        }
        foreach (GameObject go in defences)
        {
            go.transform.LookAt(point);
            go.transform.SetParent(transform);
        }
        if (arrayID == Array.one) transform.RotateAround(point, transform.right, 90);
        if (arrayID == Array.two) transform.RotateAround(point, transform.forward, 90);
        if (arrayID == Array.three)
        {
            transform.RotateAround(
                point,
                transform.right,
                90);
            transform.RotateAround(
                point,
                transform.forward,
                45);
        }
        if (arrayID == Array.four)
        {
            transform.RotateAround(
                point,
                transform.right,
                90);
            transform.RotateAround(
                point,
                transform.forward,
                -45);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (GameObject t in defences)
        {
            Gizmos.DrawRay(t.transform.position, t.transform.forward * 2);
        }

    }
}
