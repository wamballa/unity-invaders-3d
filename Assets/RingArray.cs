// https://answers.unity.com/questions/1661755/how-to-instantiate-objects-in-a-circle-formation-a.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingArray : MonoBehaviour
{
    public GameObject enemyPrefab;
    [Range(0, 10)]
    public int numberOfEnemies;
    public Transform positionTransform;
    public float radius;

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

            enemies.Add(enemy);

            foreach (GameObject go in enemies)
            {
                go.transform.LookAt(point);
                go.transform.SetParent(transform);
                //go.transform.RotateAround(point, transform.forward, 90f);
            }

            transform.RotateAround(point, transform.right, 90f);
            /* Rotate the enemy to face towards player */
            //enemy.transform.LookAt(point);

            /* Adjust height */
            //enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
        }
    }


}
