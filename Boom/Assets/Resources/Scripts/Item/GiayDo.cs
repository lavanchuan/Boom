using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiayDo : MonoBehaviour
{
    float speed = 0.4f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.gameObject.GetComponent<Player>().IncreaseSpeed(speed);
            Destroy(gameObject);
        }
    }
}
