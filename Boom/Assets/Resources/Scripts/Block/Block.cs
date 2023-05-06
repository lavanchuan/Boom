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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player" 
            && !other.gameObject.GetComponent<Player>().GetChoked()){
            if(tag == GameDefine.TAG_BLOCK_MAY_BROKEN){
                try{
                    Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
                    rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    rigidbody2D.mass = 1;
                } catch (Exception){}
            }
            return;
        }

        if(other.collider.tag != Damage.TAG){
            try{
                Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            } catch (Exception){}
        }
    }

    // private void OnCollisionStay2D(Collision2D other) {
    //     if(other.gameObject.tag == "Player" 
    //         && !other.gameObject.GetComponent<Player>().GetChoked()){
    //         if(tag == GameDefine.TAG_BLOCK_MAY_BROKEN){
    //             try{
    //                 Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
    //                 rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    //             } catch (Exception){}
    //         }
    //         return;
    //     }
    // }
}
