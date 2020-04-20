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
    public string sceneName = "OutdoorScene";
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        sentences = new Queue<string>();
    }

    public void StartText(TextBehaviour text) 
    {
        sentences.Clear();
        DayManager manager = null;
        if(GameObject.Find("Player")!=null) manager = GameObject.Find("Player").GetComponent<DayManager>();
        foreach(string sentence in text.sentences)
        {
            if (manager != null) if (manager.alreadyTypo.Contains(sentence)) { continue;
                } else manager.alreadyTypo.Add(sentence);
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
        if (isMonologue)
        {
            audio.Play();
        }
        foreach (char letter in sentence.ToCharArray())
        {
            currentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }

    void EndDialogue()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
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
