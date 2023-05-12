using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBossMap : MonoBehaviour
{
    bool loadingScene;
    // OTHER GAME OBJECT
    // MUSIC
    // SOUND
    private void Start() {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == TouchCheck.TAG && !loadingScene){
            loadingScene = true;
            StartCoroutine(FunctionMethod.EffectChangeScene(GameDefine.TURTLE_BOSS_MAP_1, 1f));
        }
    }
}
