using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDemoniacMask : MonoBehaviour
{
    string name = "Mặt nạ quỷ xanh";
    string descript = "Mang mặt nạ làm người chơi mất đi một hiệu ứng tăng lực";
    const int DECREASE_SPEED = 0;
    const int DECREASE_SIZE_BOOM = 1;
    int count = 2;
    int current;
    float time;
    float timeCanDestroy = 1f;
    private void Update() {
        time += Time.deltaTime;
    }

    private void Awake() {
        current = UnityEngine.Random.Range(0, 1000) % count;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !other.GetComponent<Player>().GetChoked()){
            Player playerComponent = (Player)other.GetComponent<Player>();
            switch(current){
                case DECREASE_SPEED:
                    playerComponent.DecreaseSpeed(1);
                    break;
                case DECREASE_SIZE_BOOM:
                    playerComponent.DecreaseSizeBoom(1);
                    break;
            }
            Destroy(gameObject);
        }

        if(other.tag == "WaterDamage" && time >= timeCanDestroy){
            Destroy(gameObject);
        }
    }
}
