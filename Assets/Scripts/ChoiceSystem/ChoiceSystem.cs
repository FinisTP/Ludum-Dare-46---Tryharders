using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ChoiceSystem : MonoBehaviour
{
    public Image choiceBox;
    public Choices choiceList;
    public Button[] buttonText;
    public Text[] choiceText;

    private string name;

    void Start()
    {
        ///choiceBox.gameObject.SetActive(false);
    }

    public void EnableChoices()
    {
        choiceBox.gameObject.SetActive(true);
        for (int i = 0; i < choiceList.choices.Length; ++i)
        {
            buttonText[i].gameObject.SetActive(true);
            //buttonText[i].onClick.AddListener((index));

            choiceText[i].gameObject.SetActive(true);
            choiceText[i].text = choiceList.choices[i];
            //Debug.Log(choiceList.responses.Length);
            
        }
    }

    public void InsertSentenceToDialogue(int index)
    {
        gameObject.GetComponent<DialogueTrigger>().dialogue.sentence = choiceList.responses[index].sentence;
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        DisableChoices();
    }

    void DisableChoices()
    {
        for (int i = 0; i < choiceList.choices.Length; ++i)
        {
            buttonText[i].onClick.RemoveAllListeners();
            choiceText[i].gameObject.SetActive(false);
            buttonText[i].gameObject.SetActive(false);
        }
        choiceBox.gameObject.SetActive(false);
    }
}
