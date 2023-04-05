using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void Awake() {
        StartCoroutine(IDestroy());
    }

    IEnumerator IDestroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
