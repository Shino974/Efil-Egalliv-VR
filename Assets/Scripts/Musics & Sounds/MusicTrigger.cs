using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isLoop;
    private bool hasPlayed = false;

    private void Start()
    {
        if (isLoop)
            audioSource.loop = true;
        else
            audioSource.loop = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed) // Vérifier si le joueur entre en collision avec le cube et si la musique n'a pas encore été jouée
        {
            audioSource.Play();
            if (isLoop)
                hasPlayed = true;
            else
                hasPlayed = false;
        }
    }
}