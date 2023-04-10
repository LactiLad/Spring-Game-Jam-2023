using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler instance;

    public AudioSource track1, track2;
    public bool track1Playing;
    public AudioClip day;
    public AudioClip night;
    public AudioClip dusk;
    public AudioClip title;

    void Start() {
        if (instance == null) instance = this;
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        track1Playing = true;
        track1.loop = true;
        track2.loop = true;

        switchTrack(title);
    }

    public void playDayTheme() {
        switchTrack(day);
    }

    public void playNightTheme() {
        switchTrack(night);
    }

    public void playDuskTheme() {
        switchTrack(dusk);
    }

    public void switchTrack(AudioClip newClip) {
        if (track1Playing) {
            track2.clip = newClip;
            track2.Play();
            track1.Stop();
        } else {
            track1.clip = newClip;
            track1.Play();
            track2.Stop();
        }

        track1Playing = !track1Playing;
    }
}
