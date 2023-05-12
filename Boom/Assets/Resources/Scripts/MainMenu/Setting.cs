using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Button btnBacktrackMainMenu;
    AsyncOperation loadScene;

    private void Start() {
        btnBacktrackMainMenu.onClick.AddListener(BtnBacktrack);
    }

    void BtnBacktrack(){
        loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }
}
