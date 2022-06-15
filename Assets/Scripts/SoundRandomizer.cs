using System;
using System.Linq;
using Essentials;
using UnityEngine;

public static class SoundRandomizer
{
    public static AudioClip LoadAudio(string[] names)
    {
        string audioName = names.ToList().PickRandomElement();
        AudioClip audio = Resources.Load<AudioClip>($"Sounds/{audioName}");
        if (audio == null)
            throw new NullReferenceException($"There is no {audioName} audio clip");
        return audio;
    }
}