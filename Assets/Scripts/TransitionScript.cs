using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public GameObject transitionTrigger; // The GameObject that should be active to allow transition
    public string nextSceneName; // The name of the next scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && transitionTrigger.activeSelf)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
