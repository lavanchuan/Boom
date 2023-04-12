using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    bool choked = true;
    int speed = 1;

    private void Awake() {
        StartCoroutine(IDestroy());
    }

    IEnumerator IDestroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!other.GetComponent<Player>().GetChoked()){
                other.GetComponent<Player>().SetChoked(choked);
                other.GetComponent<Player>().StateChoke(speed);
            }
            return;
        }
    }
}
