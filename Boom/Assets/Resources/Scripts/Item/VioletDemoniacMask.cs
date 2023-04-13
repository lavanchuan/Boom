using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletDemoniacMask : MonoBehaviour
{
    string name = "Mặt nạ quỷ tím";
    string descript = "Mang mặt nạ làm cho chủ thể đi ngược hoặc bị chậm";
    float effectTime = 15f;
    bool isContratyDirect = false;
    float deltaTimePut = 1f;

    private void Awake() {
        if(UnityEngine.Random.Range(0, 1000) % 2 == 0) isContratyDirect = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            if(isContratyDirect){
                other.GetComponent<Player>().StateContratyDirectByVioletDemoniacMask(effectTime);
            } else {
                other.GetComponent<Player>().StaetAutoPutBoomByVioletDemoniacMask(effectTime, deltaTimePut);
            }
            Destroy(gameObject);
        }
    }
}
