using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[Tooltip("Play a sound or a music when the player triggers an object")]
public class MusicTrigger : MonoBehaviour
{
    [Tooltip("Define is the music is a loop or not")]
    public bool isLoop = false;
    [Tooltip("The volume of the sound")]
    public float volume = 1.0f;
    
    private AudioSource _audioSource;
    private bool _isPlaying = false;
    private bool _hasPlayed = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = isLoop ? true : false;
        _audioSource.volume = volume;
    }

    private void Update()
    {
        if (!_audioSource.isPlaying && _isPlaying)
            _isPlaying = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasPlayed) // Vérifier si le joueur entre en collision avec le cube et si la musique n'a pas encore été jouée
        {
            if (!_isPlaying)
            {
                _audioSource.Play();
                _isPlaying = true;
            }
            if (isLoop)
                _hasPlayed = true;
            else
                _hasPlayed = false;
        }
    }
}