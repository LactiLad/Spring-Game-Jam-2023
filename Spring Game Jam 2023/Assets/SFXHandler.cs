using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public static SFXHandler instance;
    public AudioSource Audio;
    public AudioClip AmberSpeech;
    public AudioClip NoirSpeech;
    public AudioClip MenuClick;
    public AudioClip PickUpNeedle;
    public AudioClip PlaceNeedle;
    public AudioClip ThreadNeedle;

    void Start()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void amberSpeech() {
        Audio.clip = AmberSpeech;
        Audio.Play();
    }

    public void noirSpeech() {
        Audio.clip = NoirSpeech;
        Audio.Play();
    }

    public void menuClick() {
        Audio.clip = MenuClick;
        Audio.Play();
    }

    public void pickUpNeedle() {
        Audio.clip = PickUpNeedle;
        Audio.Play();
    }

    public void placeNeedle() {
        Audio.clip = PlaceNeedle;
        Audio.Play();
    }

    public void threadNeedle() {
        Audio.clip = ThreadNeedle;
        Audio.Play();
    }
}
