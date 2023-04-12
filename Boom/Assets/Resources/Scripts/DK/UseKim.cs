using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKim : MonoBehaviour
{
    GameObject player;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "TouchCheck"){
            player = (GameObject)GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().UseKim();
        }
    }
}
