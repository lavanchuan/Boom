using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Button btnBacktrackMainMenu;
    public Slider sliderSoundVolume, sliderMusicVolume;
    public Text txtSoundVolume, txtMusicVolume;
    AsyncOperation loadScene;

    private void Start() {
        btnBacktrackMainMenu.onClick.AddListener(BtnBacktrack);

        sliderSoundVolume.minValue = 0;
        sliderMusicVolume.minValue = 0;
        sliderSoundVolume.maxValue = 1;
        sliderMusicVolume.maxValue = 1;


        sliderSoundVolume.value = FunctionMethod.GetSoundVolume();;
        sliderMusicVolume.value = FunctionMethod.GetMusicVolume();
    }

    private void Update() {
        txtSoundVolume.text = "" + (int)(sliderSoundVolume.value * 100);
        txtMusicVolume.text = "" + (int)(sliderMusicVolume.value * 100);
    }

    void BtnBacktrack(){
        ArrayList audioValueData = new ArrayList();

        audioValueData.Add("SoundVolume : " + sliderSoundVolume.value);
        audioValueData.Add("MusicVolume : " + sliderMusicVolume.value);

        FunctionMethod.WriteFile(GameDefine.SYSTEM_FILE_NAME, audioValueData, false);
        
        loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }
}
