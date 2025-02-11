using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public GameObject dialogBox;
    public bool playerInRange;
    private bool isDialogueActive;
    public GameObject memory;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        dialogBox.SetActive(false); // Ensure the dialogue box is initially inactive
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogBox.SetActive(true);
        index = 0;
        textComponent.text = string.Empty; // Clear the text before starting the dialogue
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty; // Clear the text before typing the next line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogBox.SetActive(false);
        textComponent.text = string.Empty; // Clear the text
        index = 0; // Reset the index
        memory.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (isDialogueActive)
            {
                EndDialogue();
            }
        }
    }
}
