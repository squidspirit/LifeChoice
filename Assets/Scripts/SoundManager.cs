using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public Sound[] sounds;

    void Awake() {

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name) {

        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Play();
    }

    public void Stop(string name) {

        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Stop();
    }

}

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
