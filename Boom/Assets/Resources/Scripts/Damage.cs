using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static readonly string TAG = "WaterDamage";
    float dmg;
    bool choked = true;
    int speed = 1;
    public string effects;

    private void Awake() {
        StartCoroutine(IDestroy());
    }

    IEnumerator IDestroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Attack " + other.collider.tag);
        
        if(other.collider.tag == "Player"){
            if(!other.collider.GetComponent<Player>().GetChoked() 
            && !other.collider.GetComponent<Player>().GetShieldUsing()){
                other.collider.GetComponent<Player>().SetChoked(choked);
                other.collider.GetComponent<Player>().StateChoke(speed);
            }
        }

        // DEBUG
        if(other.collider.tag == GameDefine.TAG_BLOCK_MAY_BROKEN 
            && !other.collider.GetComponent<MayDropItem>().breaked){
            other.collider.GetComponent<MayDropItem>().BreakBlock();
        }

        if(other.collider.tag == "Boss1"){
            if(tag == effects){return;}
            other.collider.GetComponent<BossAttribute>().DecreaseHealthCurrent(dmg);
        }
    }

    public void SetDmg(float dmg){
        this.dmg = dmg;
    }
    public float GetDmg(){return this.dmg;}
    
}
