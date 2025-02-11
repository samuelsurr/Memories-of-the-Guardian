using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPopUp : MonoBehaviour
{
    public GameObject PopUp;
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           PopUp.SetActive(true);
        }
    }
}
