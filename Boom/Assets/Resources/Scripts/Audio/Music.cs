using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static readonly string WAITING = "waiting";
    public static readonly string TURTLE = "turtle";
    public static readonly string TURTLE_BOSS = "turtle_boss";
    public static readonly string QUEEN_BOSS = "queen_boss";
    public static readonly string ROOM = "room";

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = FunctionMethod.GetMusicVolume();
        
        //
        LoadClip(SceneManager.GetActiveScene().name);
        audioSource.Play();
    }

    void Update()
    {
        
    }

    void LoadClip(string nameScene){
        if(nameScene == "MainMenu"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + WAITING);
        } else if(nameScene == "SettingScene"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + WAITING);
        } else if(nameScene == "TurtleBoss01" || nameScene == "TurtleBoss02"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + TURTLE);
        } else if(nameScene == "TurtleBoss03"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + TURTLE_BOSS);
        } else if(nameScene == "QueenBoss01"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + QUEEN_BOSS);
        } else if(nameScene == "MapChoesScene"){
            audioSource.clip = (AudioClip)Resources.Load("Audios/music_" + ROOM);
        }

    }
}
