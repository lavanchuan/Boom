using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Square");
    }

    private void OnCollisionEnter2D(Collision2D other) {
    Debug.Log("collision square");
    }

    
}
