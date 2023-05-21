using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int direct;
    float speed;
    bool choked;
    public static float MAX_SPEED = 6f;
    public static float SPEED_DEFAULT = 3f;
    float lastSpeed; // speed hien tai - phuc hoi khi dung kim
    const int LEFT = 4;
    const int RIGHT = 3;
    const int UP = 2;
    const int DOWN = 1;
    const int NONE = 0;
    bool canKickBoom = false; // Đá boom
    float timeLiveInWater;
    const float TIME_LIVE_IN_WATER_DEFAULT = 10f;
    bool onLeft, onRight, onUp, onDown;
    bool isVisible = false;
    bool shieldUsing = false;
    bool usingSuperShield;
    // Component
    Animator animator;
    Rigidbody2D rigidbody2D;
    // Boom
    GameObject boom;
    GameObject timeBomb;
    GameObject shield;
    // Items info
    int boomQuantity = 1;
    const int MAX_BOOM_QUANTITY = 15;
    string boomName;
    int sizeBoom = 1;
    int needleQuantity = 0;
    int radarQuantity = 0;
    int timeBombQuantity = 0;
    bool putTimeBomb;
    int shieldQuantity = 0;
    public int money;
    float damageByBoom;
    // Statistic items pickup
    ArrayList itemsPickuped; // string : name item
    ArrayList itemsQuantity;
    // Other game object
    Sound sound;
    public GameObject dieGameObject;
    // Constructor
    private void Awake() {
        // InitDefault();

        animator = GetComponent<Animator>();
        this.timeLiveInWater = TIME_LIVE_IN_WATER_DEFAULT;
        
        // Statistic items pickuped
        itemsPickuped = new ArrayList();
        itemsQuantity = new ArrayList();
    }

    public void InitDefault(){
        sizeBoom = 1;
        boomQuantity = 1;
        needleQuantity = 0;
        this.choked = false;
        speed = SPEED_DEFAULT;
        putTimeBomb = false;
        timeBomb = null;
        damageByBoom = 4f;
        boomName = Bom.bom1;
        this.lastSpeed = this.speed;
        this.lastBoomQuantity = this.boomQuantity;
        this.lastSizeBoom = this.sizeBoom;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        sound = GameObject.FindGameObjectWithTag(Sound.TAG).GetComponent<Sound>();
        InitDefault();
    }

    private void Update() {
        if(Camera.main.GetComponent<GameManager>().GetIsPause()) return;
        animator.SetBool("Choked", choked);
        // animator.SetBool("isVisible", isVisible);
        // animator.SetBool("UsingShield", shieldUsing);
        animator.SetInteger("Direct", direct);

        // MOVE
        Move();

        // Use shield
        if(shieldUsing) shield.transform.position = transform.position;
    }

    void Move(){
        onLeft = GameObject.FindGameObjectWithTag(ControlPlayer.TAG_LEFT_DIRECT)
        .GetComponent<ControlPlayer>().leftPressed;
        onRight = GameObject.FindGameObjectWithTag(ControlPlayer.TAG_RIGHT_DIRECT)
        .GetComponent<ControlPlayer>().rightPressed;
        onUp = GameObject.FindGameObjectWithTag(ControlPlayer.TAG_UP_DIRECT)
        .GetComponent<ControlPlayer>().upPressed;
        onDown = GameObject.FindGameObjectWithTag(ControlPlayer.TAG_DOWN_DIRECT)
        .GetComponent<ControlPlayer>().downPressed;
        
        SetDirect();

        if(onLeft) left();
        if(onRight) right();
        if(onUp) up();
        if(onDown) down();
    }

    void SetDirect()
    {
        if(onDown) direct = 1;
        else if(onUp) direct = 2;
        else if(onRight) direct = 3;
        else if(onLeft) direct = 4;
        else direct = 0;
    }

    void left()
    {
        Vector3 pos = new Vector3(transform.position.x - speed * Time.deltaTime,
        transform.position.y,
        0);
        transform.position = pos;
    }

    void right()
    {
        Vector3 pos = new Vector3(transform.position.x + speed * Time.deltaTime,
        transform.position.y,
        0);
        transform.position = pos;
    }

    void up()
    {
        Vector3 pos = new Vector3(transform.position.x,
        transform.position.y + speed * Time.deltaTime,
        0);
        transform.position = pos;
    }

    void down()
    {
        Vector3 pos = new Vector3(transform.position.x,
        transform.position.y - speed * Time.deltaTime,
        0);
        transform.position = pos;
    }

    // Boom Item
    public void IncreaseBoomItem(int quantity){
        this.boomQuantity += quantity;
        if(this.boomQuantity >= MAX_BOOM_QUANTITY){
            this.boomQuantity = MAX_BOOM_QUANTITY;
        }

        this.lastBoomQuantity += quantity;
        if(this.lastBoomQuantity >= MAX_BOOM_QUANTITY){
            this.lastBoomQuantity = MAX_BOOM_QUANTITY;
        }
    }
    public void PutBoom()
    {
        if(!choked && boomQuantity > 0){
            boomQuantity--;
            CreateBoom();
        }
    }
    public bool IsBoomEmpty(){
        if(boomQuantity == 0)
            return true;
        return false;
    }
    public void CreateBoom(){
        boom = (GameObject) Instantiate(Resources.Load("Prefabs/" + boomName));
        boom.GetComponent<Bom>().size = sizeBoom;
        boom.GetComponent<Bom>().tagEffects = this.gameObject.tag;
        boom.GetComponent<Bom>().dmg = damageByBoom;
        boom.transform.localPosition = new Vector2(transform.localPosition.x,
            transform.localPosition.y);
    }
    public void IncreaseSizeBoom(int size){
        this.sizeBoom += size;
        if(this.sizeBoom > Bom.MAX_SIZE){
            this.sizeBoom = Bom.MAX_SIZE;
        }
        this.lastSizeBoom += size;
        if(this.lastSizeBoom >= Bom.MAX_SIZE){
            this.lastSizeBoom = Bom.MAX_SIZE;
        }
    }
    public void DecreaseSizeBoom(int size){
        this.sizeBoom -= size;
        if(this.sizeBoom < 1){
            this.sizeBoom = 1;
        }
        this.lastSizeBoom -= size;
        if(this.lastSizeBoom < 1){
            this.lastSizeBoom = 1;
        }
    }
    public void SetMaxSizeBoom(){
        if(this.sizeBoom != Bom.MAX_SIZE){
            this.sizeBoom = Bom.MAX_SIZE;
        }
        this.lastSizeBoom = Bom.MAX_SIZE;
    }
    public int GetSizeBoom(){return this.sizeBoom;}
    // Shoes and Speed
    public void IncreaseSpeed(float speed){
        this.speed += speed;
        if(this.speed > MAX_SPEED){
            this.speed = MAX_SPEED;
        }
        this.lastSpeed += speed;
        if(this.lastSpeed > MAX_SPEED){
            this.lastSpeed = MAX_SPEED;
        }
    }
    public void DecreaseSpeed(float speed){
        this.speed -= speed;
        if(this.speed < SPEED_DEFAULT){
            this.speed = SPEED_DEFAULT;
        }
        this.lastSpeed -= speed;
        if(this.lastSpeed < SPEED_DEFAULT){
            this.lastSpeed = SPEED_DEFAULT;
        }
    }
    public void SetMaxSpeed(){
        if(this.speed != MAX_SPEED){
            this.speed = MAX_SPEED;
        }
        this.lastSpeed = MAX_SPEED;
    }
    // Kick boom
    public void setCanKickBoom(bool kick){
        this.canKickBoom = kick;
    }
    public bool getCanKickBoom(){
        return this.canKickBoom;
    }
    // State Choke water
    public void StateChoke(int speed){
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        this.lastSpeed = this.speed;
        this.speed = speed;
        this.isVisible = false; // Stop visible -- Bị phát hiện và dừng tàng hình
        StartCoroutine(LiveInWater()); // Chạy thời gian sống trong nước :v
    }
    public void SetChoked(bool choked){
        this.choked = choked;
    }
    public bool GetChoked(){
        return this.choked;
    }
    public void RecoverAfterChoke(){
        this.choked = false;
        this.speed = lastSpeed;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        CreateEffect(Effect.EFFECT_USE_NEEDLE);
    }
    // Needle
    public void IncreaseNeedleQuantity(int quantity){
        this.needleQuantity += quantity;
    }

    public void DecreaseneedleQuantity(int quantity){
        this.needleQuantity -= quantity;
    }

    public void UseKim(){
        if(choked && needleQuantity > 0){
            this.needleQuantity--;
            RecoverAfterChoke();
        }
    }
    // Choked and time(die or living for time)
    IEnumerator LiveInWater(){
        yield return new WaitForSeconds(timeLiveInWater);
        if(this.choked){
            PlayerDie();
        }
    }
    // Violet Demoniac Mask
    public void StateContratyDirectByVioletDemoniacMask(float effectTime){
        if(this.speed > 0) this.speed *= -1;
        StartCoroutine(ContratyDirectByVioletDemoniacMask(effectTime));
    }

    IEnumerator ContratyDirectByVioletDemoniacMask(float effectTime){
        yield return new WaitForSeconds(effectTime);
        if(this.speed < 0) this.speed *= -1;
    }

    bool autoPutBoom = false;
    public void StaetAutoPutBoomByVioletDemoniacMask(float effectTime, float deltaTimePut){
        autoPutBoom = true;
        StartCoroutine(AutoPutBoom(deltaTimePut));
        StartCoroutine(AutoPutBoomByVioletDemoniacMask(effectTime));
    }

    IEnumerator AutoPutBoomByVioletDemoniacMask(float effectTime){
        yield return new WaitForSeconds(effectTime);
        if(this.autoPutBoom){
            autoPutBoom = false;
        }
    }

    IEnumerator AutoPutBoom(float deltaTimePut){
        while(autoPutBoom){
            PutBoom();
            yield return new WaitForSeconds(deltaTimePut);
        }
    }

    // Visible Overcoat
    float zForVisible = -10;
    public void StateVisibleByVisibleOverCoat(float effectTime){
        isVisible = true;
        StartCoroutine(Visible(effectTime));
    }

    IEnumerator Visible(float effectTime){
        yield return new WaitForSeconds(effectTime);
        if(isVisible){
            isVisible = false;
        }
    }

    public bool GetVisible(){return this.isVisible;}

    // Super Shield
    int lastSizeBoom;
    int lastBoomQuantity;
    public void StateSuperShield(float effectTime){
        if(!usingSuperShield){
            this.lastSpeed = this.speed;
            this.lastSizeBoom = this.sizeBoom;
            this.lastBoomQuantity = this.boomQuantity;
            this.speed = MAX_SPEED;
            this.sizeBoom = Bom.MAX_SIZE;
            this.boomQuantity = MAX_BOOM_QUANTITY;
            StartCoroutine(SuperShield(effectTime));
        } else {
            StopCoroutine(SuperShield(effectTime));
            StartCoroutine(SuperShield(effectTime));
        }
        
    }

    IEnumerator SuperShield(float effectTime){
        yield return new WaitForSeconds(effectTime);
        this.speed = this.lastSpeed;
        this.sizeBoom = this.lastSizeBoom;
        this.boomQuantity = this.lastBoomQuantity;
    }

    // Shield
    public void IncreaseShieldQuantity(int quantity){
        this.shieldQuantity += quantity;
    }

    public void DecreaseShieldQuantity(int quantity){
        this.shieldQuantity -= quantity;
        if(this.shieldQuantity < 0) this.shieldQuantity = 0;
    }

    public int GetShieldQuantity(){return this.shieldQuantity;}

    public bool GetShieldUsing(){return this.shieldUsing;}
    public void SetLastSpeed(float lastSpeed){this.lastSpeed = lastSpeed;}
    public float GetLastSpeed(){return this.lastSpeed;}
    public void SetLastSizeBoom(int lastSizeBoom){this.lastSizeBoom = lastSizeBoom;}
    public int GetLastSizeBoom(){return this.lastSizeBoom;}
    public void SetLastBoomQuantity(int lastBoomQuantity){this.lastBoomQuantity = lastBoomQuantity;}
    public int GetLastBoomQuantity(){return this.lastBoomQuantity;}

    public void UseShield(){
        if(this.shieldQuantity > 0){
            this.shieldQuantity--;
            shieldUsing = true;
            shield = CreateUseShield();
            StartCoroutine(ShieldEffect(shield, Shield.effectTime));
        }
    }
    GameObject CreateUseShield(){
        return (GameObject)Instantiate(
            Resources.Load("Prefabs/ItemUse/EnviromentAuto/UseShield"));
    }
    IEnumerator ShieldEffect(GameObject shield, float effectTime){
        yield return new WaitForSeconds(effectTime);
        Destroy(shield);
        shieldUsing = false;
    }

    // Radar
    bool isUseRadar = false;
    public void IncreaseRadarQuantity(int quantity){this.radarQuantity += quantity;}
    public void DecreaseRadarQuantity(int quantity){
        this.radarQuantity -= quantity;
        if(this.radarQuantity < 0){this.radarQuantity = 0;}
    }
    public int GetRadarQuantity(){return this.radarQuantity;}
    public bool GetIsUseRadar(){return this.isUseRadar;}
    public void UseRadar(){
        if(!isUseRadar && GetRadarQuantity() > 0){
            // Debug.Log("Use Radar");
            DecreaseRadarQuantity(1);
            this.isUseRadar = true;
            StartCoroutine(RadarEffect(Radar.effectTime));
        }
    }
    IEnumerator RadarEffect(float effectTime){
        yield return new WaitForSeconds(effectTime);
        this.isUseRadar = false;
    }

    // Time bomb
    public void IncreaseTimeBombQuantity(int quantity){
        timeBombQuantity += quantity;
    }

    public void DecreaseTimeBombQuantity(int quantity){
        timeBombQuantity -= quantity;
        if(timeBombQuantity < 0){timeBombQuantity = 0;}
    }

    public int GetTimeBombQuantity(){return timeBombQuantity;}

    public bool GetPutTimeBomb(){
        try{
            if(timeBomb != null && putTimeBomb) return putTimeBomb;
        } catch (Exception e){
            putTimeBomb = false;
        }
        return putTimeBomb;
    }

    public void PutTimeBomb(){
        if(GetTimeBombQuantity() > 0){
            putTimeBomb = true;
            timeBomb = (GameObject)Instantiate(Resources.Load("prefabs/TimeBomb"));
            timeBomb.GetComponent<Bom>().SetEffectTime(999f);
            timeBomb.GetComponent<Bom>().size = sizeBoom;
            timeBomb.GetComponent<Bom>().tagEffects = this.gameObject.tag;
            timeBomb.GetComponent<Bom>().dmg = damageByBoom;
            timeBomb.transform.position = new Vector2(transform.localPosition.x,
            transform.localPosition.y - transform.localScale.y/2);
            boomQuantity--;
            DecreaseTimeBombQuantity(1);
        }
    }

    public void ActiveExplosiveTimeBomb(){
        putTimeBomb = false;
        timeBomb.GetComponent<Bom>().ExplosiveBoom();
    }
    // MONEY COIN
    public void IncreaseMoney(int money){
        this.money += money;
        // UI
        Debug.Log("Money add " + money);
    }
    
    // DIE...
    public void PlayerDie(){
        sound.PlaySound(Sound.PLAYER_DIE);
        DropItemsPickuped();
        dieGameObject.GetComponent<DieGameObject>().hiddenDieUI = false;
        Destroy(gameObject);
    }

    // statisitc items pickuped
    int GetIndexItemPickuped(string nameItem){
        return this.itemsPickuped.IndexOf(nameItem);
    }
    public void AddItemPickup(string nameItem, int quantity){
        if(GetIndexItemPickuped(nameItem) == -1){
            this.itemsPickuped.Add(nameItem);
            this.itemsQuantity.Add(quantity);
        } else {
            this.itemsQuantity[GetIndexItemPickuped(nameItem)] = 
                (int)this.itemsQuantity[GetIndexItemPickuped(nameItem)] + quantity;
        }
    }

    void DropItemsPickuped(){
        for(int i = 0; i < this.itemsPickuped.Count; i++){
            for(int j = 0; j < (int)this.itemsQuantity[i]; j++){
                GameObject go;
                if(this.itemsPickuped[i].Equals("BinhNuoc")
                    || this.itemsPickuped[i].Equals("BongGai")
                    || this.itemsPickuped[i].Equals("GiayDo")
                    || this.itemsPickuped[i].Equals("GiayVang")
                    || this.itemsPickuped[i].Equals("GiayXanh")
                    || this.itemsPickuped[i].Equals("GreenDemoniacMask")
                    || this.itemsPickuped[i].Equals("RedDemoniacMask")
                    || this.itemsPickuped[i].Equals("VioletDemoniacMask")
                    || this.itemsPickuped[i].Equals("VisibleOvercoat")
                    || this.itemsPickuped[i].Equals("SuperShield")){
                        go = (GameObject)Instantiate(Resources.Load(
                            GameDefine.PATH_PREFABS_ITEM_AUTO_USE + itemsPickuped[i]));
                } else if(this.itemsPickuped[i].Equals("Kim")
                    || this.itemsPickuped[i].Equals("Radar")
                    || this.itemsPickuped[i].Equals("Shield")
                    || this.itemsPickuped[i].Equals("TimeBomb")){
                        go = (GameObject)Instantiate(Resources.Load(
                            GameDefine.PATH_PREFABS_ITEM_USE + itemsPickuped[i]));
                } else if(this.itemsPickuped[i].Equals("GoldCoin")
                    || this.itemsPickuped[i].Equals("BronzeCoin")
                    || this.itemsPickuped[i].Equals("SilverCoin")
                    || this.itemsPickuped[i].Equals("GoldenBag")){
                        go = (GameObject)Instantiate(Resources.Load(
                            GameDefine.PATH_PREFABS_COIN + itemsPickuped[i]));
                } else {
                    go = new GameObject();
                }
                go.transform.localPosition =  FunctionMethod.GetRelativePositionRandom();
                
            }
        }

    }

    // CREATE EFFECT
    public void CreateEffect(string effectName){
        Effect effect = 
            FunctionMethod.CreateEffect(effectName).GetComponent<Effect>();
        effect.owner = gameObject;
    }

}
