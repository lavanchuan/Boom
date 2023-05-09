using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaStartMap01 : MonoBehaviour
{
    ArrayList seaStarts;
    public GameObject[] seaStartList;
    bool playing;
    bool isWin;
    // GAME OBJECT OTHER
    GameManager gameManager;
    private void Start() {
        gameManager = Camera.main.GetComponent<GameManager>();
        playing = true;
        seaStarts = new ArrayList();
        foreach(GameObject go in seaStartList){
            seaStarts.Add(go);
        }
    }

    private void Update() {
        if(playing && seaStarts.Count == 0){
            playing = false;
            StartCoroutine(EffectPickupItem(GameDefine.TIME_PICKUP_ITEM_OF_ROUND));
        }
    }

    private void FixedUpdate() {
        foreach(GameObject go in seaStarts){
            if(go == null){seaStarts.Remove(go);}
        }
    }

    public bool GetPlaying(){return this.playing;}

    IEnumerator EffectPickupItem(float effectTime){
        yield return new WaitForSeconds(effectTime);
        isWin = true;
        gameManager.playing = playing;
        gameManager.isWin = isWin;
    }
}
