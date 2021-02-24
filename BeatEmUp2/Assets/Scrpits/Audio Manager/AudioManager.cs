using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // have a list of sounds to add or remove specific sounds
    public Sound[] sounds;

    private void Awake()
    {
       //loop through a lisT and for each sound
       foreach(Sound s in sounds)
        {
            //add audio source component
            s.source =  gameObject.AddComponent<AudioSource>();

            //assign the source clip
            s.source.clip = s.clip;

            //assign volume and pitch
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            //assign the loop
            s.source.loop = s.loop;
        }
    }

    public void Play(string name, bool loop)
    {
        //loop through all sounds and find the one with the appropiate name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);             //used from "System" header

        //check to see if sound was found
        if(s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }

        //Play Sound
        s.source.Play();

        //set to loop or not
        s.loop = loop;


    }
}
