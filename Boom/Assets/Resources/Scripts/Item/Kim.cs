using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kim : MonoBehaviour
{
    int quantity = 1;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().IncreaseNeedleQuantity(quantity);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
