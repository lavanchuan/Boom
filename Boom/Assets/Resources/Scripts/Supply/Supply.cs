using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    Vector2 posLeftStart;
    Vector2 posRightStart;
    Vector2 posStart;
    float speed_fly = 2f;
    float speed_rotation = 15f;
    float angularZ;
    bool dropedItem;
    int indexItemDrop;
    Vector2 posItemDrop;
    PauseButton pauseButton;
    private void Awake() {
        Vector2 pos = FunctionMethod.GetRelativePositionRandom();
        posLeftStart = pos;
        posLeftStart.x = GameDefine.X_MIN - 10;
        posRightStart = pos;
        posRightStart.x = GameDefine.X_MAX + 10;
    }
    private void Start() {
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton")
            .GetComponent<PauseButton>();
        
        Setup();
    }

    void Setup(){
        if(UnityEngine.Random.Range(1000, 999) % 2 == 0){
            posStart = posLeftStart;
        } else {
            posStart = posRightStart;
            speed_fly *= -1;
        }
        transform.localPosition = posStart;

        indexItemDrop = RateItemObject.GetItemIndex();
        posItemDrop = FunctionMethod.GetRelativePositionRandom();
    }

    private void Update() {
        if(pauseButton.GetIsPause()) return;
        transform.localPosition = 
            new Vector2(transform.localPosition.x + speed_fly * Time.deltaTime,
            transform.localPosition.y);

        if(!dropedItem) DropItem();

        if(transform.localPosition.x >= posRightStart.x
            || transform.localPosition.x <= posLeftStart.x){
            Destroy(gameObject);
        }

        angularZ += speed_rotation;
        transform.localRotation = Quaternion.Euler(0, 0, angularZ);
    }

    void DropItem(){
        if(posItemDrop.x - 1 <= transform.localPosition.x
            && posItemDrop.x + 1 >= transform.localPosition.x){
            dropedItem = true;
            GameObject item = (GameObject)Instantiate(
                Resources.Load("Prefabs/" + Camera.main.GetComponent<GameManager>()
                .GetItemsMayDrop()[indexItemDrop]));
            item.transform.localPosition = posItemDrop;
        }
    }
}
