using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaStartMapBoss : MonoBehaviour
{
    public GameObject[] seaStarts;
    public GameObject boss;
    AttributeTurtle attributeTurtle;
    string[] pathSeaStartList;
    Vector2[] posSeaStartList;
    float[] timeRegenerateList;
    int[] typeDirectList;
    float timeRegenerate = 5f;
    string PATH_PREFABS_SEA_START = "Prefabs/Boss/";
    public bool playing;
    public bool isWin;
    bool bossDied;
    public bool clearSeaStarts;
    string pathSceneLoad = MainMenu.PATH_SCENE_MAINMENU;
    
    private void Start() {
        attributeTurtle = boss.GetComponent<AttributeTurtle>();
        pathSeaStartList = new string[seaStarts.Length];
        posSeaStartList = new Vector2[seaStarts.Length];
        timeRegenerateList = new float[seaStarts.Length];
        typeDirectList = new int[seaStarts.Length];
        for(int i = 0; i < seaStarts.Length; i++){
            posSeaStartList[i] = seaStarts[i].transform.localPosition;
            pathSeaStartList[i] = seaStarts[i].name.Split('(')[0].Trim();
            timeRegenerateList[i] = timeRegenerate;
            typeDirectList[i] = seaStarts[i].GetComponent<AttributeSeaStart>().GetTypeDirect();
        }
    }

    private void FixedUpdate() {
        if(attributeTurtle.dieing) return;
        for(int i = 0; i < seaStarts.Length; i++){
            if(seaStarts[i] == null){
                timeRegenerateList[i] = timeRegenerateList[i] - Time.deltaTime;
                if(timeRegenerateList[i] <= 0){
                    seaStarts[i] =
                    (GameObject)Instantiate(Resources.Load(PATH_PREFABS_SEA_START
                        + pathSeaStartList[i]));
                    seaStarts[i].transform.localPosition = posSeaStartList[i];
                    seaStarts[i].GetComponent<AttributeSeaStart>().typeDirect = typeDirectList[i];
                    timeRegenerateList[i] = timeRegenerate;
                }
            }
        }
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        if(attributeTurtle.dieing && !clearSeaStarts){
            ClearSeaStarts();
            StartCoroutine(EffectPickupItem(GameDefine.TIME_PICKUP_ITEM_OF_ROUND));
        }
    }

    IEnumerator EffectPickupItem(float effectTime){
        yield return new WaitForSeconds(effectTime);
        StartCoroutine(FunctionMethod.EffectChangeScene(pathSceneLoad, 1f));
    }

    void ClearSeaStarts(){
        Debug.Log("CLEAR SEA START");
        clearSeaStarts = true;
        foreach(GameObject go in seaStarts){
            Destroy(go);
        }
    }
}
