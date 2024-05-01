using System;
using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isLoop;
    private bool isPlaying = false;
    private bool hasPlayed = false;

    private void Start()
    {
        if (isLoop)
            audioSource.loop = true;
        else
            audioSource.loop = false;
    }

    private void Update()
    {
        if (!audioSource.isPlaying && isPlaying)
        {
            isPlaying = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed) // Vérifier si le joueur entre en collision avec le cube et si la musique n'a pas encore été jouée
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
            if (isLoop)
                hasPlayed = true;
            else
                hasPlayed = false;
        }
    }
}