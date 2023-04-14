using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarJoystick : MonoBehaviour
{
    Player player;

    private void Awake() {
        player = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "TouchCheck" && !player.GetChoked()){
            player.UseRadar();
        }
    }
}
