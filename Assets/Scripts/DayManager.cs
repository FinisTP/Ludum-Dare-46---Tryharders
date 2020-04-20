using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public DailyTransition[] list;
    public int day = 0;

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
            case DayStatus.mailspeech:
                return day.mailboxSpeech.sentences;
            case DayStatus.crowdspeech:
                return day.crowdSpeech.sentences;
        }
        return null;
    }
<<<<<<< Updated upstream
=======

    public void NextDay()
    {
        if (day < 5)
        {
            day++;
            ToggleDay();
        }
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
>>>>>>> Stashed changes
}
