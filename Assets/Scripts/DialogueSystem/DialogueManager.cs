﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private List<string> sentences;
    public bool continueButton;
    public int index = 0;
    string[] ans = null;
    void Start()
    {
        sentences = new List<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentence)
        {
            sentences.Add(sentence);
        }

        DisplayNextSentence();
    }
    public void returnAnswer(int i)
    {
        if (ans == null) return;
        index = int.Parse(ans[i].Split('/')[1]);
        resetAnswer();
        StartCoroutine(TypeSentence(sentences[index].Split('[')[0]));
        if (sentences[index].Contains("[")) index = int.Parse(sentences[index].Split('[')[1]);
        ans = null;
    }
    void resetAnswer()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject answer = GameObject.Find("Answer" + (i + 1));
            answer.SetActive(false);
        }
    }
    public void DisplayNextSentence()
    {
        if (ans != null) return;
        if(index >= sentences.Count)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences[index];
        if (sentence.Contains("|"))
        {
            ans = sentence.Split('|')[1].Split(',');
            for(int i = 0; i < ans.Length; i++)
            {
                GameObject answer = GameObject.Find("Answer" + (i+1));
                answer.SetActive(true);
                answer.transform.GetChild(0).GetComponent<Text>().text = ans[i].Split('/')[0].Split('[')[0];
            }
        }
        if (sentence.Contains("["))
        {
            index = int.Parse(sentence.Split('[')[1]);
        }
        else index++;
        StartCoroutine(TypeSentence(sentence.Split('|')[0].Split('[')[0]));
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        string textDisplay = "";
        continueButton = false;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay += letter;
            dialogueText.text = textDisplay;
            yield return new WaitForSeconds(0.02f);
        }
        continueButton = true;
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
