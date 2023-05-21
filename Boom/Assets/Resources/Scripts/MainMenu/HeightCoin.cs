using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeightCoin : MonoBehaviour
{
    public Button btnBacktrack;
    public Text txtView;
    public string message;
    void Start()
    {
        btnBacktrack.onClick.AddListener(BtnBacktrack);

        GetMessage();

        txtView.text = message;
    }

    void GetMessage(){
        message = "";
        ArrayList pcl = FunctionMethod.GetPlayerCoinList();
        pcl = SortPlayerCoin(pcl);
        foreach(PlayerCoin pc in pcl){
            message += pc.ToLine() + "\n";
        }
    }

    void BtnBacktrack(){
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(MainMenu.PATH_SCENE_MAINMENU);
    }

    // SORT PLAYR COIN
    ArrayList SortPlayerCoin(ArrayList pcl){
        ArrayList result = pcl;
        for(int i = 0; i < pcl.Count - 1; i++){
            for(int j = i + 1; j < pcl.Count; j++){
                if(((PlayerCoin)result[i]).Equals((PlayerCoin)result[j]) == -1){
                    PlayerCoin temp = (PlayerCoin)result[i];
                    result[i] = (PlayerCoin)result[j];
                    result[j] = temp;
                }
            }
        }
        return result;
    }
}
