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
    public List<GameObject> gameObjects;
    void Start()
    {
        sentences = new List<string>();
        resetAnswer();
    }
    public void StartDialogue(Dialogue dialogue,bool random)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        if (!random)
            foreach (string sentence in dialogue.sentence)
            {
                sentences.Add(sentence);
            }
        else sentences.Add(dialogue.sentence[Random.Range(0, dialogue.sentence.Length)]);
        DisplayNextSentence();
    }
    public void returnAnswer(int i)
    {
        if (ans == null) return;
        
        index = int.Parse(ans[i].Split('/')[1]);
        resetAnswer();
        ans = null;
        if (sentences[index].Contains("|"))
        {
            ans = sentences[index].Split('|')[1].Split(',');
            for (int k = 0; k < ans.Length; k++)
            {
                GameObject answer = gameObjects[k];
                answer.SetActive(true);
                answer.transform.GetChild(0).GetComponent<Text>().text = ans[k].Split('/')[0].Split('[')[0];
            }
        }
        if (sentences[index].Contains(":food:"))
        {
            sentences[index] = sentences[index].Replace(":food:", "");
            GameObject.Find("Player").GetComponent<DayManager>().food++;
            sentences.Insert(index, "You receive food enough for 1 day["+(index+1));
        }
        StartCoroutine(TypeSentence(sentences[index].Split('[')[0]));
        if (sentences[index].Contains("[")) index = int.Parse(sentences[index].Split('/')[0].Split('[')[1]);
        
    }
    void resetAnswer()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject answer = gameObjects[i];
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
                GameObject answer = gameObjects[i];
                answer.SetActive(true);
                answer.transform.GetChild(0).GetComponent<Text>().text = ans[i].Split('/')[0].Split('[')[0];
            }
        }
        if (sentence.Contains("["))
        {
            index = int.Parse(sentence.Split('[')[1]);
        }
        if (sentence.Contains(":food:"))
        {
            sentence = sentence.Replace(":food:","");
            GameObject.Find("Player").GetComponent<DayManager>().food++;
            sentences.Insert(index, "You receive food enough for 1 day[" + (index + 1));
        }
        else index++;
        StartCoroutine(TypeSentence(sentence.Split('|')[0].Split('[')[0]));
    }
    public void sendMessage(string author,string message)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = author;
        if(sentences != null) sentences.Clear();
        StartCoroutine(TypeSentence(message));
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
        if (!continueButton) return;
        animator.SetBool("IsOpen", false);
        index = 0;
        ans = null;
        resetAnswer();
    }
}
