using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBoss : MonoBehaviour
{
    public GameObject healthBar;
    HealthBar hb;
    BossAttribute bossAttribute;
    private void Start() {
        bossAttribute = GetComponent<BossAttribute>();
        hb = healthBar.GetComponent<HealthBar>();

        if(bossAttribute.tag == "Boss1"){
            hb.width = 8;
        }
    }

    private void Update() {
        hb.healthMax = bossAttribute.GetHealthMax();
        hb.healthCurrent = bossAttribute.GetHealthCurrent();
        healthBar.transform.position
            = new Vector2(transform.position.x - hb.width/2 * transform.localScale.x,
            transform.position.y + hb.width/2 * transform.localScale.y);
    }
}
