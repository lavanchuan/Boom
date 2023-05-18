using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public LineRenderer lineHealthCurrent;
    public LineRenderer lineHealthMax;
    public float healthMax = 100;
    public float healthCurrent = 20;
    public float width = 4;
    Quaternion rotarionDefault;
    private void Start() {
        rotarionDefault = transform.rotation;
        
        lineHealthCurrent.SetPosition(0, new Vector3(0, 0, 0));
        lineHealthCurrent.SetPosition(1, new Vector3(1, 0, 0));
        lineHealthCurrent.SetWidth(0.5f, 0.5f);
        lineHealthCurrent.SetColors(new Color(255, 0, 0), new Color(255, 0, 0));

        lineHealthMax.SetPosition(0, new Vector3(0,0,0));
        lineHealthMax.SetPosition(1, new Vector3(width,0,0));
        lineHealthMax.SetWidth(0.5f, 0.5f);
        lineHealthMax.SetColors(new Color(255, 255, 255), new Color(255, 255, 255));
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        transform.rotation = rotarionDefault;

        float x = width * healthCurrent / healthMax;
        lineHealthCurrent.SetPosition(1, new Vector3(x, 0, 0));
        lineHealthMax.SetPosition(1, new Vector3(width,0,0));

    }
}
