using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Component
    MayDropItem mdi;
    private void Start() {
        mdi = GetComponent<MayDropItem>();

        GameObject.FindGameObjectWithTag("MainCamera")
        .GetComponent<GameManager>().listBlock.Add(gameObject);

        // setup position
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        // Debug.Log("Position: " + transform.position);
    }

    public void BreakBlock(){
        mdi.BreakBlock();
        GameObject.FindGameObjectWithTag("MainCamera")
        .GetComponent<GameManager>().listBlock.Remove(gameObject);
        Destroy(gameObject);
    }
}
