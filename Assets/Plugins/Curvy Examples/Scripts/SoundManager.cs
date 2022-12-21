using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audio_Clip;
    AudioSource audio_Source;
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        audio_Source = gameObject.GetComponent<AudioSource>();
    }
    public void PlaySound(int id)
    {
        audio_Source.PlayOneShot(audio_Clip[id]);
    }
}
