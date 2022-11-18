using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioS;
    public AudioClip[] audioClips;
    public static AudioManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
        audioS = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioC)             //Plays audioClip on the audioSource
    {
        audioS.PlayOneShot(audioC);
    }
   
}
