using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute : MonoBehaviour
{
    private string name;
    private string descript;
    private int health = 1;
    private bool attacking = false;
    private int direct = GameDefine.DOWN;
    private int healthCurrent;
    private float speed;
    private const float SPEED_MAX = 2f;
    float speedIncrease = 0.05f;
    private ArrayList items;
    const int HEALTH_BOSS_1 = 20;
    float deltaTimeAttack = 10f;
    float minDeltaTimeAttack = 3f;
    float deltatimeIncrease = 0.2f;
    int boomQuantity;

    // 
    float deltaTimeUpdateDirect; // seconds
    private void Awake() {
        if(tag == "Boss1"){
            name = "Boss1";
            descript = "Look at me, I'm the king. You have to kneel!!!";
            health = HEALTH_BOSS_1;
            healthCurrent = health;
            deltaTimeUpdateDirect = 5f;
            speed = 0.5f;
            boomQuantity = 1;
        }
    }

    // update direct
    void UpdateDirect(){
        direct = UnityEngine.Random.Range(100, 1000) % 5;
    }

    IEnumerator EffectUpdateDirect(){
        while(healthCurrent > 0){
            yield return new WaitForSeconds(deltaTimeUpdateDirect);
            UpdateDirect();
        }
    }

    IEnumerator EffectAttack(){
        while(healthCurrent > 0){
            yield return new WaitForSeconds(deltaTimeAttack);
            attacking = true;
            deltaTimeAttack -= deltatimeIncrease;
            if(deltaTimeAttack < minDeltaTimeAttack)
                deltaTimeAttack = minDeltaTimeAttack;
            // Dat bom
            PutBoom();
            StartCoroutine(StopPutBoom());
        }
    }

    new IEnumerator StopPutBoom(){
        yield return new WaitForSeconds(1);
        attacking = false;
        boomQuantity++;
        speed += speedIncrease;
        if(speed > SPEED_MAX){
            speed = SPEED_MAX;
        }
        UpdateDirect();
    }

    // Put boom at position
    void PutBoom(){
        for(int i = 0; i < boomQuantity; i++){
            Debug.Log("Dat bom " + i);
        }
    }

    // Update Animator
    Animator animator;

    void UpdateAnimator(){
        animator.SetInteger("Direct", direct);
        animator.SetInteger("Health", healthCurrent);
        animator.SetBool("Attacking", attacking);
    }

    private void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(EffectUpdateDirect());
        StartCoroutine(EffectAttack());
    }

    private void Update() {
        UpdateAnimator();
        if(health > 0) {
            Move();
        }

        // update direct if attack
        if(attacking){direct = GameDefine.STAND;}
    }

    // Move
    void Move(){

        if(direct == GameDefine.DOWN){
            transform.localPosition = new Vector2(transform.localPosition.x, 
            transform.localPosition.y - speed * Time.deltaTime);
        } 
        else if(direct == GameDefine.UP){
            transform.localPosition = new Vector2(transform.localPosition.x, 
            transform.localPosition.y + speed * Time.deltaTime);
        } else if(direct == GameDefine.LEFT){
            transform.localPosition = 
            new Vector2(transform.localPosition.x - speed * Time.deltaTime, 
            transform.localPosition.y);
        } else if(direct == GameDefine.RIGHT){
            transform.localPosition = 
            new Vector2(transform.localPosition.x + speed * Time.deltaTime, 
            transform.localPosition.y);
        }
    }

}

