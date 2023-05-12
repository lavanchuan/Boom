using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    string name = "Khiên";
    string descript = "Sử dụng tạo màng chắn giúp bảo vệ chủ thể không dính boom";
    public static float effectTime = 10f;
    int quantity = 1;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().IncreaseShieldQuantity(quantity);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
