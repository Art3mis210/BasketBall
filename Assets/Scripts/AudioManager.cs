using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioS;
    public static AudioManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
        audioS = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioC)
    {
        audioS.PlayOneShot(audioC);
    }
   
}
