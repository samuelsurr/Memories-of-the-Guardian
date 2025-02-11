using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionGhost : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject transitionTrigger; // The GameObject that should be active to allow transition
    public GameObject memoryP2;
    public GameObject flicker;
    public string nextSceneName; // The name of the next scene to load
    public AudioSource backgroundAudioSource; // The initial background music
    public AudioSource newAudioSource; // The new music to play during the flicker effect
    public float fadeOutDuration = 1f; // The duration of the fade-out effect
    public float flickerDuration = 2f; // Total duration of the flicker effect
    public float initialFlickerInterval = 0.5f; // Initial interval between flickers

    private void Start()
    {
        // Ensure the new audio source is not playing at the start
        newAudioSource.Stop();

        // Start the background music if not already playing
        if (!backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && transitionTrigger.activeSelf)
        {
            StartCoroutine(StartSceneTransition());
        }
    }

    private IEnumerator StartSceneTransition()
    {
        // Flicker the transition trigger and change the music
        yield return StartCoroutine(FlickerTransitionTrigger());

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FlickerTransitionTrigger()
    {
        rb.bodyType = RigidbodyType2D.Static;
        float elapsedTime = 0;
        float currentInterval = initialFlickerInterval;

        // Start the new music
        newAudioSource.Play();

        // Fade out the background music
        StartCoroutine(FadeOutAudio(backgroundAudioSource, fadeOutDuration));

        while (elapsedTime < flickerDuration)
        {
            flicker.SetActive(!flicker.activeSelf);
            yield return new WaitForSeconds(currentInterval);
            currentInterval *= .95f; // Reduce the interval to flicker faster
            elapsedTime += currentInterval;
        }

        flicker.SetActive(true); // Ensure it's active at the end
        memoryP2.SetActive(true);
        yield return new WaitForSeconds(8f);
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume to original for next use
    }
}
