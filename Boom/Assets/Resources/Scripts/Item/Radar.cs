using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public static float z = -1f;
    string name = "Radar";
    string descript = "Sử dụng để dò vùng nguy hiểm khi boom phát nổ";
    public static float effectTime = 20f;
    int quantity = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().IncreaseRadarQuantity(quantity);
            Destroy(gameObject);
        }
    }
}
