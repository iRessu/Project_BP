using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name, int variation)
    {
        Sound[] selectedSounds = Array.FindAll(sounds, sound => sound.name == name);

        if(selectedSounds.Length == 0)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        if(variation < 0 || variation >= selectedSounds.Length)
        {
            Debug.Log("Variation index out of range for sound: " + name);
            return;
        }

        selectedSounds[variation].source.Play();
    }
}
