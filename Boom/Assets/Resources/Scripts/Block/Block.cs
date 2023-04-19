using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera")
        .GetComponent<GameManager>().listBlock.Add(gameObject);
    }
}
