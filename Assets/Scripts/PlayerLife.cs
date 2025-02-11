using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public SignalSender playerHealthSignal;
    public FloatValue currentHealth;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (currentHealth.RuntimeValue <= 0)
        {
            Die();
        }
    }




    private void Die()
    {
        
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");

            StartCoroutine(RestartLevel());
        
    }

    private IEnumerator RestartLevel()
    {
        // Wait for a specific duration (e.g., 2 seconds)
        yield return new WaitForSeconds(2.0f);
        gameObject.layer = LayerMask.NameToLayer("Default");
        // Restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentHealth.RuntimeValue = currentHealth.initialValue; // Reset health to initial value
    }
}
