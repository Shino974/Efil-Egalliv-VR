using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[Tooltip("Play a narration when the player enters the trigger")]
public class NarrationScript : MonoBehaviour
{
    [Tooltip("Introduction of the narration")]
    [SerializeField] private AudioClip clipIntro;
    
    [Tooltip("First medal narration")]
    [SerializeField] private AudioClip clipFirstMedal;
    
    [Tooltip("Second medal narration")]
    [SerializeField] private AudioClip clipSecondMedal;
    
    [Tooltip("Outro narration")]
    [SerializeField] private AudioClip clipOutro;
    
    [Tooltip("First fail narration")]
    [SerializeField] private AudioClip clipFirstFail;
    
    [Tooltip("Second fail narration")]
    [SerializeField] private AudioClip clipSecondFail;
    
    [Tooltip("Third fail narration")]
    [SerializeField] private AudioClip clipThirdFail;
    
    [Tooltip("The barrier for the dummy scene")]
    [SerializeField] private GameObject barrierDummy;
    
    [Tooltip("The barrier for the survive scene")]
    [SerializeField] private GameObject barrierSurvive;
    
    [Tooltip("The barrier for the boss scene")]
    [SerializeField] private GameObject barrierBoss;
    
    [SerializeField] private GameObject[] medals; 
    
    private AudioSource _audioSource;
    private int _medalCount;

    private void Awake()
    {
        HandleBarrier();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.LogError("Audio source not found" + this.gameObject.name);
        _audioSource.spatialBlend = 0.0f;
    }

    private void HandleBarrier()
    {
        _medalCount = PlayerPrefs.GetInt("MedalCount");
        if (_medalCount == 0 || _medalCount == 4)
        {
            barrierDummy.SetActive(false);
            barrierSurvive.SetActive(true);
            barrierBoss.SetActive(true);
        }
        else if (_medalCount == 1 || _medalCount == 5)
        {
            barrierDummy.SetActive(true);
            barrierSurvive.SetActive(false);
            barrierBoss.SetActive(true);
        }
        else if (_medalCount == 2 || _medalCount == 6)
        {
            barrierDummy.SetActive(false);
            barrierSurvive.SetActive(false);
            barrierBoss.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("MedalCount") == 3)
        {
            barrierDummy.SetActive(false);
            barrierSurvive.SetActive(false);
            barrierBoss.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_audioSource.isPlaying)
        {
            if (PlayerPrefs.GetInt("MedalCount") == 0)
            {
                _audioSource.clip = clipIntro;
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 1)
            {
                _audioSource.clip = clipFirstMedal;
                medals[0].SetActive(true);
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 2)
            {
                _audioSource.clip = clipSecondMedal;
                medals[1].SetActive(true);
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 3)
            {
                _audioSource.clip = clipOutro;
                medals[2].SetActive(true);
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 4)
            {
                _audioSource.clip = clipFirstFail;
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 5)
            {
                _audioSource.clip = clipSecondFail;
                _audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("MedalCount") == 6)
            {
                _audioSource.clip = clipThirdFail;
                _audioSource.Play();
            }
        }
    }
}
