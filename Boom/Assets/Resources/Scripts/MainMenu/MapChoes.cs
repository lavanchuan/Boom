using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapChoes : MonoBehaviour
{
    public Button btnPlay, btnBacktrack;
    AsyncOperation loadScene;
    public string pathScenePlay;

    private void Start() {
        btnPlay.enabled = false;
        btnBacktrack.onClick.AddListener(BtnBacktrack);
        btnPlay.onClick.AddListener(BtnPlay);
    }

    void BtnPlay(){
        loadScene = SceneManager.LoadSceneAsync(pathScenePlay);
    }

    void BtnBacktrack(){
        loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }
}
