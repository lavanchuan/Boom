using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Sound sound;
    int valueMoney;
    public static readonly string GOLD_COIN = "GoldCoin";
    public static readonly string BRONZE_COIN = "BronzeCoin";
    public static readonly string SILVER_COIN = "SilverCoin";
    public static readonly string GOLDEN_BAG = "GoldenBag";
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        time += Time.deltaTime;
    }

    private void Awake() {
        string namePrefabs = name.Split('(')[0];

        if(namePrefabs == GOLD_COIN) valueMoney = 15;
        else if(namePrefabs == SILVER_COIN) valueMoney = 10;
        else if(namePrefabs == BRONZE_COIN) valueMoney = 5;
        else if(namePrefabs == GOLDEN_BAG) valueMoney = 100;
    }
    private void Start() {
        sound = GameObject.FindGameObjectWithTag(Sound.TAG).GetComponent<Sound>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            sound.PlaySound(Sound.COIN);
            other.GetComponent<Player>().IncreaseMoney(valueMoney);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);

            EffectCoin effectCoin = 
                FunctionMethod.CreateEffect(EffectCoin.EFFECT_COIN).GetComponent<EffectCoin>();
            effectCoin.text = "" + valueMoney;
            effectCoin.transform.localPosition = new Vector2(
                transform.localPosition.x,
                transform.localPosition.y + transform.localScale.y/2
            );

            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
