using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void Awake() {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Di vao");
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Roi khoi");
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
