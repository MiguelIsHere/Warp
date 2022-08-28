using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This allows the custom class to show up in the inspector
public class Sound 
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)] // Only values between 0-1 can be put in this variable. Creates a slider that allows us to change the float value
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop; // Determines if this clip will loop unless stopped

    [HideInInspector] public AudioSource source;
}
