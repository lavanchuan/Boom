using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ArrayList listBlock;
    public static bool running;
    // IN ROUND MAP(LEVLE)
    public bool playing = true;
    public bool isWin;
    ArrayList itemsMayDrop;// string: path prefabs of item
    ArrayList coinsMayDrop;// string: path prefabs of coin
    // GAME PLAY STATE
    public GameDefine.GAMEPLAY_STATE gamePlayState;
    // Sound Game Object
    Sound sound;
    private void Awake() {
        gamePlayState = GameDefine.GAMEPLAY_STATE.PLAYING;
        listBlock = new ArrayList();
        running = true;
        itemsMayDrop = new ArrayList();
        coinsMayDrop = new ArrayList();
        SetupItemsMayDrop();
        SetUpCoinMayDrop();
    }
    // Start
    private void Start() {
        sound = GameObject.FindGameObjectWithTag(Sound.TAG).GetComponent<Sound>();
    }
    // Setup items may drop
    void SetupItemsMayDrop(){
        // Item auto use
        itemsMayDrop.Add("ItemAutoUse/BinhNuoc");
        itemsMayDrop.Add("ItemAutoUse/BongGai");
        itemsMayDrop.Add("ItemAutoUse/GiayDo");
        itemsMayDrop.Add("ItemAutoUse/GiayVang");
        itemsMayDrop.Add("ItemAutoUse/GiayXanh");
        itemsMayDrop.Add("ItemAutoUse/GreenDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/RedDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/VioletDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/VisibleOvercoat");
        itemsMayDrop.Add("ItemAutoUse/SuperShield");

        // item use
        itemsMayDrop.Add("ItemUse/Kim");
        itemsMayDrop.Add("ItemUse/Radar");
        itemsMayDrop.Add("ItemUse/Shield");
        itemsMayDrop.Add("ItemUse/TimeBomb");

        // coin
        itemsMayDrop.Add("Coin/GoldCoin");
        itemsMayDrop.Add("Coin/BronzeCoin");
        itemsMayDrop.Add("Coin/SilverCoin");

    }

    void SetUpCoinMayDrop(){
        coinsMayDrop.Add("Coin/GoldCoin");
        coinsMayDrop.Add("Coin/BronzeCoin");
        coinsMayDrop.Add("Coin/SilverCoin");
        coinsMayDrop.Add("Coin/GoldenBag");

    }

    private void Update() {
        if(!playing && isWin){
            isWin = false;
            sound.PlaySound(Sound.WIN);
        }
    }

    public ArrayList GetItemsMayDrop(){return this.itemsMayDrop;}
    public ArrayList GetCoinsMayDrop(){return this.coinsMayDrop;}
 
}
