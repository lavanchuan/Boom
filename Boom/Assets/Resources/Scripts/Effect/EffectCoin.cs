using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCoin : MonoBehaviour
{
    public static readonly string EFFECT_COIN = "EffectCoin";
    public static readonly float TIME_EFFECT = 2f;
    public string text;
    float timer;

    private void Start() {
        GetComponent<TextMesh>().text = text;
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer >= TIME_EFFECT){
            Destroy(gameObject);
        }
    }
}
