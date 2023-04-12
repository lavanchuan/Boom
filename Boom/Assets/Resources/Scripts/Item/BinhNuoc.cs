using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinhNuoc : MonoBehaviour
{
    int size = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.gameObject.GetComponent<Player>().IncreaseSizeBoom(this.size);
            Destroy(gameObject);
        }
    }
}
