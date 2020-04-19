using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    public Text currentText;
    public float textSpeed = 0.5f;
    public bool isMonologue = false;
    public Canvas transitionCanvas;
    private Queue<string> sentences;
    public string sceneName = "";

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartText(TextBehaviour text) 
    {
        sentences.Clear();

        foreach(string sentence in text.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        } else if (sentences.Count == 1 && isMonologue)
        {
            currentText.alignment = TextAnchor.MiddleCenter;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        currentText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            currentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        if (isMonologue)
        {
            if (sceneName.Length > 0) SceneManager.LoadScene(sceneName);
        }
        else
        {
            transitionCanvas.gameObject.SetActive(false);
        }
    }
}
