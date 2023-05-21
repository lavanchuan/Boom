using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button btnPlay, btnSetting, btnQuit, btnHeightCoin;
    AsyncOperation loadScene;
    public static readonly string PATH_SCENE_SETTING = "Scenes/SettingScene";
    public static readonly string PATH_SCENE_MAP_CHOES = "Scenes/MapChoesScene";
    public static readonly string PATH_SCENE_QUEEN_MAP = "Scenes/QueenBoss01";
    public static readonly string PATH_SCENE_TURTLE_MAP = "Scenes/TurtleBoss01";
    public static readonly string PATH_SCENE_TURTLE_MAP2 = "Scenes/TurtleBoss02";
    public static readonly string PATH_SCENE_TURTLE_MAP3 = "Scenes/TurtleBoss03";
    public static readonly string PATH_SCENE_MAINMENU = "Scenes/MainMenu";
    public static readonly string PATH_SCENE_HEIGHT_COIN = "Scenes/HeightCoin";
    public static readonly string PATH_SCENE_LOGIN = "Scenes/Login";
    Sound sound;
    string username;
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag(Sound.TAG).GetComponent<Sound>();

        btnPlay.onClick.AddListener(BtnPlay);
        btnSetting.onClick.AddListener(BtnSetting);
        btnQuit.onClick.AddListener(BtnQuit);
        btnHeightCoin.onClick.AddListener(BtnHeightCoin);

        // create system...
        if(!FunctionMethod.IsExistsFile(Application.persistentDataPath, GameDefine.LOGIN_STATE_FILE))
            FunctionMethod.CreateFile2(Application.persistentDataPath, GameDefine.LOGIN_STATE_FILE);
        if(!FunctionMethod.IsExistsFile(Application.persistentDataPath, GameDefine.PALYER_COIN_FILE)){
            FunctionMethod.CreateFile2(Application.persistentDataPath, GameDefine.PALYER_COIN_FILE);
            FunctionMethod.WriteFile2(Application.persistentDataPath, GameDefine.PALYER_COIN_FILE,
            true, "lavanchuan : 999999");
        }
        if(!FunctionMethod.IsExistsFile(Application.persistentDataPath, GameDefine.ACCOUNT_PLAYER_FILE))
            FunctionMethod.CreateFile2(Application.persistentDataPath, GameDefine.ACCOUNT_PLAYER_FILE);
            
        username = FunctionMethod.GetUsernameLoged();
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

    void BtnHeightCoin(){
        if(username != ""){
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(PATH_SCENE_HEIGHT_COIN);
        } else {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(PATH_SCENE_LOGIN);
        }
    }


}
