using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSeaStart : MonoBehaviour
{
    public static readonly string TAG = "SeaStart";

    // Attribute
    int direct;
    bool regenerate;
    bool sleep;
    bool breaking;
    bool attacking;
    const float SPEED_MOVE = 0.5f;
    const float SPEED_ATTACK = 20f;
    bool collisionAfterByKick;
    float angleRotationAttack;
    float dmg;
    // Component
    Animator animator;
    BoxCollider2D boxCollider2d;
    // Time Define
    const float TIME_REGENERATE = 3f;
    const float TIME_BROKEN = 2f;
    const float TIME_SLEEP = 20f;
    const float TIME_UPDATE_DIRECT = 10f;
    // DIRECT MOVE
    ArrayList DIRECT_MAY_MOVE;

    public int typeDirect;
    const int horizonDirect = 1;
    const int verticalDirect = 2;
    const int fullDirect = 0;
    
    private void Awake() {
        dmg = 10f;
        regenerate = true;
        sleep = false;
        direct = GameDefine.STAND;
        breaking = false;
        DIRECT_MAY_MOVE = new ArrayList();
        // typeDirect = UnityEngine.Random.Range(0, 3);
        // Debug.Log(typeDirect);

        int[] directTest;
        if(typeDirect == horizonDirect){
            directTest = new int[2];
            directTest[0] = GameDefine.UP;
            directTest[1] = GameDefine.DOWN;
        } else if(typeDirect == verticalDirect){
            directTest = new int[2];
            directTest[0] = GameDefine.LEFT;
            directTest[1] = GameDefine.RIGHT;
        } else {
            directTest = new int[4];
            directTest[0] = GameDefine.DOWN;
            directTest[1] = GameDefine.UP;
            directTest[2] = GameDefine.LEFT;
            directTest[3] = GameDefine.RIGHT;
        }
        SetupDirectMayMove(directTest);
    }

    private void Start() {
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        boxCollider2d.isTrigger = true;
        StartCoroutine(EffectRegenerate());
    }

    private void Update() {
        UpdateAnimator();
        if(direct == 0 && !regenerate){UpdateDirect();}
        if(!regenerate && !sleep && !breaking && !attacking){Move();}
    }

    void UpdateAnimator(){
        animator.SetInteger("Direct", direct);
        animator.SetBool("Sleep", sleep);
        animator.SetBool("Regenerate", regenerate);
        animator.SetBool("Breaking", breaking);
        animator.SetBool("Attacking", attacking);
    }

    public void SetupDirectMayMove(int[] directs){
        foreach(int d in directs){
            DIRECT_MAY_MOVE.Add(d);
        }
    }

    public void UpdateDirect(){
        int lastDirect = direct;
        // if((!regenerate && !sleep && !breaking) || (sleep && direct != GameDefine.STAND))
        if(!regenerate && !sleep && !breaking && !attacking){
            do{
                direct = (int)DIRECT_MAY_MOVE[UnityEngine.Random.Range(0, DIRECT_MAY_MOVE.Count)];
            } while(lastDirect == direct);
        }
    }

    void Move(){
        if(direct == GameDefine.LEFT){
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.localPosition = new Vector2(
                transform.localPosition.x - SPEED_MOVE * Time.deltaTime,
                transform.localPosition.y
            );
        } else if(direct == GameDefine.RIGHT){
            transform.rotation = Quaternion.Euler(0, 0, -90);
            transform.localPosition = new Vector2(
                transform.localPosition.x + SPEED_MOVE * Time.deltaTime,
                transform.localPosition.y
            );
        } else if(direct == GameDefine.DOWN){
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.localPosition = new Vector2(
                transform.localPosition.x,
                transform.localPosition.y - SPEED_MOVE * Time.deltaTime
            );
        } else if(direct == GameDefine.UP){
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector2(
                transform.localPosition.x,
                transform.localPosition.y + SPEED_MOVE * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "WaterDamage" && !sleep && !attacking){
            SleepState();
        }

        if(other.collider.tag == AttributeSeaStart.TAG){
            if(other.gameObject.GetComponent<AttributeSeaStart>().attacking){
                if(sleep) StartCoroutine(EffectBroken());
                else if(!sleep && !attacking && !regenerate && !breaking) {
                    sleep = true;
                    StartCoroutine(EffectSleep());
                }
            } else if(!sleep){
                UpdateDirect();
            }
        }

        if(other.collider.tag == "Player" && !other.collider.GetComponent<Player>().GetChoked()){
            Player player = other.gameObject.GetComponent<Player>();
            if(sleep){
                attacking = true;
                sleep = false;
                StartCoroutine(EffectAttack(player.direct));
            } else if(!sleep && !attacking && !player.GetShieldUsing()){
                player.PlayerDie();
            }
            
        }

        if(other.collider.tag == GameDefine.TAG_BLOCK_LIMIT 
            || other.collider.tag == GameDefine.TAG_BLOCK_MAY_BROKEN
            || other.collider.tag == GameDefine.TAG_BLOCK_NOT_BROKEN){
            if(attacking){
                StartCoroutine(EffectBroken());
            } else if(!sleep){
                UpdateDirect();
            }
        }

        // Attack turtle => (turtle)
    }

    // public method
    public void SleepState(){
        sleep = true;
        direct = GameDefine.STAND;
        transform.rotation = Quaternion.Euler(0,0,0);
        StartCoroutine(EffectSleepState());
    }

    public void BrokenState(){
        StartCoroutine(EffectBroken());
    }

    // GETTER AND SETTER
    public bool IsSleep(){
        return this.sleep;
    }

    public bool IsAttacking(){return this.attacking;}
    public float GetDamage(){return this.dmg;}

    IEnumerator EffectSleep(){
        boxCollider2d.isTrigger = true;
        transform.rotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(2f);
        boxCollider2d.isTrigger = false;
        StartCoroutine(EffectSleepState());
    }

    IEnumerator EffectSleepState(){
        yield return new WaitForSeconds(TIME_SLEEP);
        if(!attacking){
            sleep = false;
            regenerate = true;
            Start();
        }
    }

    IEnumerator EffectRegenerate(){
        yield return new WaitForSeconds(TIME_REGENERATE);
        regenerate = false;
        boxCollider2d.isTrigger = false;

        // Update Direct per times
        StartCoroutine(EffectUpdateDirect());
    }

    IEnumerator EffectUpdateDirect(){
        while(true){
            UpdateDirect();
            yield return new WaitForSeconds(TIME_UPDATE_DIRECT);
        }
    }

    IEnumerator EffectBroken(){
        breaking = true;
        attacking = false;
        boxCollider2d.isTrigger = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(TIME_BROKEN);
        Destroy(gameObject);
    }

    IEnumerator EffectAttack(int direct){
        while(attacking){
            angleRotationAttack += 30;
            if(angleRotationAttack >= 360) angleRotationAttack = 0;
            transform.rotation = Quaternion.Euler(0, 0, angleRotationAttack);
            if(direct == GameDefine.LEFT){
                transform.localPosition = new Vector2(
                    transform.localPosition.x - Time.deltaTime * SPEED_ATTACK,
                    transform.localPosition.y);
            } else if(direct == GameDefine.RIGHT){
                transform.localPosition = new Vector2(
                    transform.localPosition.x + Time.deltaTime * SPEED_ATTACK,
                    transform.localPosition.y);
            } else if(direct == GameDefine.DOWN){
                transform.localPosition = new Vector2(
                    transform.localPosition.x,
                    transform.localPosition.y - Time.deltaTime * SPEED_ATTACK);
            } else if(direct == GameDefine.UP){
                transform.localPosition = new Vector2(
                    transform.localPosition.x,
                    transform.localPosition.y + Time.deltaTime * SPEED_ATTACK);
            }
            yield return new WaitForSeconds(0.02f);
        }

        
    }
}
