using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float sentenceTimeGap = 0.5f;
    [SerializeField] private string[] dialogueSentences;
    [SerializeField] private string[] dialogueHeaders;

    private int index = 0;
    private bool isDialogueFinished = false;

    void Start()
    {
        //StartDialogue();
    }

    public void StartDialogue()
    {
        isDialogueFinished = false;
        StartCoroutine(TypeDialogue(dialogueSentences));
    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        //{
        //    DisplayNextSentence();
        //}

        if (isDialogueFinished)
        {
            EndDialogue();
            isDialogueFinished = false;
        }
    }

    //public void DisplayNextSentence()
    //{
    //    if (index < dialogueSentences.Length - 1)
    //    {
    //        index++;
    //        dialogueText.text = "";
    //        StartCoroutine(TypeSentence(dialogueSentences[index]));
    //    }
    //    else
    //    {
    //        EndDialogue();
    //    }
    //}

    IEnumerator TypeDialogue(string[] sentences)
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            string sentence = sentences[i];
            dialogueText.text = dialogueHeaders[i];

            yield return TypeSentence(sentence);
            yield return new WaitForSeconds(sentenceTimeGap);
        }
        isDialogueFinished = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        // You can add any logic here for ending the dialogue, e.g., closing the dialogue box, ending the scene, etc.
        Debug.Log("End of dialogue");
        dialogueText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}