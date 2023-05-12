using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShield : MonoBehaviour
{
    string name = "Siêu Khiên - Khiên Super Man";
    string descript = "Sở hữu khiên này giúp chủ thể có tối đa tốc độ di chuyển, " +
    "tối đa số lượng boom và tối đa độ dài boom trong thời gian ngắn";
    float effectTime = 20f;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().StateSuperShield(effectTime);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
