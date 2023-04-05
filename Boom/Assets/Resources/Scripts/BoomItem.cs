using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomItem : MonoBehaviour
{
    ArrayList entities;

    private void Awake() {
        SetupEntities();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        foreach(string entity in entities){
            if(other.tag == entity){
                Destroy(gameObject);
                break;
            }
        }
    }

    void SetupEntities(){
        entities = new ArrayList();
        entities.Add("Player");

    }
}
