using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientMusicManager : MonoBehaviour{
    
    private AudioSource _ambientMusic;

    private void Awake(){
        _ambientMusic = GetComponent<AudioSource>();
    }


    public void StartAmbientMusic(){
        _ambientMusic.Play();
    }

    public void StopAmbientMusic(){
        _ambientMusic.Stop();
    }

}
