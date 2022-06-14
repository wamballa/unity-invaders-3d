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
        two
    }
    public Ring ringID;

    public TMP_Text debugText;

    public GameObject enemyPrefab;
    [Range(0, 10)]
    public int numberOfEnemies;
    public Transform positionTransform;
    public float radius;
    public float rotation;

    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        CreateEnemiesAroundPoint(numberOfEnemies, positionTransform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        SpaceEnemies();
        PrintDebug();
    }

    void PrintDebug()
    {
        //int i = 0;
        //foreach (GameObject go in enemies)
        //{
        //    debugText.text += "> "+ i+ " "+go.name + "\n";
        //    i++;
        //}
        string s = "";
        for (int i=0; i < enemies.Count; i++)
        {
            s += "> " + i + " " + enemies[i].name + "\n";
        }
        debugText.text = s;

    }
    void SpaceEnemies()
    {
        if (enemies.Count < numberOfEnemies)
        {
            //print("Ring " + ringID + " has less enemies in it");
        }
        
    }
    public void RemoveEnemy (GameObject enemy)
    {
        // Removes enemy from the list
        if (enemies.Count >0)
        {
            enemies.Remove(enemy);
        }
    }
    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
    {

        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            //var radians = 2 * Mathf.PI / num * i; // objects overlap
            float radians = i * 2 * Mathf.PI / num; // no overlap

            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            Vector3 spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            //var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);
            enemy.name = ringID + " enemy " + i;

            enemies.Add(enemy);

            foreach (GameObject go in enemies)
            {
                go.transform.LookAt(point);
                go.transform.SetParent(transform);
                //go.transform.RotateAround(point, transform.forward, 90f);
            }


            /* Rotate the enemy to face towards player */
            //enemy.transform.LookAt(point);

            /* Adjust height */
            //enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
        }

        if (ringID == Ring.one) transform.RotateAround(point, transform.right, 90);
        if (ringID == Ring.two) transform.RotateAround(point, transform.forward, 90);
    }
}
