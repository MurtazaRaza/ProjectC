using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.BaseClasses;

public class AudioHolder : UnitySingleton<AudioHolder>
{
    public AudioClip cardPlaceAudio;
    public AudioClip cardFlipAudio;
    public AudioClip correctAudio;
    public AudioClip incorrectAudio;
    public AudioClip gameComplete;
}
