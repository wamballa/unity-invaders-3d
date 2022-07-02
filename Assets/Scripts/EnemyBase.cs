using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    //public AudioClip fireSFX;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private AudioClip explosionSFX;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = transform.GetComponentInParent<AudioSource>();

        if (audioSource == null) print("ERROR: null audiosource");
        if (explosionSFX == null) print("ERROR: null SFX");
    }

    public void ExplodeMe()
    {
        audioSource.PlayOneShot(explosionSFX);
        GameObject exp = Instantiate(
            explosionVFX,
            transform.position,
            Quaternion.identity);
        Destroy(exp, 1);
    }



}
