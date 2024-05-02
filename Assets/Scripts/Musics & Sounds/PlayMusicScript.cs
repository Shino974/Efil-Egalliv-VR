using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[Tooltip("Play a sound or a music when the player triggers something")]
public class PlayMusicScript : MonoBehaviour
{
    public enum Mode
    {
        Normal,
        Loop,
        PlayOnShot,
        MultiMusic
    }

    [Tooltip("Define the mode of the music")]
    public Mode mode;

    [Tooltip("The volume of the sound")]
    public float volume = 1.0f;

    [Tooltip("Set to true for 3D spatial blend, false for 2D")]
    public bool is3DSpatialBlend;

    [Tooltip("List of music clips for MultiMusic mode")]
    public AudioClip[] musicClips;

    private AudioSource _audioSource;
    private bool _hasPlayed = false;
    private Collider _collider;
    private int _currentClipIndex = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = volume;
        _audioSource.spatialBlend = is3DSpatialBlend ? 1.0f : 0.0f;
        _collider = GetComponent<Collider>();

        if (mode == Mode.Loop)
        {
            _audioSource.loop = true;
        }
        else if (mode == Mode.MultiMusic && musicClips.Length > 0)
        {
            _audioSource.clip = musicClips[_currentClipIndex];
        }
    }

    private void Update()
    {
        if (!_audioSource.isPlaying && _hasPlayed)
        {
            if (mode == Mode.PlayOnShot)
            {
                Destroy(_collider);
            }
            else if (mode == Mode.MultiMusic && musicClips.Length > 0)
            {
                _currentClipIndex = (_currentClipIndex + 1) % musicClips.Length;
                _audioSource.clip = musicClips[_currentClipIndex];
                _audioSource.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_audioSource.isPlaying)
        {
            _audioSource.Play();
            _hasPlayed = true;
        }
    }
}