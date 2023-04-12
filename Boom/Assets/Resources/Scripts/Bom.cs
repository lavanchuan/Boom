using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    // types
    public static string bom1 = "Bom";
    public int size;
    public static int MAX_SIZE = 6;

    // component
    BoxCollider2D boxCollider2d;
    
    // Setup when player exit bom
    int speed = 0;
    int speedDefault = 10;
    Vector2 currentPos;
    GameDefine.DIRECT direct;
    
    float timer = 5;
    private void Awake()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
        direct = GameDefine.DIRECT.NONE;
        boxCollider2d = GetComponent<BoxCollider2D>();
        Boom();
    }

    private void Update() {
        currentPos = transform.localPosition; // [update]
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
            boxCollider2d.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log(this.gameObject.GetComponent<BoxCollider2D>().enabled);
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
        if(other.collider.tag == "Block"){
            this.speed = 0;
            this.direct = GameDefine.DIRECT.NONE;
            return;
        }
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
        // damage
        GameObject damage;
        Vector3 pos = transform.position;
        int i;
        // center
        damage = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
        damage.transform.position = transform.position;
        // left
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x - i * transform.localScale.x, pos.y, pos.z);
        }
        // right
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x + i * transform.localScale.x, pos.y, pos.z);
        }
        // top
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x, pos.y + i * transform.localScale.y, pos.z);
        }
        // bottom
        for (i = 1; i <= size; i++)
        {
            GameObject dtemp = (GameObject)Instantiate(Resources.Load("Prefabs/Damage"));
            dtemp.transform.position = new Vector3(pos.x, pos.y - i * transform.localScale.y, pos.z);
        }

        // damage
        yield return new WaitForSeconds(0.1f);
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
}
