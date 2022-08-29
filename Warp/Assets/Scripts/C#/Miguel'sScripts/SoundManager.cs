using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this; // If there is no SoundManager in scene, set this to be the instance
        }
        else
        {
            Destroy(gameObject); // If there is already a soundManager in this scene, destroy this gameObject
            return;
        }

        DontDestroyOnLoad(gameObject); // Prevents soundManager from getting destroyed when changing scenes

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // Sets the new audio source of this sound 
            s.source.clip = s.clip; // Sets the clip of audio source to the clip stored in the sound class we're referring to

            s.source.volume = s.volume; // Sets volume
            s.source.pitch = s.pitch; // Sets pitch

            s.source.loop = s.loop; // Sets whether this sound clip will loop after finishing
        }
    }

    private void Start()
    {
        //Play("SelectMusic");
    }

    public void Play(string name)
    {
        // Look through the sound array and find a sound where sound.name is the same as the name passed to this method.
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) // Do not play a song that isn't there.
        {
            Debug.LogWarning("Sound: " + name + " not found!"); // Prints a warning that this sound was not found
            return;
        }

        s.source.Play(); // Play the audio clip
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + " not found!");
            return;
        }

        if (s.source.isPlaying) //  If the clip is playing right now...
        {
            s.source.Stop(); // Stops playing the clip, used for clips that loop
        }
    }
}
