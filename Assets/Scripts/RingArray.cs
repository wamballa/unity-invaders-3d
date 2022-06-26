// https://answers.unity.com/questions/1661755/how-to-instantiate-objects-in-a-circle-formation-a.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RingArray : MonoBehaviour
{

    public enum Ring
    {
        one,
        two,
        three,
        four,
        five,
        six,
        seven
    }
    public Ring ringID;

    //public TMP_Text debugText;

    public GameObject prefab;
    [Range(0, 10)]
    public int numberOfEnemies;
    public Transform positionTransform;
    private float radius;
    //public float rotation;

    [HideInInspector]
    public List<GameObject> units;

    [HideInInspector]
    private bool canFire;
    public bool CanFire
    {
        get { return canFire; }
        set
        {
            canFire = value;
            //print(transform.name + " Canfire "+value);
        }
    }

    float fireTimer;
    float fireDelay = 1;

    private int angleInt;

    // Start is called before the first frame update
    void Start()
    {
        ArrayManager enemyManager = transform.GetComponentInParent<ArrayManager>();
        if (enemyManager == null) print("ERROR; no enemy manager");
        radius = enemyManager.radius;
        units = new List<GameObject>();
        CreateEnemiesAroundPoint(numberOfEnemies, positionTransform.position, radius);

        fireTimer = Time.time + fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        SpaceEnemies();
        HandleFiring();
        HandleTransparency();
    }

    public int GetRotation()
    {
        return angleInt;
    }

    private void HandleTransparency()
    {
        //float angle;
        //Vector3 axis = Vector3.zero;
        //transform.rotation.ToAngleAxis(out angle, out axis);
        float localRotation = transform.rotation.eulerAngles.y;
        angleInt = Mathf.RoundToInt(localRotation);

        //if (ringID == Ring.four)
        //{
        //    if (angleInt == 180 || angleInt == 0)
        //    {
        //        foreach (GameObject go in enemies)
        //        {
        //            IAmInTheWay[] allChildren = go.GetComponentsInChildren<IAmInTheWay>();

        //            if (allChildren.Length != 0)
        //                foreach (IAmInTheWay child in allChildren)
        //                {
        //                    print("set solid ");
        //                    child.ShowSolid();
        //                }
        //        }
        //    }
        //    else
        //    {
        //        foreach (GameObject go in enemies)
        //        {
        //            IAmInTheWay[] allChildren = go.GetComponentsInChildren<IAmInTheWay>();

        //            if (allChildren.Length != 0)
        //                foreach (IAmInTheWay child in allChildren)
        //                {
        //                    child.ShowSolid();
        //                }
        //        }
        //    }
        //}

        if (angleInt != 0)
        //if (angleInt % 90 != 0 || angleInt == 0 || angleInt == 180)
        {
            foreach (GameObject go in units)
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
            foreach (GameObject go in units)
            {
                IAmInTheWay[] allChildren = go.GetComponentsInChildren<IAmInTheWay>();

                if (allChildren.Length != 0)
                    foreach (IAmInTheWay child in allChildren)
                    {
                        child.ShowSolid();
                    }
            }
        }
        if (ringID == Ring.four)
        {
            if (angleInt == 180 || angleInt == 0)
            {
                foreach (GameObject go in units)
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
    }

    void HandleFiring()
    {
        if (!CanFire) return;
        if (Time.time > fireTimer)
        {
            fireTimer = Time.time + fireDelay;
            int enemyCount = units.Count;
            int randomEnemy = Random.Range(0, units.Count - 1);
            units[randomEnemy].GetComponent<EnemyController>().HandleFiring();
            //print("RingArray trigger fire");
        }
    }

    void SpaceEnemies()
    {
        if (units.Count < numberOfEnemies)
        {
            //print("Ring " + ringID + " has less enemies in it");
        }

    }
    public void RemoveEnemy(GameObject enemy)
    {
        // Removes enemy from the list
        if (units.Count > 0)
        {
            units.Remove(enemy);
        }
    }
    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
    {

        for (int i = 0; i < num; i++)
        {
            float radians = i * 2 * Mathf.PI / num; // no overlap

            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            //Vector3 spawnDir = new Vector3(horizontal, 0, vertical);
            Vector3 spawnDir = new Vector3(horizontal, vertical, 0);

            /* Get the spawn position */
            Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            GameObject unit = Instantiate(
                prefab,
                spawnPos,
                transform.rotation);

            unit.name = ringID.ToString() + i;

            units.Add(unit);

            foreach (GameObject go in units)
            {
                go.transform.LookAt(point);
                go.transform.SetParent(transform);
            }
        }

        if (prefab.transform.CompareTag("Defence"))
        {
            units[2].transform.RotateAround(
                units[2].transform.position,
                units[2].transform.forward,
                90);
            units[6].transform.RotateAround(
                units[6].transform.position,
                units[6].transform.forward,
                90);
        }

        {
            int rot = 0;
            if (ringID == Ring.one) { }// do nothing;
            rot += 45;
            if (ringID == Ring.two) transform.RotateAround(point, transform.up, rot);
            rot += 45;
            if (ringID == Ring.three) transform.RotateAround(point, transform.up, rot);
            rot += 45;
            if (ringID == Ring.four) transform.RotateAround(point, transform.up, rot);
            rot += 45;
            if (ringID == Ring.five) transform.RotateAround(point, transform.up, rot);
            rot += 45;
            if (ringID == Ring.six) transform.RotateAround(point, transform.up, rot);
            rot += 45;
            if (ringID == Ring.seven) transform.RotateAround(point, transform.up, rot);
        }
    }


}
