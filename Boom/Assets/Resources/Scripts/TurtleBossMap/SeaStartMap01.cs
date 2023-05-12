using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaStartMap01 : MonoBehaviour
{
    ArrayList seaStarts;
    public GameObject[] seaStartList;
    bool playing;
    bool isWin;
    string pathSceneLoad;
    public static readonly string map1 = "SeaStartsMap1";
    public static readonly string map2 = "SeaStartsMap2";
    public static readonly string map3 = "SeaStartsMapBoss";
    // GAME OBJECT OTHER
    GameManager gameManager;
    private void Start() {
        gameManager = Camera.main.GetComponent<GameManager>();
        playing = true;
        seaStarts = new ArrayList();
        foreach(GameObject go in seaStartList){
            seaStarts.Add(go);
        }

        if(this.name.Equals(map1)) this.pathSceneLoad = MainMenu.PATH_SCENE_TURTLE_MAP2;
        else if(this.name.Equals(map2)) this.pathSceneLoad = MainMenu.PATH_SCENE_TURTLE_MAP3;
        else if(this.name.Equals(map3)) this.pathSceneLoad = MainMenu.PATH_SCENE_MAINMENU;
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
        StartCoroutine(FunctionMethod.EffectChangeScene(pathSceneLoad, 1f));
    }
}
