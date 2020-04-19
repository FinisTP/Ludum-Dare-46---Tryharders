using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Day", menuName ="Day")]
public class DailyTransition : ScriptableObject
{
    public int dayNumber;

    // Day section
    public TextBehaviour familyTransitionDay;
    public TextBehaviour neighborhoodTransitionDay;
    public TextBehaviour downtownTransitionDay;

    // Night section
    public TextBehaviour familyTransitionNight;
    public TextBehaviour neighborhoodTransitionNight;
    public TextBehaviour downtownTransitionNight;

    // Speech
    public TextBehaviour neighbor1Speech;
    public TextBehaviour neighbor2Speech;
    public TextBehaviour wifeSpeech;
    public TextBehaviour sonSpeech; 
    public TextBehaviour tvSpeech;
    public TextBehaviour mailboxSpeech;
    public TextBehaviour crowdSpeech;
}
