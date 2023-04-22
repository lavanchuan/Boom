using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayDropItem : MonoBehaviour
{
    float itemDropRate = 0.8f;
    bool isDropItem = false;
    public bool breaked = false;
    int indexItem;
    private void Awake() {
        
    }

    private void Start() {
        int min = 100, max = 1000;
        int rate = UnityEngine.Random.Range(min, max);
        if(rate <= itemDropRate * (max - min) + min){
            isDropItem = true;
            indexItem = UnityEngine.Random.Range(0, GameManager.itemsMayDrop.Count);
        }
    }

    public void BreakBlock(){
        breaked = true;
        if(isDropItem){
            GameObject item = CreateItem();
            item.transform.position = transform.position;
        }
        // Destroy(gameObject);
    }

    GameObject CreateItem(){
        return (GameObject)Instantiate(Resources.Load("prefabs/" 
            + GameManager.itemsMayDrop[indexItem]));
    }
}
