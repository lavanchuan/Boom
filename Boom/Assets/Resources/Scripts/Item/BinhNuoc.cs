using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinhNuoc : MonoBehaviour
{
    int size = 1;
    float time = 0;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.gameObject.GetComponent<Player>().IncreaseSizeBoom(this.size);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
