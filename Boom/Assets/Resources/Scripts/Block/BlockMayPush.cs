using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMayPush : MonoBehaviour
{
    bool canPush;
    Rigidbody2D rigidbody2D;
    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(canPush) rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        else rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            canPush = true;
        }

        if(other.gameObject.tag == "Player" 
            && !other.gameObject.GetComponent<Player>().GetChoked()){
                
            if(!canPush) return;
            if(other.contacts[0].normal.x < 0) transform.localPosition
                = new Vector2(transform.localPosition.x - 0.1f, 
                transform.localPosition.y);
            else if(other.contacts[0].normal.x > 0) transform.localPosition
                = new Vector2(transform.localPosition.x + 0.1f, 
                transform.localPosition.y);
            else if(other.contacts[0].normal.y < 0) transform.localPosition
                = new Vector2(transform.localPosition.x, 
                transform.localPosition.y - 0.1f);
            else if(other.contacts[0].normal.y > 0) transform.localPosition
                = new Vector2(transform.localPosition.x, 
                transform.localPosition.y + 0.1f);
            return;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){canPush = false;}
    }
}
