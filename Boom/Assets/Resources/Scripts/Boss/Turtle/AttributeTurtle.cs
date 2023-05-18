using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTurtle : MonoBehaviour
{
    // order in layer 4
    // pixels per unit 20
    public static string TAG = "BossTurtle";
    public static string NAME = "SEA TURTLE";
    public static float MAX_HEALTH = 100f;
    public static float effectTimeUpdateDirect = 5f;
    public static float SPEED_DEFAULT = 0.5f;
    public static float SPEED_MAX = 3f;
    public static float TIME_DIE = 2f;
    const float TIME_CHOKE = 6f;
    int minRandom = 100;
    int maxRandom = 1000;
    int direct;
    bool beingAttacked = false;
    bool attacking = false;
    public bool dieing = false;
    bool prepareAttacking = false;
    bool stopAttacking = false;
    bool choked = false;
    const float TIME_BEING_ATTACKED = 1.8f;
    const float TIME_PREPARE_ATTACK = 3f;
    const float TIME_STOP_ATTACK = 3f;
    const float TIME_ATTACK = 10f;
    const float DELTA_TIME_ATTACK = 30f;
    float deltaTimeAttack = 0f;
    // Component
    BossAttribute bossAttribute;
    Animator animator;

    private void Start() {
        bossAttribute = GetComponent<BossAttribute>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;

        if(!prepareAttacking && !attacking && !stopAttacking && !dieing){
            deltaTimeAttack += Time.deltaTime;
            Move(SPEED_DEFAULT);
        } else if(attacking && !stopAttacking){
            if(direct == GameDefine.DOWN){
                transform.localPosition = new Vector2(transform.localPosition.x, 
                transform.localPosition.y - SPEED_MAX * Time.deltaTime);
            } else if(direct == GameDefine.UP){
                transform.localPosition = new Vector2(transform.localPosition.x, 
                transform.localPosition.y + SPEED_MAX * Time.deltaTime);
            } else if(direct == GameDefine.LEFT){
                transform.localPosition = new Vector2(
                    transform.localPosition.x - SPEED_MAX * Time.deltaTime,
                    transform.localPosition.y);
            } else if(direct == GameDefine.RIGHT){
                transform.localPosition = new Vector2(
                    transform.localPosition.x + SPEED_MAX * Time.deltaTime,
                    transform.localPosition.y);
            }
        }
        // UpdateAnimator();
    }

    public void UpdateAnimator(){
        animator.SetBool("BeingAttacked", beingAttacked);
        animator.SetBool("PrepareAttacking", prepareAttacking);
        animator.SetBool("Attacking", attacking);
        animator.SetBool("StopAttacking", stopAttacking);
        animator.SetBool("Dieing", dieing);
    }

    void LoadDirect(){
        if(beingAttacked || prepareAttacking || attacking || stopAttacking || dieing) return;
        int rd = UnityEngine.Random.Range(minRandom, maxRandom);
        if(rd % 3 == 0) direct = GameDefine.STAND;
        else if(rd % 3 == 1) direct = GameDefine.DOWN;
        else direct = GameDefine.UP;
        animator.SetInteger("Direct", direct);
    }

    IEnumerator EffectUpdateDirect(float effectTime, float healthCurrent){
        while(healthCurrent > 0){
            yield return new WaitForSeconds(effectTime);
            LoadDirect();
            GetComponent<BossAttribute>().SetDirect(direct);
        }
    }

    public void UpdateDirect(float healthCurrent){
        StartCoroutine(EffectUpdateDirect(effectTimeUpdateDirect, healthCurrent));
    }

    // MOVE
    public void Move(float speed){
        if( bossAttribute.GetHealthCurrent() < 1|| beingAttacked || prepareAttacking 
            || attacking || stopAttacking || dieing) return;
        if(direct == GameDefine.DOWN){
            transform.localPosition = new Vector2(transform.localPosition.x, 
            transform.localPosition.y - speed * Time.deltaTime);
        } else if(direct == GameDefine.UP){
            transform.localPosition = new Vector2(transform.localPosition.x, 
            transform.localPosition.y + speed * Time.deltaTime);
        } else if(direct == GameDefine.STAND){
            if(bossAttribute.GetHealthCurrent() >= 1) Action();
        }
    }

    void Action(){
        if(deltaTimeAttack >= DELTA_TIME_ATTACK){
            if(UnityEngine.Random.Range(100, 1000) % 10 >= 7){
                prepareAttacking = true;
                deltaTimeAttack = 0;
                StartCoroutine(EffectAttacking());
            }
        }
    }

    IEnumerator EffectAttacking(){
        yield return new WaitForSeconds(TIME_PREPARE_ATTACK);
        attacking = true;
        direct = GameDefine.DOWN;//rd
        transform.localPosition = new Vector2(transform.localPosition.x,
            transform.localPosition.y + 2 * transform.localScale.y);
        StartCoroutine(EffectAttack());
        // Move Attack => Update Direct Attack
        yield return new WaitForSeconds(TIME_ATTACK);
        stopAttacking = true;
        transform.localPosition = new Vector2(transform.localPosition.x,
            transform.localPosition.y - 2 * transform.localScale.y);
        transform.rotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(TIME_STOP_ATTACK);
        prepareAttacking = false;
        attacking = false;
        stopAttacking = false;
        direct = GameDefine.STAND;
        animator.SetInteger("Direct", direct);
    }
    
    IEnumerator EffectAttack(){
        int angle = 0;
        while(attacking && !stopAttacking){
            angle += 5;
            if(angle >= 360){angle = 0;}
            transform.rotation = Quaternion.Euler(0,0,angle);
            // yield return new WaitForSeconds(0.0001f);
            yield return null;
        }
    }

    // Update Direct Attack
    void UpdateDirectAttack(){
        int size = 4;
        Vector2 relativePos = transform.localPosition;
        if(relativePos.x < 0 && relativePos.y + size/2 * transform.localScale.y > 0){
            if(UnityEngine.Random.Range(10, 100) % 2 == 0) direct = GameDefine.RIGHT;
            else direct = GameDefine.DOWN;
        } else if(relativePos.x < 0 && relativePos.y + size/2 * transform.localScale.y < 0){
            if(UnityEngine.Random.Range(10, 100) % 2 == 0) direct = GameDefine.RIGHT;
            else direct = GameDefine.UP;
        } else if(relativePos.x > 0 && relativePos.y + size/2 * transform.localScale.y < 0){
            if(UnityEngine.Random.Range(10, 100) % 2 == 0) direct = GameDefine.LEFT;
            else direct = GameDefine.UP;
        } else if(relativePos.x > 0 && relativePos.y + size/2 * transform.localScale.y > 0){
            if(UnityEngine.Random.Range(10, 100) % 2 == 0) direct = GameDefine.LEFT;
            else direct = GameDefine.DOWN;
        }
        animator.SetInteger("Direct", direct);
    }

    // Being attack => OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "WaterDamage" && !beingAttacked && !prepareAttacking
            && !attacking && !stopAttacking && !dieing){
            beingAttacked = true;
            float dmgOther = other.GetComponent<Damage>().GetDmg();
            if(other.GetComponent<Damage>().effects == "Boss1") dmgOther = 2;
            bossAttribute.DecreaseHealthCurrent(dmgOther);
            StartCoroutine(EffectBeingAttacked(TIME_BEING_ATTACKED));
        }

        if(other.tag == GameDefine.TAG_BLOCK_LIMIT){
            if(attacking){
                UpdateDirectAttack();
            } else {
                if(direct == GameDefine.UP) direct = GameDefine.DOWN;
                else direct = GameDefine.UP;
                animator.SetInteger("Direct", direct);
            }
        }

        if(other.tag == AttributeSeaStart.TAG){
            AttributeSeaStart seaStart = other.gameObject.GetComponent<AttributeSeaStart>();
            if(!seaStart.IsSleep() && !seaStart.IsAttacking() && attacking){
                seaStart.SleepState();
            } else if(seaStart.IsAttacking() && !beingAttacked && !prepareAttacking
                && !attacking && !stopAttacking && !dieing){
                beingAttacked = true;
                bossAttribute.DecreaseHealthCurrent(seaStart.GetDamage());
                Debug.Log("Health current: " + bossAttribute.GetHealthCurrent());
                StartCoroutine(EffectBeingAttacked(TIME_BEING_ATTACKED));
                seaStart.BrokenState();
            }
        }
    }

    bool triggerStaying = false;
    IEnumerator EffectTriggerStaying(){
        yield return new WaitForSeconds(3f);
        triggerStaying = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == GameDefine.TAG_BLOCK_LIMIT){
            if(!attacking && !triggerStaying){
                triggerStaying = true;
                StartCoroutine(EffectTriggerStaying());
                if(direct == GameDefine.UP) direct = GameDefine.DOWN;
                else direct = GameDefine.UP;
                animator.SetInteger("Direct", direct);
            }
        }
    }

    IEnumerator EffectBeingAttacked(float effectTime){
        yield return new WaitForSeconds(effectTime);
        beingAttacked = false;
    }
    
    public void ChokedState(){
        GetComponent<CircleCollider2D>().isTrigger = false;
        choked = true;
        effectChoked = StartCoroutine(EffectChoked());
    }

    IEnumerator EffectChoked(){
        yield return new WaitForSeconds(TIME_CHOKE);
        // Drop item
        StartCoroutine(EffectDie());
    }

    IEnumerator EffectDie(){
        dieing = true;
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<MayDropItemBoss>().DropItems();
        yield return new WaitForSeconds(TIME_DIE);
        Destroy(gameObject);
    }

    Coroutine effectChoked;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player" && !other.gameObject.GetComponent<Player>().GetChoked()){
            StopCoroutine(effectChoked);
            StartCoroutine(EffectDie());
        }
    }
}
