using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPause : MonoBehaviour {

    AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    void Update() {
        if(Time.timeScale == 0 && source.isPlaying) {
            source.Pause();
        }else if(Time.timeScale > 0 && !source.isPlaying) {
            source.UnPause();
        }
        source.pitch = Time.timeScale;
    }
}
