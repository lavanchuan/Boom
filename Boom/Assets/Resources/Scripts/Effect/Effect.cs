using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static readonly string EFFECT_USE_NEEDLE = "EffectUseNeedle";
    public static readonly string EFFECT_USE_FORTIFY = "EffectUseFortify";
    public static readonly float TIME_EFFECT = 5f;
    public float timer;

    public GameObject owner;

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        transform.localPosition = new Vector2(
            owner.transform.localPosition.x,
            owner.transform.localPosition.y + owner.transform.localScale.y/2
        );

        timer += Time.deltaTime;
        if(timer >= TIME_EFFECT){
            Destroy(gameObject);
        }
        
    }
}
