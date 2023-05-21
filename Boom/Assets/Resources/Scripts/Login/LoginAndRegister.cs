using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginAndRegister : MonoBehaviour
{
    // login
    // public Text txtUsernameLogin;
    // public Text txtPasswordLogin;
    public InputField txtUsernameLogin, txtPasswordLogin;
    public Button btnLoginLogin, btnRegisterLogin, btnBacktrackLogin;

    // register
    // public Text txtUsernameRegister;
    // public Text txtPasswordRegister;
    // public Text txtRepasswordRegister;
    public InputField txtUsernameRegister, txtPasswordRegister, txtRepasswordRegister;
    public Button btnLoginRegister, btnRegisterRegister, btnBacktrackRegister;

    // placeholder
    // public Text txtUsernamePlaceholderLogin, txtPasswordPlaceholderLogin,
    //     txtUsernamePlaceholderRegister, txtPasswordPlaceholderRegister, txtRepasswordPlaceholderRegister;
    public Text txtMessage;
    Vector2 posTxtUsernameLoginDefault, posTxtPasswordLoginDefault,
        posBtnLoginLoginDefault, posBtnRegisterLoginDefault, posBacktrackLoginDefault;
    Vector2 posTxtUsernameRegisterDefault, posTxtPasswordRegisterDefault, posTxtRepasswordRegisterDefault,
        posBtnLoginRegisterDefault, posBtnRegisterRegisterDefault, posBtnBacktrackRegisterDefault;
    Vector2 posHidden;

    bool isLoginHidden;
    private void Start() {

        btnLoginLogin.onClick.AddListener(BtnLoginLogin);
        btnRegisterLogin.onClick.AddListener(BtnRegisterLogin);
        btnBacktrackLogin.onClick.AddListener(BtnBacktrack);
        btnBacktrackRegister.onClick.AddListener(BtnBacktrack);
        btnLoginRegister.onClick.AddListener(BtnLoginRegister);
        btnRegisterRegister.onClick.AddListener(BtnRegisterRegister);

        // pos default
        posTxtUsernameLoginDefault = txtUsernameLogin.transform.localPosition;
        posTxtPasswordLoginDefault = txtPasswordLogin.transform.localPosition;
        posBtnLoginLoginDefault = btnLoginLogin.transform.localPosition;
        posBtnRegisterLoginDefault = btnRegisterLogin.transform.localPosition;
        posBacktrackLoginDefault = btnBacktrackLogin.transform.localPosition;

        posTxtUsernameRegisterDefault = txtUsernameRegister.transform.localPosition;
        posTxtPasswordRegisterDefault = txtPasswordRegister.transform.localPosition;
        posTxtRepasswordRegisterDefault = txtRepasswordRegister.transform.localPosition;
        posBtnLoginRegisterDefault = btnLoginRegister.transform.localPosition;
        posBtnRegisterRegisterDefault = btnRegisterRegister.transform.localPosition;
        posBtnBacktrackRegisterDefault = btnBacktrackRegister.transform.localPosition;

        posHidden = new Vector2(-9999, -9999);
    }

    private void Update() {
        HiddenUILogin(isLoginHidden);
        HiddenUIRegister(!isLoginHidden); 
    }

    void BtnLoginLogin(){
        string username = txtUsernameLogin.text;
        string password = txtPasswordLogin.text;

        if(!CheckInput(true)) {
            txtMessage.text = "Username or password can't empty";
            ClearText();
        } else if(!FunctionMethod.CheckLogin(username, password)){
            txtMessage.text = "Username or password incorrect";
            ClearText();
        } else {
            FunctionMethod.WriteLogin(username);
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
        }
    }
    void BtnRegisterLogin(){
        isLoginHidden = true;
        txtMessage.text = "";
    }
    void BtnLoginRegister(){
        isLoginHidden = false;
        txtMessage.text = "";
    }
    void BtnRegisterRegister(){
        string username = txtUsernameRegister.text;
        string password = txtPasswordRegister.text;
        string repassword = txtRepasswordRegister.text;

        if(!CheckInput(false)){
            txtMessage.text = "Fields can't empty";
            ClearText();
        } else if(password != repassword){
            txtMessage.text = "Password not equals";
            ClearText();
        } else if(!FunctionMethod.AddAccount(username, password)){
            txtMessage.text = "Username is exists";
            ClearText();
        } else {
            isLoginHidden = false;
            ClearText();
            txtMessage.text = "";
        }
    }
    void BtnBacktrack(){
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }
    void HiddenUILogin(bool hidden){
        if(!hidden){
            txtUsernameLogin.transform.localPosition = posTxtUsernameLoginDefault;
            // txtUsernamePlaceholderLogin.transform.localPosition = posTxtUsernameLoginDefault;
            txtPasswordLogin.transform.localPosition = posTxtPasswordLoginDefault;
            // txtPasswordPlaceholderLogin.transform.localPosition = posTxtPasswordLoginDefault;
            btnLoginLogin.transform.localPosition = posBtnLoginLoginDefault;
            btnRegisterLogin.transform.localPosition = posBtnRegisterLoginDefault;
            btnBacktrackLogin.transform.localPosition = posBacktrackLoginDefault;
        } else {
            txtUsernameLogin.transform.localPosition = posHidden;
            // txtUsernamePlaceholderLogin.transform.localPosition = posHidden;
            txtPasswordLogin.transform.localPosition = posHidden;
            // txtPasswordPlaceholderLogin.transform.localPosition = posHidden;
            btnLoginLogin.transform.localPosition = posHidden;
            btnRegisterLogin.transform.localPosition = posHidden;
            btnBacktrackLogin.transform.localPosition = posHidden;
        }
    }
    
    void HiddenUIRegister(bool hidden){
        if(!hidden){
            txtUsernameRegister.transform.localPosition = posTxtUsernameRegisterDefault;
            // txtUsernamePlaceholderRegister.transform.localPosition = posTxtUsernameRegisterDefault;
            txtPasswordRegister.transform.localPosition = posTxtPasswordRegisterDefault;
            // txtPasswordPlaceholderRegister.transform.localPosition = posTxtPasswordRegisterDefault;
            txtRepasswordRegister.transform.localPosition = posTxtRepasswordRegisterDefault;
            // txtRepasswordPlaceholderRegister.transform.localPosition = posTxtRepasswordRegisterDefault;
            btnLoginRegister.transform.localPosition = posBtnLoginRegisterDefault;
            btnRegisterRegister.transform.localPosition = posBtnRegisterRegisterDefault;
            btnBacktrackRegister.transform.localPosition = posBtnBacktrackRegisterDefault;
        } else {
            txtUsernameRegister.transform.localPosition = posHidden;
            // txtUsernamePlaceholderRegister.transform.localPosition = posHidden;
            txtPasswordRegister.transform.localPosition = posHidden;
            // txtPasswordPlaceholderRegister.transform.localPosition = posHidden;
            txtRepasswordRegister.transform.localPosition = posHidden;
            // txtRepasswordPlaceholderRegister.transform.localPosition = posHidden;
            btnLoginRegister.transform.localPosition = posHidden;
            btnRegisterRegister.transform.localPosition = posHidden;
            btnBacktrackRegister.transform.localPosition = posHidden;
        }
    }
    void ClearText(){
        txtUsernameLogin.text = "";
        txtPasswordLogin.text = "";
        txtUsernameRegister.text = "";
        txtPasswordRegister.text = "";
        txtRepasswordRegister.text = "";
    }
    bool CheckInput(bool isLogin){
        if(isLogin){
            return txtUsernameLogin.text != "" && txtPasswordLogin.text != "";
        } else {
            return txtUsernameRegister.text != "" && txtPasswordRegister.text != ""
                && txtRepasswordRegister.text != "";
        }
    }

    string TextShowToTextPassword(string text){
        string result = "";
        for(int i = 0; i < text.Length; i++) result += "*";
        return result;
    }
}
