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
    private List<string> sentences;
    public string sceneName = "OutdoorScene";
    private AudioSource audio;
    private bool isTyping = false;
    private int i = -1;
    public DayManager manager = null;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        sentences = new List<string>();
    }

    public void StartText(TextBehaviour text) 
    {
        sentences.Clear();
        manager = null;
        foreach(string sentence in text.sentences)
        {
            sentences.Add(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0 || i == sentences.Count-1)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        if (isTyping)
        {
            currentText.text = sentences[i];
            isTyping = false;
            if (audio.isPlaying) audio.Stop();
        } else
        {
            i++;
            if (i < sentences.Count)
            StartCoroutine(TypeSentence(sentences[i]));
        }
        
    }

    IEnumerator TypeSentence (string sentence)
    {
        if (GameObject.Find("Player") != null) manager = GameObject.Find("Player").GetComponent<DayManager>();
        if (isMonologue || (manager != null && !manager.alreadyTypo.Contains(sentence)))
            {
                currentText.text = "";
                if (isMonologue)
                {
                    audio.Play();
                }
                isTyping = true;
                foreach (char letter in sentence.ToCharArray())
                {
                    currentText.text += letter;
                    yield return new WaitForSeconds(textSpeed);
                }

                isTyping = false;
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
            }
        manager.alreadyTypo.Add(sentence);
        
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
