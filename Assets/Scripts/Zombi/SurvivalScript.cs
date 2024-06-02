using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SurvivalScript : MonoBehaviour
{
    // Variables related to the player and survival time
    public GameObject player;
    private float survivalTime = 5 * 60; // 5 minutes in seconds
    private float timer = 0;
    private Collider _hitCollider;
    public bool isTouchedByZombie = false;

    // Variables related to the death sequence
    public CanvasGroup fadeCanvasGroup; // Assign this in the inspector
    public TMP_Text deathText; // Assign this in the inspector
    public AudioSource audioSource; // Assign this in the inspector
    public AudioClip endMusic; // Assign this in the inspector
    private bool isDeathSequenceRunning = false;

    // Variables related to the fade in sequence
    public Image fadeImage; // Assign this in the inspector
    public TMP_Text surviveText; // Assign this in the inspector
    public float fadeDuration = 5f;

    private void Awake()
    {
        _hitCollider = player.GetComponent<Collider>();
    }

    private void Start()
    {
        StartCoroutine(StartFadeIn());
    }

    private void Update()
    {
        CheckCollision();
        if (!isTouchedByZombie)
        {
            timer += Time.deltaTime;
            if (timer >= survivalTime)
            {
                Debug.Log("Player has survived for 5 minutes!");
                // You can add any code here to indicate that the player has won
            }
        }
        else
        {
            Debug.Log("Player has been touched by a zombie!");
            if (!isDeathSequenceRunning)
            {
                StartCoroutine(DeathSequence());
            }
        }
    }

    private void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapBox(_hitCollider.bounds.center, _hitCollider.bounds.extents,
            _hitCollider.transform.rotation);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Zombi"))
            {
                isTouchedByZombie = true;
                break;
            }
        }
    }

    private IEnumerator StartFadeIn()
    {
        // Set the initial state
        fadeImage.color = Color.black;
        surviveText.text = "SURVIVE";
        surviveText.enabled = false;

        yield return StartCoroutine(FadeIn());

        // Disable the text after another 5 seconds
        yield return new WaitForSeconds(5f);
        surviveText.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            // Calculate the fraction of the total duration that has passed
            float t = fadeTimer / fadeDuration;

            // Use this fraction to interpolate between black and transparent
            fadeImage.color = Color.Lerp(Color.black, Color.clear, t);

            // Enable the text after half the duration has passed
            if (t > 0.5f && !surviveText.enabled)
            {
                surviveText.enabled = true;
            }

            fadeTimer += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is fully transparent and the text is enabled at the end
        fadeImage.color = Color.clear;
        surviveText.enabled = true;
    }

    private IEnumerator DeathSequence()
    {
        isDeathSequenceRunning = true;
        Time.timeScale = 0f; // Pause the game

        deathText.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 0f; // Set the alpha to 0

        float fadeDuration = 5f;
        float fadeTimer = 0f;

        // Define the initial and final font sizes
        float initialFontSize = deathText.fontSize;
        float finalFontSize = 150f; // Change this to the desired final font size

        while (fadeTimer < fadeDuration)
        {
            fadeCanvasGroup.alpha = fadeTimer / fadeDuration; // Fade in
            fadeTimer += Time.unscaledDeltaTime; // Use unscaledDeltaTime to continue the fade while the game is paused

            // Increase the font size gradually
            deathText.fontSize = Mathf.Lerp(initialFontSize, finalFontSize, fadeTimer / fadeDuration);

            yield return null;
        }

        fadeCanvasGroup.alpha = 1f; // Make sure the alpha is 1 at the end
        deathText.fontSize = finalFontSize; // Make sure the font size is the final size at the end
        audioSource.clip = endMusic;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(5f); // Use WaitForSecondsRealtime to wait while the game is paused
        SceneManager.LoadScene("MainCity");

        Time.timeScale = 1f; // Resume the game

        isDeathSequenceRunning = false;
    }
}