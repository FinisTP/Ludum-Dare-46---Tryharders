﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public DailyTransition[] list;
    public int day = 0;

    public int food = 0;
    public bool gun = false;
    public bool knife = false;
    public int mask = 0; //2

    public bool choose1 = false;
    public bool choose2 = false;

    public int money = 200;

    public List<string> infectedName = new List<string>();

    public bool daytime = true;

    public DailyTransition getScript()
    {
        return list[day];
    }
    public List<string> getS(DayStatus d)
    {
        DailyTransition day = getScript();
        switch (d)
        {
            case DayStatus.n1speech:
                return day.neighbor1Speech.sentences;
            case DayStatus.n2speech:
                return day.neighbor2Speech.sentences;
            case DayStatus.wifespeech:
                return day.wifeSpeech.sentences;
            case DayStatus.sonspeech:
                return day.sonSpeech.sentences;
            case DayStatus.tvspeech:
                return day.tvSpeech.sentences;
        }
        return null;
    }

    public void NextDay()
    {
        if (day < 5)
        {
            day++;
            ToggleDay();
        }
        if (mask > 0) mask--;
    }

    public void ToggleDay()
    {
        if(day < 5)
        {
            daytime = true;
        }
    }
    public void ToggleNight()
    {
        if (day < 5)
        {
            daytime = false;
        }
    }
}
