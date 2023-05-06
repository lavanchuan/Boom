using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTime : MonoBehaviour
{
    public float timePlay;
    public bool endGame = false;

    private void Awake() {
        if(true){
            timePlay = 2.5f * 60;
        }
    }

    private void Start() {
        StartCoroutine(EffectTimePlay(timePlay));
    }

    IEnumerator EffectTimePlay(float effectTime){
        yield return new WaitForSeconds(effectTime);
        endGame = true;
        Camera.main.GetComponent<GameManager>().gamePlayState = GameDefine.GAMEPLAY_STATE.ENDGAME;
    }
}
