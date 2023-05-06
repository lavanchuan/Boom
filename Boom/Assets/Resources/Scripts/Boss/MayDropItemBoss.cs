using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayDropItemBoss : MonoBehaviour
{
    int itemQuantity;
    const int MIN_ITEM_QUANTITY = 10;
    const int MAX_ITEM_QUANTITY = 30;
    ArrayList items;
    private void Start() {
        itemQuantity = UnityEngine.Random.Range(MIN_ITEM_QUANTITY, MAX_ITEM_QUANTITY + 1);
        items = new ArrayList();
        int indexItem;
        ArrayList coinsMayDrop = Camera.main.GetComponent<GameManager>().GetCoinsMayDrop();
        for(int i = 0; i < itemQuantity; i++){
            indexItem = UnityEngine.Random.Range(0, coinsMayDrop.Count);
            items.Add(coinsMayDrop[indexItem]);
        }
    }
    public void DropItems(){
        foreach(string pathItem in items){
            // Test with item
            GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/" + pathItem));
            item.transform.localPosition = FunctionMethod.GetRelativePositionRandom();
        }
    }
}
