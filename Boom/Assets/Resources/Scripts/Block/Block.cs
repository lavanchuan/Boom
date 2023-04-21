using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera")
        .GetComponent<GameManager>().listBlock.Add(gameObject);

        // setup position
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        // Debug.Log("Position: " + transform.position);
    }
}
