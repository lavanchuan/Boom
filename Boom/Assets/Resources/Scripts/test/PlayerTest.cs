using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public int direct;
    public int speed;
    public bool onLeft, onRight, onUp, onDown;
    Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(EffectTest(2f));
    }

    private void Update() {
        animator.SetInteger("Direct", direct);
        if(direct == 0){onLeft = onRight = onDown = onUp = false;}
        if(direct == 1){onLeft = onRight = onUp = false; onDown = true;}
        if(direct == 2){onLeft = onRight = onDown = false; onUp = true;}
        if(direct == 3){onLeft = onDown = onUp = false; onRight = true;}
        if(direct == 4){onRight = onDown = onUp = false; onLeft = true;}
        Vector2 pos = transform.localPosition;
        if(onLeft) pos.x -= speed * Time.deltaTime;
        if(onRight) pos.x += speed * Time.deltaTime;
        if(onUp) pos.y += speed * Time.deltaTime;
        if(onDown) pos.y -= speed * Time.deltaTime;
        transform.localPosition = pos;
    }

    IEnumerator EffectTest(float effectTime){
        while(true){
            yield return new WaitForSeconds(effectTime);
            direct = UnityEngine.Random.Range(0, 5);
        }
    }
}
