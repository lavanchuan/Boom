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
    Sound sound;
    private void Start() {
        sound = GameObject.FindGameObjectWithTag(Sound.TAG).GetComponent<Sound>();

        btnPlay.enabled = false;
        btnBacktrack.onClick.AddListener(BtnBacktrack);
        btnPlay.onClick.AddListener(BtnPlay);
    }

    void BtnPlay(){
        sound.PlaySound(Sound.GAME_START);
        StartCoroutine(FunctionMethod.EffectChangeScene(pathScenePlay, 1f));
    }

    void BtnBacktrack(){
        loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }
}
