using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSizeBoom : MonoBehaviour
{
    // Static attribute
    public static readonly string PATH_PREFABS = "Prefabs/Boom/CheckSizeBoom"; 
    //
    public int sizeCheck;
    public float sizeExplosiveTargetLeft = -1;
    public float sizeExplosiveTargetRight = -1;
    public float sizeExplosiveTargetUp = -1;
    public float sizeExplosiveTargetDown = -1;
    public int direct;
    // Other Game Object
    public GameObject target;

    private void Start() {
        transform.localPosition = target.transform.localPosition;
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        if(target == null){Destroy(gameObject); return;}
        float distanceX;
        float distanceY;
        distanceX = UnityEngine.Mathf.Abs(target.transform.localPosition.x - transform.localPosition.x);
        distanceY = UnityEngine.Mathf.Abs(target.transform.localPosition.y - transform.localPosition.y);
        if(direct == GameDefine.LEFT && sizeCheck > distanceX){
            transform.localPosition = new Vector2(transform.localPosition.x - 0.1f,
                transform.localPosition.y);
        } else if (direct == GameDefine.RIGHT && sizeCheck > distanceX){
            transform.localPosition = new Vector2(transform.localPosition.x + 0.1f,
                transform.localPosition.y);
        } else if(direct == GameDefine.UP && sizeCheck > distanceY){
            transform.localPosition = new Vector2(transform.localPosition.x,
                transform.localPosition.y + 0.1f);
        } else if(direct == GameDefine.DOWN && sizeCheck > distanceY){
            transform.localPosition = new Vector2(transform.localPosition.x,
                transform.localPosition.y - 0.1f);
        } 
        else if(sizeCheck <= distanceX || sizeCheck <= distanceY){
            // transform.localPosition = target.transform.localPosition;
            // if(direct == GameDefine.LEFT) direct = GameDefine.UP;
            // else if(direct == GameDefine.UP) direct = GameDefine.RIGHT;
            // else if(direct == GameDefine.RIGHT) direct = GameDefine.DOWN;
            // else if(direct == GameDefine.DOWN){ //direct = GameDefine.LEFT;
            //     target.GetComponent<Bom>().sizeLeft = sizeExplosiveTargetLeft;
            //     target.GetComponent<Bom>().sizeRight = sizeExplosiveTargetRight;
            //     target.GetComponent<Bom>().sizeDown = sizeExplosiveTargetDown;
            //     target.GetComponent<Bom>().sizeUp = sizeExplosiveTargetUp;
            //     Destroy(gameObject);
            // }

            // if(direct == GameDefine.LEFT)
            //     target.GetComponent<Bom>().sizeLeft = sizeExplosiveTargetLeft;
            // else if(direct == GameDefine.RIGHT)
            //     target.GetComponent<Bom>().sizeRight = sizeExplosiveTargetRight;
            // else if(direct == GameDefine.DOWN)
            //     target.GetComponent<Bom>().sizeDown = sizeExplosiveTargetDown;
            // else if(direct == GameDefine.UP)
            //     target.GetComponent<Bom>().sizeUp = sizeExplosiveTargetUp;
            transform.localPosition = target.transform.localPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(target == null) {Destroy(gameObject); return;}
        if(other.tag == GameDefine.TAG_BLOCK_LIMIT
            || other.tag == GameDefine.TAG_BLOCK_MAY_BROKEN
            || other.tag == GameDefine.TAG_BLOCK_NOT_BROKEN){
            float distance;
            if(direct == GameDefine.LEFT){
                distance = UnityEngine.Mathf.Abs(
                    target.transform.localPosition.x - transform.localPosition.x);
                sizeExplosiveTargetLeft = distance;
                // direct = GameDefine.UP;
                transform.localPosition = target.transform.localPosition;
                
                //
                if(other.tag == GameDefine.TAG_BLOCK_MAY_BROKEN)
                    target.GetComponent<Bom>().sizeLeft = sizeExplosiveTargetLeft + 1;
                else target.GetComponent<Bom>().sizeLeft = sizeExplosiveTargetLeft;
            } else if(direct == GameDefine.UP){
                distance = UnityEngine.Mathf.Abs(
                    target.transform.localPosition.y - transform.localPosition.y);
                sizeExplosiveTargetUp = distance;
                // direct = GameDefine.RIGHT;
                transform.localPosition = target.transform.localPosition;
                target.GetComponent<Bom>().sizeUp = sizeExplosiveTargetUp;
                if(other.tag == GameDefine.TAG_BLOCK_MAY_BROKEN)
                    target.GetComponent<Bom>().sizeUp = sizeExplosiveTargetUp + 1;
            } else if(direct == GameDefine.RIGHT){
                distance = UnityEngine.Mathf.Abs(
                    target.transform.localPosition.x - transform.localPosition.x);
                sizeExplosiveTargetRight = distance;
                // direct = GameDefine.DOWN;
                transform.localPosition = target.transform.localPosition;
                target.GetComponent<Bom>().sizeRight = sizeExplosiveTargetRight;
                if(other.tag == GameDefine.TAG_BLOCK_MAY_BROKEN)
                    target.GetComponent<Bom>().sizeRight = sizeExplosiveTargetRight + 1;
            } else if(direct == GameDefine.DOWN){
                distance = UnityEngine.Mathf.Abs(
                    target.transform.localPosition.y - transform.localPosition.y);
                sizeExplosiveTargetDown = distance;
                // direct = GameDefine.LEFT;
                transform.localPosition = target.transform.localPosition;
                target.GetComponent<Bom>().sizeDown = sizeExplosiveTargetDown;
                if(other.tag == GameDefine.TAG_BLOCK_MAY_BROKEN)
                    target.GetComponent<Bom>().sizeDown = sizeExplosiveTargetDown + 1;
            }
        }
    }

    // Destroy game onject
    public void DestroyCheckSizeBoom(){
        Destroy(gameObject);
    }
}
