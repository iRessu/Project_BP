using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

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
            s.originalVolume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayRandomSound(string name, int variation)
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

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void ShutUpEggBen()
    {
        Sound eggBen = Array.Find(sounds, sound => sound.name == "EggBen");
        if(eggBen != null && eggBen.source.isPlaying)
        {
            eggBen.source.Stop();
        }
    }

    public void StopMenuMusic()
    {
        Sound menuMusic = Array.Find(sounds, sound => sound.name == "MenuMusic");
        if(menuMusic != null && menuMusic.source.isPlaying)
        {
            StartCoroutine(FadeOut(menuMusic, 1f));
        }
    }

    private IEnumerator FadeOut(Sound sound, float fadeTime)
    {
        AudioSource audioSource = sound.source;
        float startVolume = sound.originalVolume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
