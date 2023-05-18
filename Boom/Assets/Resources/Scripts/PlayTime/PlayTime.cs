using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    public float timePlay;
    public bool endGame = false;
    public Text textTimer;
    public GameObject playerGameObject, pauseGameObject;
    Player player;
    PauseGameObject pause;
    float timer;
    bool changedColor;

    private void Awake() {
        if(true){
            timePlay = 5f * 60;
        }
    }

    private void Start() {
        StartCoroutine(EffectTimePlay(timePlay));
        player = playerGameObject.GetComponent<Player>();
        pause = pauseGameObject.GetComponent<PauseGameObject>();
    }

    private void Update() {
        if(pause.GetPause() || player == null) return;
        timer += Time.deltaTime;
        textTimer.text = FunctionMethod.GetTime((int)(timePlay - timer));
        if(timePlay - timer <= 30 && !changedColor){
            changedColor = true;
            ChangeColorText();
        }
    }

    IEnumerator EffectTimePlay(float effectTime){
        yield return new WaitForSeconds(effectTime);
        endGame = true;
        Camera.main.GetComponent<GameManager>().gamePlayState = GameDefine.GAMEPLAY_STATE.ENDGAME;
    }

    void ChangeColorText(){
        textTimer.color = new Color(255, 0, 0, 255);
    }
}
