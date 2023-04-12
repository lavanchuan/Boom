using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomItem : MonoBehaviour
{
    int quantity = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            other.GetComponent<Player>().IncreaseBoomItem(quantity);
            Destroy(gameObject);
        }
    }

}
