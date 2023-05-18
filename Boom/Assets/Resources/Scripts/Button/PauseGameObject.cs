using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameObject : MonoBehaviour
{
    public Button btnPlay, btnReplay, btnMenu;
    public GameObject btnPause;
    Vector2 posHidden = new Vector2(99999, 99999);
    Vector2 posShow = new Vector2(0,0);
    Vector2 posShowBtnPlay, posShowBtnReplay, posShowBtnMenu;
    private void Start() {
        btnPlay.onClick.AddListener(BtnPlay);
        btnReplay.onClick.AddListener(BtnReplay);
        btnMenu.onClick.AddListener(BtnMenu);

        posShowBtnPlay = btnPlay.transform.localPosition;
        posShowBtnReplay = btnReplay.transform.localPosition;
        posShowBtnMenu = btnMenu.transform.localPosition;

        HiddenPauseScreen(true);
    }

    private void Update() {
        HiddenPauseScreen(!btnPause.GetComponent<PauseButton>().GetIsPause());
    }

    void BtnPlay(){
        btnPause.GetComponent<PauseButton>().Click();
    }
    void BtnReplay(){
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().path);
    }
    void BtnMenu(){
        // FunctionMethod.EffectChangeScene(MainMenu.PATH_SCENE_MAINMENU, 0f);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }

    void HiddenPauseScreen(bool hidden){
        if(hidden){
            transform.localPosition = posHidden;
            btnPlay.transform.localPosition = posHidden;
            btnReplay.transform.localPosition = posHidden;
            btnMenu.transform.localPosition = posHidden;
        } else {
            transform.localPosition = posShow;
            btnPlay.transform.localPosition = posShowBtnPlay;
            btnReplay.transform.localPosition = posShowBtnReplay;
            btnMenu.transform.localPosition = posShowBtnMenu;
        }
    }

    public bool GetPause(){return btnPause.GetComponent<PauseButton>().GetIsPause();}
}
