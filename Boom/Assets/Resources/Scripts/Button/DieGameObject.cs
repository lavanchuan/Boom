using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieGameObject : MonoBehaviour
{
    public Button btnReplay, btnMenu;
    public bool hiddenDieUI = true;
    Vector2 posHidden = new Vector2(99999, 99999);
    Vector2 posShow = new Vector2(0,0);
    Vector2 posShowBtnReplay, posShowBtnMenu;
    void Start()
    {
        btnReplay.onClick.AddListener(BtnReplay);
        btnMenu.onClick.AddListener(BtnMenu);

        posShowBtnReplay = btnReplay.transform.localPosition;
        posShowBtnMenu = btnMenu.transform.localPosition;

        HiddenDieUI(hiddenDieUI);
    }

    private void Update() {
        HiddenDieUI(hiddenDieUI);
    }

    void BtnReplay(){
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().path);
    }
    void BtnMenu(){
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }

    void HiddenDieUI(bool hidden){
        if(hidden){
            transform.localPosition = posHidden;
            btnReplay.transform.localPosition = posHidden;
            btnMenu.transform.localPosition = posHidden;
        } else {
            transform.localPosition = posShow;
            btnReplay.transform.localPosition = posShowBtnReplay;
            btnMenu.transform.localPosition = posShowBtnMenu;
        }
    }
}
