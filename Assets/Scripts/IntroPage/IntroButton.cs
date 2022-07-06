using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButton : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSource;
    public AudioClip audioClip;

    bool isClipPlaying = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToNextScene()
    {
        StartCoroutine(NextScene());

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

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
