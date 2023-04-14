using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPoint : MonoBehaviour
{
    public float effectTime;
    private void Awake() {
        StartCoroutine(RadarPointEffect(effectTime));
    }

    IEnumerator RadarPointEffect(float effectTime){
        yield return new WaitForSeconds(effectTime);
        Destroy(gameObject);
    }
}
