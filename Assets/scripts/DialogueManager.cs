using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float typingSpeed = 0.1f;
    public string[] sentences;

    private int index;
    private bool isTyping = false;

    void Start()
    {
        StartCoroutine(TypeSentence(sentences[index]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeSentence(sentences[index]));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        // You can add any logic here for ending the dialogue, e.g., closing the dialogue box, ending the scene, etc.
        Debug.Log("End of dialogue");
    }
}