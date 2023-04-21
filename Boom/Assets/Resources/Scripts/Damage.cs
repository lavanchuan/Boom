using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static readonly string TAG = "WaterDamage";
    bool choked = true;
    int speed = 1;

    private void Awake() {
        StartCoroutine(IDestroy());
    }

    IEnumerator IDestroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!other.GetComponent<Player>().GetChoked() 
            && !other.GetComponent<Player>().GetShieldUsing()){
                other.GetComponent<Player>().SetChoked(choked);
                other.GetComponent<Player>().StateChoke(speed);
            }
        }

        if(other.tag == "Block" && !other.GetComponent<MayDropItem>().breaked){
            other.GetComponent<MayDropItem>().BreakBlock();
            Destroy(other.gameObject);
        }
    }

    
}
