using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyItemPlaying : MonoBehaviour
{
    public static readonly float DELTA_TIME_SUPPLY_ITEM = 60f;
    public static readonly string PATH_SUPPLY = "Supply/Supply";
    float timer = 0f;
    PauseButton pauseButton;
    bool dropingItem;
    private void Start() {
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton")
            .GetComponent<PauseButton>();
    }
    private void Update() {
        if(pauseButton.GetIsPause()) return;
        timer += Time.deltaTime;
        if(timer >= DELTA_TIME_SUPPLY_ITEM && !dropingItem){
            timer = 0f;
            dropingItem = true;
            GameObject supplyItem = (GameObject)Instantiate(
                Resources.Load("Prefabs/" + PATH_SUPPLY));
            StartCoroutine(EffectDropingItem());
        }
    }

    IEnumerator EffectDropingItem(){
        yield return new WaitForSeconds(1f);
        dropingItem = false;
    }
}
