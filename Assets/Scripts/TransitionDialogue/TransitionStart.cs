﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayStatus
{
    familyDay,
    familyNight,
    neighborhoodDay,
    neighborhoodNight,
    downtownDay,
    downtownNight,
    n1speech,
    n2speech,
    wifespeech,
    sonspeech,
    tvspeech,
    downtownwgun,
    downtownnogun
}

public class TransitionStart : MonoBehaviour
{
    public DailyTransition day;
    public TextTrigger transitionText;
    public DayStatus dayStatus;

    void Start()
    {
        StartCoroutine(WaitForLoad());
    }
    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
        reload();
    }

    public void reload()
    {
        transitionText.text.sentences.Clear();
        day = GameObject.Find("Player").GetComponent<DayManager>().getScript();
        switch (dayStatus)
        {
            case DayStatus.familyDay:
                foreach (string sentence in day.familyTransitionDay.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;

            case DayStatus.familyNight:
                foreach (string sentence in day.familyTransitionNight.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;

            case DayStatus.neighborhoodDay:
                foreach (string sentence in day.neighborhoodTransitionDay.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;

            case DayStatus.neighborhoodNight:
                foreach (string sentence in day.neighborhoodTransitionNight.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;

            case DayStatus.downtownDay:
                foreach (string sentence in day.downtownTransitionDay.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;

            case DayStatus.downtownNight:
                foreach (string sentence in day.downtownTransitionNight.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;
            case DayStatus.downtownwgun:
                foreach (string sentence in day.downtownWithGun.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;
            case DayStatus.downtownnogun:
                foreach (string sentence in day.downtownWithoutGun.sentences)
                    transitionText.text.sentences.Add(sentence);
                break;
        }
    }
}
