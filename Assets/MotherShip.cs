using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject explosionVFX;
    public GameObject parentRingArray;

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentRingArray = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            print("Impact");
            Destroy(other.gameObject);
            gameManager.SetScore();

            GameObject exp = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(exp, 2f);
            gameManager.isMotherVisible = false;
            parentRingArray.SetActive(false);
        }
    }
    private void OnEnable()
    {
        //print("MOTHER SHIP SET ACTIVE");
    }
}
