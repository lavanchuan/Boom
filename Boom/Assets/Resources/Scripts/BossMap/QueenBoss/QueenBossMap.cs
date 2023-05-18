using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBossMap : MonoBehaviour
{
    public GameObject boss;
    BossAttribute bossAttribute;
    bool bossIsDied;
    string pathSceneLoad;

    private void Start() {
        pathSceneLoad = MainMenu.PATH_SCENE_MAINMENU;
        bossAttribute = boss.GetComponent<BossAttribute>();
    }

    private void Update() {
        if(bossAttribute.dieing && !bossIsDied){
            bossIsDied = true;
            StartCoroutine(EffectPickupItem(GameDefine.TIME_PICKUP_ITEM_OF_ROUND));
        }
    }

    IEnumerator EffectPickupItem(float effectTime){
        yield return new WaitForSeconds(effectTime);
        StartCoroutine(FunctionMethod.EffectChangeScene(pathSceneLoad, 1f));
    }
}
