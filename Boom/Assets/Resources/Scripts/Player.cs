using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;
    public static float MAX_SPEED = 6;
    public int direct;
    const int LEFT = 1;
    const int RIGHT = 2;
    const int UP = 3;
    const int DOWN = 4;
    const int NONE = 0;

    // item
    GameObject bom;

    // item info
    int bomQuantity = 1;
    string bomName;
    int sizeBom = 1;

    // Constructor
    private void Awake() {
        bomName = Bom.bom1;
        speed = 3;
    }

    private void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BoomItem")
        {
            bomQuantity++;
        }
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
}

