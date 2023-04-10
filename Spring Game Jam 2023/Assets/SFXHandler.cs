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
        PlayOneShot(AmberSpeech);
    }

    public void noirSpeech() {
        PlayOneShot(NoirSpeech);
    }

    public void menuClick() {
        PlayOneShot(MenuClick);
    }

    public void pickUpNeedle() {
        PlayOneShot(PickUpNeedle);
    }

    public void placeNeedle() {
        PlayOneShot(PlaceNeedle);
    }

    public void threadNeedle() {
        PlayOneShot(ThreadNeedle);
    }
}
