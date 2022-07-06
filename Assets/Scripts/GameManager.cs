using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Audio stuff")]
    [SerializeField] AudioClip[] invaderSFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMP_Text scoreText;

    int clipCounter = 0;
    private float audioTimer;
    [SerializeField] private float audioDelay = 1f;

    [Header("Mother ship stuff")]
    [SerializeField] private AudioClip motherSFX;
    private float mothershipTimer;
    [SerializeField] private float mothershipDelay = 5f;
    public bool isMotherVisible = false;
    [SerializeField] private GameObject mothershipPF;

    [SerializeField] private GameObject vhsPF;

    [Header("Planet")]
    [SerializeField] MeshRenderer planetMesh;
    [SerializeField] private TMP_Text healthText;

    [Header("Player = so can disable while exploding")]
    [SerializeField] private PlayerController playerController;

    Color planetColour;
    Material planetMaterial;

    private int planetHealth;
    public int PlanetHealth
    {
        get { return planetHealth; }
        set { planetHealth = value; }
    }

    private int score;

    [Header("Explosion Spawn Points")]
    [SerializeField] GameObject[] explosionSpawnPoints;
    [SerializeField] GameObject explosionPF;
    private bool isPlanetDead = false;
    private int maxExplosions = 10;
    private int explosionCounter = 0;
    private float explosionTimer;
    private float explosionDelay = 0.5f;


    private void Awake()
    {
        vhsPF.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetHealth = 5;
        score = 0;
        Material[] m = planetMesh.materials;
        planetMaterial = m[0];
        planetColour = new Color(0, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (planetHealth <= 0)
        {
            //print("Planet Dead");
        }
        HandleAudio();
        HandleMothership();
        HandleScoreText();
        HandlePlanetExplode();

        if (Input.GetKeyDown(KeyCode.C))
        {
            isPlanetDead = true;
        }
    }

    private void HandlePlanetExplode()
    {
        if (isPlanetDead)
        {
            // kill player
            playerController.PlayerIsDead();

            if (Time.time > explosionTimer)
            {
                if (explosionCounter <= maxExplosions)
                {
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
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandlePlanetHealth()
    {
        if (isPlanetDead) return;
        //planetHealth--;
        planetColour.g = planetColour.g - 0.001f;
        if (planetColour.g <= 0)
        {
            isPlanetDead = true;
        }
        planetMaterial.color = planetColour;

        // update text
        float health = Mathf.Round(planetColour.g * 100f);
        healthText.text = health.ToString() + "%";
    }

    void HandleScoreText()
    {
        scoreText.text = score.ToString();
    }
    public void SetScore()
    {
        score += 5;
    }

    private void HandleMothership()
    {
        // IF NOT VISIBLE
        if (!isMotherVisible)
        {
            float rand = Random.Range(0, 10000);
            if (rand > 9990f)
            //if (rand > 9990f)
            {
                print("MAKE MOTHER VISIBLE");
                audioSource.PlayOneShot(motherSFX);
                isMotherVisible = true;
                mothershipPF.SetActive(true);
                mothershipTimer = Time.time + mothershipDelay;
            }
            else
            {
                mothershipPF.SetActive(false);
                isMotherVisible = false;
            }
        }
        // IF VISIBLE
        else
        {
            if (Time.time > mothershipTimer)
            {

                isMotherVisible = false;
            }
        }



    }
    private void HandleAudio()
    {
        if (Time.time > audioTimer)
        {
            audioSource.PlayOneShot(invaderSFX[clipCounter]);
            audioTimer = Time.time + audioDelay;
            clipCounter++;
        }

        if (clipCounter >= invaderSFX.Length)
        {
            clipCounter = 0;
        }
    }
}
