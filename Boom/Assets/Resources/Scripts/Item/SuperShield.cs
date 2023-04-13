using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShield : MonoBehaviour
{
    string name = "Siêu Khiên - Khiên Super Man";
    string descript = "Sở hữu khiên này giúp chủ thể có tối đa tốc độ di chuyển, " +
    "tối đa số lượng boom và tối đa độ dài boom trong thời gian ngắn";
    float effectTime = 20f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().StateSuperShield(effectTime);
            Destroy(gameObject);
        }
    }
}
