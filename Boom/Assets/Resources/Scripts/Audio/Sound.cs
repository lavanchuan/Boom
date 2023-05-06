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
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlaySound(GAME_START);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string state){
        audio.PlayOneShot((AudioClip)Resources.Load("Audios/sound_" + state));
    }
}
