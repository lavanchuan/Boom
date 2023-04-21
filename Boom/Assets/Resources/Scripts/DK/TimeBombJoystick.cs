using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBombJoystick : MonoBehaviour
{
    Player player;

    private void Awake() {
        player = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(GameManager.running && other.tag == "TouchCheck" && !player.GetChoked()){
            if(!player.GetPutTimeBomb()){
                player.PutTimeBomb();
            } else {
                player.ActiveExplosiveTimeBomb();
            }
        }
    }
}
