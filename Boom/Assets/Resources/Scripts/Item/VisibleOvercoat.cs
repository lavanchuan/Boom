using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOvercoat : MonoBehaviour
{
    string name ="Áo choàng tàng hình";
    string descript = "Mặc vào làm cho chủ thể vô hình trong thời gian ngắn";
    float effectTime = 20f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().StateVisibleByVisibleOverCoat(effectTime);
            Destroy(gameObject);
        }
    }
}
