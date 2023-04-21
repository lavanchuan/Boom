// using System.Reflection.Metadata;
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
    string boomName;
    int boomSize;

    // 
    float deltaTimeUpdateDirect; // seconds
    // Limit create boom
    float pos_x_max;
    float pos_y_max;
    float pos_x_min; 
    float pos_y_min; 
    void SetupBoomPosLimt(){
        pos_x_max = GameDefine.X_MAX - 1;
        pos_x_min = GameDefine.X_MIN + 1;
        pos_y_max = GameDefine.Y_MAX - 1;
        pos_y_min = GameDefine.Y_MIN;
    }
    private void Awake() {
        if(tag == "Boss1"){
            name = "Boss1";
            descript = "Look at me, I'm the king. You have to kneel!!!";
            health = HEALTH_BOSS_1;
            healthCurrent = health;
            deltaTimeUpdateDirect = 5f;
            speed = 0.5f;
            boomQuantity = 1;
            boomName = Bom.bom2;
            boomSize = 1;
        }

        // Limit random item or boom
        SetupBoomPosLimt();
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
        if(++boomSize > Bom.MAX_SIZE){
            boomSize = Bom.MAX_SIZE;
        }
        UpdateDirect();
    }

    // Put boom at position
    void PutBoom(){
        for(int i = 0; i < boomQuantity; i++){
            GameObject boomTemp = (GameObject)Instantiate(Resources.Load("Prefabs/" + boomName));
            Bom bom = (Bom)boomTemp.GetComponent<Bom>();
            bom.GetComponent<CircleCollider2D>().isTrigger = false; // !important
            bom.transform.position = new Vector3(
                UnityEngine.Random.Range(0, pos_x_max - pos_x_min + 1) + pos_x_min,
                UnityEngine.Random.Range(0, pos_y_max - pos_y_min + 1) + pos_y_min,
                0
            );
            bom.size = boomSize;
            bom.tagEffects = tag;
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

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.collider.tag == "Player"){
            other.gameObject.GetComponent<Player>().PlayerDie();
        }
    }
}

