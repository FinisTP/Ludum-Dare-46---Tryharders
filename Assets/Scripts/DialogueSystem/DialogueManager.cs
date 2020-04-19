using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    public GameObject continueButton;

    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentence)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));       
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        string textDisplay = "";
        continueButton.SetActive(false);
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay += letter;
            dialogueText.text = textDisplay;
            yield return new WaitForSeconds(0.02f);
        }
        continueButton.SetActive(true);
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
