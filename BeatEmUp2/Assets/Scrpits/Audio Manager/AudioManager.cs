using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // have a list of sounds to add or remove specific sounds
    public Sound[] sounds;

    public GameObject enemy, player;

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


    /// <summary>
    /// Basic Play function which plays a 2d sound
    /// </summary>
    /// <param name="name"></param>
    /// <param name="loop"></param>
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

    
    /// <summary>
    /// Ovveride function to play a specific sound and make it 3d
    /// </summary>
    /// <param name="name"></param>
    /// <param name="loop"></param>
    /// <param name="is3D"></param>
    public void Play(string name, bool loop, bool is3D)
    {
        //loop through all sounds and find the one with the appropiate name
        Sound s = Array.Find(sounds, sounds => sounds.name == name);             //used from "System" header

        //check to see if sound was found
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }



        //set to loop or not
        s.loop = loop;

        //make the sound 3d
        if(is3D)
        {
            if (name == "EnemySteps1" || name == "EnemySteps2")
            {

                AudioSource enemyAudio = enemy.GetComponent<AudioSource>();
                enemyAudio.clip = s.source.clip;



                enemyAudio.spatialBlend = 1f;
                enemyAudio.rolloffMode = AudioRolloffMode.Logarithmic;

                enemyAudio.minDistance = .5f;
                enemyAudio.maxDistance = 10f;
                enemyAudio.volume = 1;

                enemyAudio.Play();

               // s.source.volume = 0;
                

            }
        }
        else
            //Play Sound
            s.source.Play();



    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);

        //check to see if sound was found
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " not found!");
            return;
        }

        //Play Sound
        s.source.Stop();
    }
}
