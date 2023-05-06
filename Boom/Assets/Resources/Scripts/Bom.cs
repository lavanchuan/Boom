using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    // types
    public static string bom1 = "Bom";
    public static string bom2 = "Boom2";
    public int size = 3;
    public static int MAX_SIZE = 6;
    public string tagEffects;
    public bool onRadar;

    // component
    CircleCollider2D circleCollider2D;
    
    // Setup when player exit bom
    int speed = 0;
    int speedDefault = 10;
    Vector2 currentPos;
    GameDefine.DIRECT direct;
    
    float timer = 5f;
    // Damage ->
    public float dmg;

    // Check size explosive game object
    public float sizeLeft, sizeRight, sizeUp, sizeDown;

    private void Awake()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
        direct = GameDefine.DIRECT.NONE;
        circleCollider2D = GetComponent<CircleCollider2D>();
        onRadar = false;
    }

    private void Start() {
        sizeLeft = sizeUp = sizeRight = sizeDown = size;
        CheckSizeBoom csbGOL, csbGOR, csbGOD, csbGOU;
        GameObject go1 = (GameObject)Instantiate(Resources.Load(CheckSizeBoom.PATH_PREFABS));
        csbGOL = go1.GetComponent<CheckSizeBoom>();
        csbGOL.target = gameObject;
        csbGOL.direct = GameDefine.LEFT;

        GameObject go2 = (GameObject)Instantiate(Resources.Load(CheckSizeBoom.PATH_PREFABS));
        csbGOR = go2.GetComponent<CheckSizeBoom>();
        csbGOR.target = gameObject;
        csbGOR.direct = GameDefine.RIGHT;

        GameObject go3 = (GameObject)Instantiate(Resources.Load(CheckSizeBoom.PATH_PREFABS));
        csbGOD = go3.GetComponent<CheckSizeBoom>();
        csbGOD.target = gameObject;
        csbGOD.direct = GameDefine.DOWN;

        GameObject go4 = (GameObject)Instantiate(Resources.Load(CheckSizeBoom.PATH_PREFABS));
        csbGOU = go4.GetComponent<CheckSizeBoom>();
        csbGOU.target = gameObject;
        csbGOU.direct = GameDefine.UP;

        Boom();
    }

    private void Update() {
        Debug.Log("SIZE:\t" + size);
        // sizeLeft = csbGO.sizeExplosiveTargetLeft;
        // sizeRight = csbGO.sizeExplosiveTargetRight;
        // sizeUp = csbGO.sizeExplosiveTargetUp;
        // sizeDown = csbGO.sizeExplosiveTargetDown;
        if(sizeLeft == -1) sizeLeft = size;
        if(sizeRight == -1) sizeRight = size;
        if(sizeUp == -1) sizeUp = size;
        if(sizeDown == -1) sizeDown = size;


        currentPos = transform.localPosition; // [update]

        try{
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            onRadar = go.GetComponent<Player>().GetIsUseRadar();
            if(onRadar){
                ViewRadar();// for size
            }
        } catch(Exception e){}
    }

    private void FixedUpdate() {
        if(this.direct != GameDefine.DIRECT.NONE){
            switch(this.direct){
                case GameDefine.DIRECT.UP:
                    currentPos.y = currentPos.y + speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.DOWN:
                    currentPos.y = currentPos.y - speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.LEFT:
                    currentPos.x = currentPos.x - speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.RIGHT:
                    currentPos.x = currentPos.x + speed * Time.deltaTime;
                    break;
            }
            this.transform.localPosition = currentPos;
        }
    }

    // Player di chuyển ra khỏi quả bam đang đặt sẽ đặt trạng thái quả bom thành block
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            circleCollider2D.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Damage.TAG){
            ExplosiveBoom();
        }

        if(other.tag == AttributeSeaStart.TAG){
            other.gameObject.GetComponent<AttributeSeaStart>().UpdateDirect();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // with player
        if(other.collider.tag == "Player"){
            GameObject player = (GameObject)GameObject.FindGameObjectWithTag("Player");
            bool kickBoom = player.GetComponent<Player>().getCanKickBoom();
            if(kickBoom){
                if(this.speed == 0){
                    this.speed = speedDefault;
                    currentPos = transform.position; //[update]
                } else {
                    this.speed = 0;
                    this.direct = GameDefine.DIRECT.NONE;
                }
                
                UpdateDirect(other);
            }
            return;
        }

        // with block,...
        if(other.collider.tag == GameDefine.TAG_BLOCK_MAY_BROKEN 
            || other.collider.tag == GameDefine.TAG_BLOCK_NOT_BROKEN
            || other.collider.tag == GameDefine.TAG_BLOCK_LIMIT){
            this.speed = 0;
            this.direct = GameDefine.DIRECT.NONE;
            return;
        }

        // with water damage
        if(other.collider.tag == Damage.TAG){
            ExplosiveBoom();
        }
    }

    // Set timer
    public void SetEffectTime(float effectTime){
        this.timer = effectTime;
    }

    // Phat no
    void Boom()
    {
        StartCoroutine(IBoom(timer));
    }

    // ANI
    IEnumerator IBoom(float time)
    {
        yield return new WaitForSeconds(time - 1);
        // // damage
        // GameObject damage;
        // Vector2 pos = transform.localPosition;
        // pos.y = pos.y + transform.localScale.y/2;
        // int i;
        // // center
        // damage = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        // damage.transform.localPosition = pos;
        // // left
        // for (i = 1; i <= size; i++)
        // {
        //     GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        //     dtemp.transform.localPosition = new Vector2(pos.x - i * transform.localScale.x, pos.y);
        //     if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
        //         break;
        //     }
        // }
        // // right
        // for (i = 1; i <= size; i++)
        // {
        //     GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        //     dtemp.transform.localPosition = new Vector2(pos.x + i * transform.localScale.x, pos.y);
        //     if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
        //         break;
        //     }
        // }
        // // top
        // for (i = 1; i <= size; i++)
        // {
        //     GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        //     dtemp.transform.localPosition = new Vector2(pos.x, pos.y + i * transform.localScale.y);
        //     if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
        //         break;
        //     }
        // }
        // // bottom
        // for (i = 1; i <= size; i++)
        // {
        //     GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        //     dtemp.transform.localPosition = new Vector2(pos.x, pos.y - i * transform.localScale.y);
        //     if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
        //         break;
        //     }
        // }

        // // damage
        // yield return new WaitForSeconds(0.1f);
        // RestoreBoomQuantity(tagEffects);
        // Destroy(gameObject);

        // Explosive ...
        ExplosiveBoom();
    }

    // Explosive
    public void ExplosiveBoom(){
        Debug.Log("Size: " + size);
        Debug.Log("Size Left:\t" + sizeLeft);
        Debug.Log("Size RIGHT:\t" + sizeRight);
        Debug.Log("Size DOWN:\t" + sizeDown);
        Debug.Log("Size UP:\t" + sizeUp);

        // damage
        GameObject damage;
        Vector2 pos = transform.localPosition;
        pos.y = pos.y;// + transform.localScale.y/2;
        int i;
        // center
        damage = (GameObject)Instantiate(Resources.Load("Prefabs/DamageCenter"));
        damage.GetComponent<Damage>().SetDmg(dmg);
        damage.GetComponent<Damage>().effects = tagEffects;
        damage.transform.localPosition = pos;
        // left
        for (i = 1; i <= sizeLeft; i++)
        {
            GameObject dtemp;
            if(i == (int)sizeLeft){dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageLeft2"));}
            else {dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageLeft1"));}
            dtemp.GetComponent<Damage>().SetDmg(dmg);
            dtemp.GetComponent<Damage>().effects = tagEffects;
            dtemp.transform.localPosition = new Vector2(pos.x - i * transform.localScale.x, pos.y);
            if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
                break;
            }
        }
        // right
        for (i = 1; i <= sizeRight; i++)
        {
            GameObject dtemp;
            if(i == (int)sizeRight){dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageRight2"));}
            else {dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageRight1"));}
            dtemp.GetComponent<Damage>().SetDmg(dmg);
            dtemp.GetComponent<Damage>().effects = tagEffects;
            dtemp.transform.localPosition = new Vector2(pos.x + i * transform.localScale.x, pos.y);
            if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
                break;
            }
        }
        // top
        for (i = 1; i <= sizeUp; i++)
        {
            GameObject dtemp;
            if(i == (int)sizeUp){dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageUp2"));}
            else {dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageUp1"));}
            dtemp.GetComponent<Damage>().SetDmg(dmg);
            dtemp.GetComponent<Damage>().effects = tagEffects;
            dtemp.transform.localPosition = new Vector2(pos.x, pos.y + i * transform.localScale.y);
            if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
                break;
            }
        }
        // bottom
        for (i = 1; i <= sizeDown; i++)
        {
            GameObject dtemp;
            if(i == (int)sizeDown){dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageDown2"));}
            else {dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/DamageDown1"));}
            dtemp.GetComponent<Damage>().SetDmg(dmg);
            dtemp.GetComponent<Damage>().effects = tagEffects;
            dtemp.transform.localPosition = new Vector2(pos.x, pos.y - i * transform.localScale.y);
            if(!GameObjectCheck.CheckExplosiveArea(dtemp)){
                break;
            }
        }

        // damage
        // yield return new WaitForSeconds(0.1f);
        RestoreBoomQuantity(tagEffects);
        // csbGO.DestroyCheckSizeBoom();
        Destroy(gameObject);
    }

    // UpdateDirect(Collision2D other)
    void UpdateDirect(Collision2D other){
        if(this.speed > 0){
            if(other.contacts[0].normal.x < 0){
                this.direct = GameDefine.DIRECT.LEFT;
            } else
            if(other.contacts[0].normal.x > 0){
                this.direct = GameDefine.DIRECT.RIGHT;
            } else
            if(other.contacts[0].normal.y < 0){
                this.direct = GameDefine.DIRECT.DOWN;
            } else
            if(other.contacts[0].normal.y > 0){
                this.direct = GameDefine.DIRECT.UP;
            }
        }
    }

    // Restore Boom Quantity
    void RestoreBoomQuantity(string tag){
        if(tag == "Player"){
            try{
                GameObject.FindGameObjectWithTag(tag).GetComponent<Player>().IncreaseBoomItem(1);
            } catch (Exception e){}
        }
    }

    // ViewRadar(GameObject go)
    void ViewRadar(){
        Vector2 pos = transform.localPosition;
        pos.y = pos.y + transform.localScale.y/2;
        int i;
        // center
        GameObject temp = CreateRadarPoint();
        temp.transform.localPosition = pos;
        // left
        for (i = 1; i <= sizeLeft; i++)
        {
            GameObject dtemp = CreateRadarPoint();
            dtemp.transform.localPosition = new Vector2(pos.x - i * transform.localScale.x, pos.y);
        }
        // right
        for (i = 1; i <= sizeRight; i++)
        {
            GameObject dtemp = CreateRadarPoint();
            dtemp.transform.localPosition = new Vector2(pos.x + i * transform.localScale.x, pos.y);
        }
        // top
        for (i = 1; i <= sizeUp; i++)
        {
            GameObject dtemp = CreateRadarPoint();
            dtemp.transform.localPosition = new Vector2(pos.x, pos.y + i * transform.localScale.y);
        }
        // bottom
        for (i = 1; i <= sizeDown; i++)
        {
            GameObject dtemp = CreateRadarPoint();
            dtemp.transform.localPosition = new Vector2(pos.x, pos.y - i * transform.localScale.y);
        }
    }

    // Create Radar Point
    GameObject CreateRadarPoint(){
        GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/ItemUse/EnviromentAuto/RadarPoint"));
        dtemp.GetComponent<RadarPoint>().effectTime = timer - 0.5f;
        return dtemp;
    }

        
}
