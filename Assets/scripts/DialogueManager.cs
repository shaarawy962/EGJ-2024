using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float sentenceTimeGap = 0.5f;
    [SerializeField] private string[] dialogueSentences;
    [SerializeField] private string[] dialogueHeaders;
    [SerializeField] private string MainMapSceneName;
    [SerializeField] private int MainMapSceneBuildIndex;

    bool bShouldSkip;

    public Action DialogueEnded;

    private int index = 0;
    private bool isDialogueFinished = false;

    void Start()
    {
        //StartDialogue();
        bShouldSkip = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && !bShouldSkip) {
            bShouldSkip = true;
        }

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
            dialogueText.text = dialogueHeaders[i] + sentence;
            bShouldSkip = false;
            yield return new WaitForSeconds(sentenceTimeGap);
        }
        isDialogueFinished = true;
        DialogueEnded.Invoke();
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            if (bShouldSkip)
            {
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    void EndDialogue()
    {
        // You can add any logic here for ending the dialogue, e.g., closing the dialogue box, ending the scene, etc.
        Debug.Log("End of dialogue");
        dialogueText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            //SceneManager.LoadScene(MainMapSceneBuildIndex);
            SceneManager.LoadScene(MainMapSceneName);
        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void OnNextButtonPressed()
    {

    }
}