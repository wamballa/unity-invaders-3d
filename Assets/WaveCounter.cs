using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveCounter : MonoBehaviour
{

    public GameObject[] invaderGroups;
    public int[] numInvadersInRing;

    public TMP_Text waveText1;

    int numberOfArrays;
    bool areInvadersAllDead = false;

    public AudioSource audioSource;
    public AudioClip winningMusic;
    bool isWinMusicPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        int ring = 0;
        numInvadersInRing = new int[7];
    }

    // Update is called once per frame
    void Update()
    {
        HandleInvaderNumbers();
        CheckNoInvaders();
        HandleAllInvadersDead();
    }
    void HandleAllInvadersDead()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Invader");
            foreach(GameObject g in invaders)
            {
                g.GetComponentInParent<BaseRingArray>().RemoveUnit(g);
                Destroy(g);
            }
        }
        if (areInvadersAllDead)
        {
            StartCoroutine(GameWon());
        }
    }

    IEnumerator GameWon()
    {
        if (!isWinMusicPlaying)
        {
            isWinMusicPlaying = true;
            audioSource.PlayOneShot(winningMusic);
        }
        print("YOU WON");
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Win");
    }
    void CheckNoInvaders()
    {
        int count = 0;
        for (int i = 0; i < 7; i++)
        {
            count += numInvadersInRing[i];
        }
        if (count == 0)
        {
            areInvadersAllDead = true;
        }
        else areInvadersAllDead = false;
    }
    void HandleInvaderNumbers()
    {
        // Invader Group 0
        BaseRingArray[] rings = invaderGroups[0].GetComponentsInChildren<BaseRingArray>();
        for (int c = 0; c < rings.Length; c++)
        {
            numInvadersInRing[c] = rings[c].GetNumberOfInvaders();
        }
        rings = invaderGroups[1].GetComponentsInChildren<BaseRingArray>();
        for (int c = 0; c < rings.Length; c++)
        {
            numInvadersInRing[c] = numInvadersInRing[c] + rings[c].GetNumberOfInvaders();
        }
        rings = invaderGroups[2].GetComponentsInChildren<BaseRingArray>();
        for (int c = 0; c < rings.Length; c++)
        {
            numInvadersInRing[c] = numInvadersInRing[c] + rings[c].GetNumberOfInvaders();
        }

        waveText1.text = "";
        for (int i = 0; i < 7; i++)
        {
            bool isVisible = rings[i].GetIsRingVisble();
            int waveNum = i + 1;
            if (isVisible)
            {
                waveText1.text += "> Wave " + waveNum + " " + numInvadersInRing[i] + "\n";
            }
            else
            {
                waveText1.text += "  Wave " + waveNum + " " + numInvadersInRing[i] + "\n";
            }
        }
    }
}
