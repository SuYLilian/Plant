using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;

    public AudioSource audioSource;
    //public static AudioManager instance = null;



    public void PlayClip_BGM(AudioClip _clip)
    {
        audioSource.PlayOneShot(_clip);
    }
}
