using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool choked; // sặc nước
    float speed;
    public static float MAX_SPEED = 6;
    float lastSpeed; // speed hien tai - phuc hoi khi dung kim
    public int direct;
    const int LEFT = 1;
    const int RIGHT = 2;
    const int UP = 3;
    const int DOWN = 4;
    const int NONE = 0;
    bool canKickBoom = false; // Đá boom
    float timeLiveInWater;
    const float TIME_LIVE_IN_WATER_DEFAULT = 3; // 3s

    // item
    GameObject bom;

    // item info
    int bomQuantity = 1;
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
        speed = 3;
    }
    private void Update()
    {
        // load animation
        animator.SetBool("choked", choked);

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
        if(bomQuantity > 0){
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
        bom.transform.position = transform.position;
        bom.GetComponent<Bom>().size = sizeBom;
    }

    // set size boom
    public void IncreaseSizeBoom(int size){
        this.sizeBom += size;
        if(this.sizeBom > Bom.MAX_SIZE){
            this.sizeBom = Bom.MAX_SIZE;
        }
    }

    public void SetMaxSizeBoom(){
        if(this.sizeBom != Bom.MAX_SIZE){
            this.sizeBom = Bom.MAX_SIZE;
        }
    }

    // speed
    public void IncreaseSpeed(float speed){
        this.speed += speed;
        if(this.speed > speed){
            this.speed = MAX_SPEED;
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
        StartCoroutine(LiveInWater()); // Chạy thời gian sống trong nước :v
    }

    public void RecoverAfterChoke(){
        this.choked = false;
        this.speed = lastSpeed;
        Debug.Log("lastSpeed" + lastSpeed);
        Debug.Log("speed" + speed);
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
}

