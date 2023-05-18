using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource audio;
    public static string TAG = "Sound";
    public static string PLAYER_DIE = "die";
    public static string GAME_START = "start";
    public static readonly string COIN = "coin";
    public static readonly string WIN = "win";
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = FunctionMethod.GetSoundVolume();
    }

    // Update is called once per frame
    void Update()
    {
        try{
            if(Camera.main.GetComponent<GameManager>().GetIsPause()){
                audio.Pause();
            }
        } catch (Exception){}
    }

    public void PlaySound(string state){
        audio.PlayOneShot((AudioClip)Resources.Load("Audios/sound_" + state));
    }
    public void PauseSound(){
        audio.Pause();
    }
}
