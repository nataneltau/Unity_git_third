using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public const string THEME = "Theme";

    private void Awake()
    {
        //make sure there is only one AudioManager each scene
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);//that way we will always have our same AudioManager


        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(THEME);
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, x => x.audioName == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    
}
