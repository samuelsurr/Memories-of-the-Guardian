using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VICTORY : MonoBehaviour
{
    public GameObject transitionTrigger; // The GameObject that should be active to allow transition
    public string nextSceneName; // The name of the next scene to load
    public GameObject hideDoor; // The door that appears upon boss defeat

    private void Awake()
    {
        hideDoor.SetActive(false); // Initially hide the door
    }

    private void Update()
    {
        // Check if the transitionTrigger (boss) is inactive
        if (!transitionTrigger.activeInHierarchy)
        {
            hideDoor.SetActive(true); // Show the door
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hideDoor.activeInHierarchy)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
