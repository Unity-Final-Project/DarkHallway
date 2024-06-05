using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    private AudioSource mainAudioSource;

    public List<AudioClip> clipList;
    private Dictionary<string, AudioSource> playingAudioSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        mainAudioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
            instance.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void Init()
    {
        Debug.Log("Init AudioManager");
    }

    public void Play(string name)
    {
        // Check if the main audio source is already playing a clip
        if (mainAudioSource.isPlaying)
        {
            return;
        }

        AudioClip clip = GetClip(name);
        if (clip != null)
        {
            mainAudioSource.clip = clip;
            mainAudioSource.Play();
        }
    }

    public void Pause()
    {
        mainAudioSource.Pause();
    }

    public void Stop()
    {
        mainAudioSource.Stop();
    }

    public AudioClip GetClip(string name)
    {
        foreach (AudioClip clip in clipList)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }

    // New method to play multiple audio clips simultaneously
    public void PlayMultiple(string[] names)
    {
        foreach (string name in names)
        {
            AudioClip clip = GetClip(name);
            if (clip != null)
            {
                // Check if an AudioSource for this clip is already playing
                if (playingAudioSources.ContainsKey(name))
                {
                    continue; // Skip if already playing
                }

                // Create a new AudioSource component for each clip
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();

                // Add to the dictionary of playing audio sources
                playingAudioSources[name] = audioSource;

                //// Remove the AudioSource from the dictionary once it finishes playing
                StartCoroutine(DestroyAudioSourceAfterPlay(name, audioSource));
            }
        }
    }

    private IEnumerator DestroyAudioSourceAfterPlay(string name, AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        playingAudioSources.Remove(name);
        Destroy(audioSource);
    }

    // Method to pause multiple audio clips
    public void PauseMultiple(string[] names)
    {
        foreach (string name in names)
        {
            if (playingAudioSources.ContainsKey(name))
            {
                AudioSource audioSource = playingAudioSources[name];
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
            }
        }
    }

    // Method to stop multiple audio clips
    public void StopMultiple(string[] names)
    {
        while (playingAudioSources.Keys.Any(key => names.Contains(key)))
        {
            foreach (string name in names)
            {
                if (playingAudioSources.ContainsKey(name))
                {
                    AudioSource audioSource = playingAudioSources[name];
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                        playingAudioSources.Remove(name);
                        Destroy(audioSource);
                    }
                }
            }
        }
    }

}
