using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button btnPlay, btnSetting, btnQuit;
    AsyncOperation loadScene;
    public static readonly string PATH_SCENE_SETTING = "Scenes/SettingScene";
    public static readonly string PATH_SCENE_MAP_CHOES = "Scenes/MapChoesScene";
    public static readonly string PATH_SCENE_QUEEN_MAP = "Scenes/QueenBoss01";
    public static readonly string PATH_SCENE_TURTLE_MAP = "Scenes/TurtleBoss01";
    public static readonly string PATH_SCENE_TURTLE_MAP2 = "Scenes/TurtleBoss02";
    public static readonly string PATH_SCENE_TURTLE_MAP3 = "Scenes/TurtleBoss03";
    public static readonly string PATH_SCENE_MAINMENU = "Scenes/MainMenu";


    void Start()
    {
        btnPlay.onClick.AddListener(BtnPlay);
        btnSetting.onClick.AddListener(BtnSetting);
        btnQuit.onClick.AddListener(BtnQuit);
    }

    void BtnPlay(){
        loadScene = SceneManager.LoadSceneAsync(PATH_SCENE_MAP_CHOES);
    }

    void BtnSetting(){
        loadScene = SceneManager.LoadSceneAsync(PATH_SCENE_SETTING);
    }

    void BtnQuit(){
        Application.Quit();
    }


}
