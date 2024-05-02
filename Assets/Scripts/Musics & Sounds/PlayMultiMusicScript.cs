using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("Play multiple music tracks in a row")]
public class PlayMultiMusicScript : MonoBehaviour
{
    [Tooltip("The music list that are played")]
    public List<AudioClip> musicTracks;
    [Tooltip("Loop all tracks when the last one is played")]
    public bool loopAllTracks; // Added loop option

    private AudioSource _audioSource;
    private int _currentTrackIndex = 0;

    private void Awake()
    {
        musicTracks = new List<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (musicTracks.Count > 0)
        {
            PlayTrack(_currentTrackIndex);
        }
    }

    public void PlayNextTrack()
    {
        _currentTrackIndex = (_currentTrackIndex + 1) % musicTracks.Count;
        if (_currentTrackIndex == 0 && !loopAllTracks)
        {
            return;
        }
        PlayTrack(_currentTrackIndex);
    }

    private void PlayTrack(int trackIndex)
    {
        _audioSource.clip = musicTracks[trackIndex];
        _audioSource.Play();
    }
}