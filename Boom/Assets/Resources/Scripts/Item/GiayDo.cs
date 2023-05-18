using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiayDo : MonoBehaviour
{
    float speed = 0.4f;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.gameObject.GetComponent<Player>().IncreaseSpeed(speed);
            other.GetComponent<Player>().AddItemPickup(name.Split('(')[0].Trim(), 1);
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
