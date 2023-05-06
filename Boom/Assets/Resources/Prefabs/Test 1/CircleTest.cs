using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Circle");
    }
}
