// using System.Reflection.Metadata;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttribute : MonoBehaviour
{
    private string name;
    private string descript;
    private float health = 1;
    private bool attacking = false;
    private int direct = GameDefine.STAND;
    private float healthCurrent;
    private float speed;
    private const float SPEED_MAX = 2f;
    float speedIncrease = 0.05f;
    private ArrayList items;
    const int HEALTH_BOSS_1 = 50;
    float deltaTimeAttack = 20f;
    float minDeltaTimeAttack = 3f;
    float deltatimeIncrease = 0.2f;
    int boomQuantity;
    string boomName;
    int boomSize;
    bool attacked;
    // DIEING
    public bool dieing;

    // 
    float deltaTimeUpdateDirect; // seconds
    // Limit create boom
    float pos_x_max;
    float pos_y_max;
    float pos_x_min; 
    float pos_y_min; 
    void SetupBoomPosLimit(){
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
            attacked = false;
            // Limit random item or boom
            SetupBoomPosLimit();
        }

        // SEA TURTLE BOSS
        if(tag == AttributeTurtle.TAG){
            name = AttributeTurtle.NAME;
            descript = "...";
            health = AttributeTurtle.MAX_HEALTH;
            healthCurrent = health;
            speed = AttributeTurtle.SPEED_DEFAULT;
            attacked = false;
        }

    }

    // update direct
    void UpdateDirect(){
        if(tag == "Boss1"){
            direct = UnityEngine.Random.Range(100, 1000) % 5;
        }
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
        animator.SetInteger("Health", (int)healthCurrent);
        animator.SetBool("Attacking", attacking);
    }

    private void Start() {
        animator = GetComponent<Animator>();
        if(tag == "Boss1"){
            StartCoroutine(EffectUpdateDirect());
            StartCoroutine(EffectAttack());
        } else if(tag == AttributeTurtle.TAG){
            GetComponent<AttributeTurtle>().UpdateDirect(healthCurrent);
        }
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        if(tag == "Boss1") UpdateAnimator();
        if(tag == AttributeTurtle.TAG){
            animator.SetInteger("Health", (int)healthCurrent);
            GetComponent<AttributeTurtle>().UpdateAnimator();
        }
        if(healthCurrent > 0) {
            if(tag == "Boss1") {
                Move();
                // update direct if attack
                if(attacking){direct = GameDefine.STAND;}
            } 
            // else if(tag == AttributeTurtle.TAG){
            //     GetComponent<AttributeTurtle>().Move(speed);
            //     // Move();
            // }
        }

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
        if(dieing) return;
        if(other.collider.tag == "Player" && 
            !other.collider.GetComponent<Player>().GetShieldUsing()
            && healthCurrent > 0){
            other.gameObject.GetComponent<Player>().PlayerDie();
        }
    }

    // Trigger
    private void OnTriggerEnter2D(Collider2D other) {
        if(dieing) return;
        // Player
        if(other.tag == "Player" && !other.GetComponent<Player>().GetShieldUsing()){
            if(tag == AttributeTurtle.TAG && healthCurrent > 0)
            other.GetComponent<Player>().PlayerDie();
        }
        // Sea Start
        
    }

    // health
    public void DecreaseHealthCurrent(float damage){
        if(tag == "Boss1" && !attacked){
            healthCurrent -= damage;
            if(healthCurrent < 0){healthCurrent = 0;}
            attacked = true;
            StartCoroutine(EffectAttacked(0.1f));
            if(healthCurrent == 0){
                StartCoroutine(EffectDie(2f));
            }
        }

        if(tag == AttributeTurtle.TAG){
            healthCurrent -= damage;
            if(healthCurrent < 1){healthCurrent = 0;} // < 1
            if(healthCurrent == 0){
                GetComponent<AttributeTurtle>().ChokedState();
            }
        }

    }

    IEnumerator EffectAttacked(float effectTime){
        yield return new WaitForSeconds(effectTime);
        attacked = false;
    }

    IEnumerator EffectDie(float effectTime){
        dieing = true;
        yield return new WaitForSeconds(effectTime);
        GetComponent<MayDropItemBoss>().DropItems();
        Destroy(gameObject);
    }

    // Set Direct
    public void SetDirect(int direct){this.direct = direct;}
    public float GetHealthCurrent(){return this.healthCurrent;}
    public float GetHealthMax(){return this.health;}

}

