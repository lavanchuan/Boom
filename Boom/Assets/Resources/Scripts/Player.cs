using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool choked; // sặc nước
    float speed;
    public static float MAX_SPEED = 6f;
    public static float SPEED_DEFAULT = 3f;
    float lastSpeed; // speed hien tai - phuc hoi khi dung kim
    public int direct;
    const int LEFT = 1;
    const int RIGHT = 2;
    const int UP = 3;
    const int DOWN = 4;
    const int NONE = 0;
    bool canKickBoom = false; // Đá boom
    float timeLiveInWater;
    const float TIME_LIVE_IN_WATER_DEFAULT = 5f; // 5s

    // item
    GameObject bom;

    // item info
    int bomQuantity = 1;
    const int MAX_BOOM_QUANTITY = 15;
    string bomName;
    int sizeBom = 1;
    int kimQuantity = 0;

    // COMPONENT
    Animator animator;

    // Constructor
    private void Awake() {
        animator = GetComponent<Animator>();
        this.choked = false;
        this.timeLiveInWater = TIME_LIVE_IN_WATER_DEFAULT;
        bomName = Bom.bom1;
        speed = SPEED_DEFAULT;
    }
    private void Update()
    {
        // show info
        // Debug.Log("Shield Quantity: " + this.shieldQuantity);

        // load animation
        animator.SetBool("choked", choked);
        animator.SetBool("isVisible", isVisible);
        animator.SetBool("isUseShield", shieldUsing);

        // move with direct
        switch (direct)
        {
            case LEFT:
                left();
                break;
            case RIGHT:
                right();
                break;
            case DOWN:
                down();
                break;
            case UP:
                up();
                break;
        }

        // direct = 0;
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

    public static int GetLEFT()
    {
        return LEFT;
    }

    public static int GetRIGHT()
    {
        return RIGHT;
    }

    public static int GetUP()
    {
        return UP;
    }

    public static int GetDOWN()
    {
        return DOWN;
    }

    // Boom Item
    public void IncreaseBoomItem(int quantity){
        this.bomQuantity += quantity;
    }

    // Dat bom
    public void DatBom()
    {
        if(!choked && bomQuantity > 0){
            bomQuantity--;
            CreateBom();
        }
    }

    // Check empty bom
    public bool IsBoomEmpty(){
        if(bomQuantity == 0)
            return true;
        return false;
    }

    // Set Direct
    public void SetDirect(GameDefine.DIRECT direct)
    {
        switch (direct)
        {
            case GameDefine.DIRECT.LEFT:
                this.direct = LEFT;
                break;
            case GameDefine.DIRECT.RIGHT:
                this.direct = RIGHT;
                break;
            case GameDefine.DIRECT.UP:
                this.direct = UP;
                break;
            case GameDefine.DIRECT.DOWN:
                this.direct = DOWN;
                break;
            default:
                this.direct = NONE;
                break;
        }
    }

    // Set Boom
    public void CreateBom(){
        bom = (GameObject) Instantiate(Resources.Load("Prefabs/" + bomName));
        bom.transform.localPosition = new Vector2(transform.localPosition.x,
            transform.localPosition.y - bom.transform.localScale.y/2);
        bom.GetComponent<Bom>().size = sizeBom;
        bom.GetComponent<Bom>().tagEffects = this.gameObject.tag;
    }

    // set size boom
    public void IncreaseSizeBoom(int size){
        this.sizeBom += size;
        if(this.sizeBom > Bom.MAX_SIZE){
            this.sizeBom = Bom.MAX_SIZE;
        }
    }

    public void DecreaseSizeBoom(int size){
        this.sizeBom -= size;
        if(this.sizeBom < 1){
            this.sizeBom = 1;
        }
    }

    public void SetMaxSizeBoom(){
        if(this.sizeBom != Bom.MAX_SIZE){
            this.sizeBom = Bom.MAX_SIZE;
        }
    }

    public int GetBoomSize(){return this.sizeBom;}

    // speed
    public void IncreaseSpeed(float speed){
        this.speed += speed;
        if(this.speed > speed){
            this.speed = MAX_SPEED;
        }
    }

    public void DecreaseSpeed(float speed){
        this.speed -= speed;
        if(this.speed < SPEED_DEFAULT){
            this.speed = SPEED_DEFAULT;
        }
    }

    public void SetMaxSpeed(){
        if(this.speed != MAX_SPEED){
            this.speed = MAX_SPEED;
        }
    }

    // To kick boom
    public void setCanKickBoom(bool kick){
        this.canKickBoom = kick;
    }

    public bool getCanKickBoom(){
        return this.canKickBoom;
    }

    // Choke water
    public void SetChoked(bool choked){
        this.choked = choked;
    }

    public bool GetChoked(){
        return this.choked;
    }

    // State choke water
    public void StateChoke(int speed){
        this.lastSpeed = this.speed;
        this.speed = speed;
        this.isVisible = false; // Stop visible -- Bị phát hiện và dừng tàng hình
        StartCoroutine(LiveInWater()); // Chạy thời gian sống trong nước :v
    }

    public void RecoverAfterChoke(){
        this.choked = false;
        this.speed = lastSpeed;
        // Debug.Log("lastSpeed" + lastSpeed);
        // Debug.Log("speed" + speed);
    }

    // Kim
    public void IncreaseKimQuantity(int quantity){
        this.kimQuantity += quantity;
    }

    public void DecreaseKimQuantity(int quantity){
        this.kimQuantity -= quantity;
    }

    public void UseKim(){
        if(choked && kimQuantity > 0){
            this.kimQuantity--;
            RecoverAfterChoke();
        }
    }

    // Choked and time(die or living for time)
    IEnumerator LiveInWater(){
        yield return new WaitForSeconds(timeLiveInWater);
        if(this.choked){
            Debug.Log("PLAYER ... DIE");
            Destroy(gameObject);
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
            DatBom();
            yield return new WaitForSeconds(deltaTimePut);
        }
    }

    // Visible Overcoat
    bool isVisible = false;
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
        this.lastSpeed = this.speed;
        this.lastSizeBoom = this.sizeBom;
        this.lastBoomQuantity = this.bomQuantity;
        this.speed = MAX_SPEED;
        this.sizeBom = Bom.MAX_SIZE;
        this.bomQuantity = MAX_BOOM_QUANTITY;
        StartCoroutine(SuperShield(effectTime));
    }

    IEnumerator SuperShield(float effectTime){
        yield return new WaitForSeconds(effectTime);
        this.speed = this.lastSpeed;
        this.sizeBom = this.lastSizeBoom;
        this.bomQuantity = this.lastBoomQuantity;
    }

    // Shield
    int shieldQuantity = 0;
    bool shieldUsing = false;
    public void IncreaseShieldQuantity(int quantity){
        this.shieldQuantity += quantity;
    }

    public void DecreaseShieldQuantity(int quantity){
        this.shieldQuantity -= quantity;
        if(this.shieldQuantity < 0) this.shieldQuantity = 0;
    }

    public int GetShieldQuantity(){return this.shieldQuantity;}

    public bool GetShieldUsing(){return this.shieldUsing;}

    public void UseShield(){
        if(this.shieldQuantity > 0){
            this.shieldQuantity--;
            shieldUsing = true;
            StartCoroutine(ShieldEffect(Shield.effectTime));
        }
    }

    IEnumerator ShieldEffect(float effectTime){
        yield return new WaitForSeconds(effectTime);
        shieldUsing = false;
    }

    // Radar
    int radarQuantity = 0;
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
    
}

