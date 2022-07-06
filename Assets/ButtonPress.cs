using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject vhsFX;

    bool isClipPlaying = false;

    private void Start()
    {
        vhsFX.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(NextScene());
        }
    }
    private IEnumerator NextScene()
    {
        if (!isClipPlaying)
        {
            audioSourceMusic.Stop();
            audioSource.PlayOneShot(audioClip);
            isClipPlaying = true;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");
    }
}
