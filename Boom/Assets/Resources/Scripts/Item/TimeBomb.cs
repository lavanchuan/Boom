using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    int quantity = 1;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().IncreaseTimeBombQuantity(quantity);
            // Debug.Log("Time-bomb quantity: "+other.GetComponent<Player>().GetTimeBombQuantity());
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
