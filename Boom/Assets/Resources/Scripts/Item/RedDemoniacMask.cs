using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDemoniacMask : MonoBehaviour
{
    string name = "Mặt nạ quỷ đỏ";
    string descript = "Mang mặt nạ làm cho chủ thể có được tốc độ tối đa, đá được boom";
    bool canKickBoom = true;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().SetMaxSpeed();
            other.GetComponent<Player>().setCanKickBoom(canKickBoom);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            other.GetComponent<Player>().CreateEffect(Effect.EFFECT_USE_FORTIFY);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
