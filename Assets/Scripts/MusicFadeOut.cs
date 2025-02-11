using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour
{
    public AudioSource backgroundAudioSource;
    public float fadeOutDuration = 1f;
    private void Update()
    {
        //if(something happens then fade music)
        //StartCoroutine(FadeOutAudio(backgroundAudioSource, fadeOutDuration));
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
