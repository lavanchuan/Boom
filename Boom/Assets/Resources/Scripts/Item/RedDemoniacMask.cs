using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDemoniacMask : MonoBehaviour
{
    string name = "Mặt nạ quỷ đỏ";
    string descript = "Mang mặt nạ làm cho chủ thể có được tốc độ tối đa, đá được boom";
    bool canKickBoom = true;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().SetMaxSpeed();
            other.GetComponent<Player>().setCanKickBoom(canKickBoom);
            Destroy(gameObject);
        }
    }
}
