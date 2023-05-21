using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnLogin : MonoBehaviour
{
    public bool loged;
    public Text txtLogin;
    public Button btnLogin;
    private void Start() {
        if(FunctionMethod.GetUsernameLoged() != "") loged = true;

        btnLogin.onClick.AddListener(BtnLoginX);
    }

    private void Update() {
        if(loged) txtLogin.text = "Logout";
        else txtLogin.text = "Login";
    }

    void BtnLoginX(){
        if(loged){
            loged = false;
            FunctionMethod.WriteFile2(Application.persistentDataPath,
                GameDefine.LOGIN_STATE_FILE,
                false,
                "");
        } else {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_LOGIN);
        }
    }
}
