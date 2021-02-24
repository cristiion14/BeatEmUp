using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    //class which holds info about the sound 

    public string name;
    public AudioClip clip;

    public bool loop;

    //sliders for volume and pitch
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
