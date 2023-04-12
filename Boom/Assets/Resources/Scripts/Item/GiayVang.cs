using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiayVang : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.gameObject.GetComponent<Player>().SetMaxSpeed();
            Destroy(gameObject);
        }
    }
}
