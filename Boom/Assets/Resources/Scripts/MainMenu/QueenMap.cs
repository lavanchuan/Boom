using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueenMap : MonoBehaviour
{
    
    public Button btnPlay;
    public GameObject mapChoes;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == TouchCheck.TAG){
            btnPlay.enabled = true;
            mapChoes.GetComponent<MapChoes>().pathScenePlay = MainMenu.PATH_SCENE_QUEEN_MAP;
        }
    }
}
