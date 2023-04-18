using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatBom : MonoBehaviour
{
    GameObject player;
    Player playerComponent;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = (Player)player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "TouchCheck"){
            DBom();
        }
    }

    void DBom(){
        if(!playerComponent.IsBoomEmpty()){
            playerComponent.DatBom();
            // Debug.Log("Da dat 1 qua bom");
        } else {
            // Debug.Log("Het bom de dat");
        }
    }
}
