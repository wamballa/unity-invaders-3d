using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    bool isMotherVisible = false;
    [SerializeField] private GameObject mothershipPF;

    private int planetHealth;
    public int PlanetHealth
    {
        get { return planetHealth; }
        set { planetHealth = value; }
    }

    private int score;


    // Start is called before the first frame update
    void Start()
    {
        planetHealth = 5;
        score = 0;
        //audioSource = transform.GetComponent<AudioSource>();
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
            {
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
