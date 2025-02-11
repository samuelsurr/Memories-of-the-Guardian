using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    // List of positions to teleport to, set these in the Inspector
    public Vector3[] teleportPositions;
    [SerializeField] private SimpleFlash flashEffect;
    public FloatValue enemyHealth; // Ensure this is a ScriptableObject
    // Index of the current teleport position
    public GameObject enemy;
    private int currentTeleportIndex = 0;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyHealth.RuntimeValue = enemyHealth.initialValue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Swing"))
        {
            // Check and reduce health before teleporting
            if (enemyHealth.RuntimeValue > 0)
            {
                enemyHealth.RuntimeValue -= 1;
                flashEffect.Flash();
                Teleport();
                Debug.Log(enemyHealth.initialValue);
            }
            if (enemyHealth.RuntimeValue == 0)
            {
                
                Die();
            }

        }
    }
    public void Die()
    {

        enemy.layer = LayerMask.NameToLayer("IgnoreCollision");
        anim.SetTrigger("dead");
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2.0f);
        enemy.SetActive(false);
    }
    // Method to teleport the character to the next position
    private void Teleport()
    {
        if (teleportPositions.Length == 0)
        {
            Debug.LogWarning("No teleport positions set.");
            return;
        }

        // Teleport to the current position
        transform.position = teleportPositions[currentTeleportIndex];

        // Move to the next position, looping back to the start if necessary
        currentTeleportIndex = (currentTeleportIndex + 1) % teleportPositions.Length;
    }
}
