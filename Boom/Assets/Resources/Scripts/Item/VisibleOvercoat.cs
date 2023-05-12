using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOvercoat : MonoBehaviour
{
    string name ="Áo choàng tàng hình";
    string descript = "Mặc vào làm cho chủ thể vô hình trong thời gian ngắn";
    float effectTime = 20f;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().StateVisibleByVisibleOverCoat(effectTime);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
