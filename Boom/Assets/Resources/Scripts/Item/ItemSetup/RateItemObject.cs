using System;
using System.Collections;

class RateItemObject{

    // Actions
    public static int GetItemIndex(){
        ItemObjectList iol = new ItemObjectList();
        int max = 9999;
        int min = 1000;
        int rateMax = (int)iol.GetMaxRate();
        int rate = UnityEngine.Random.Range(min, max + 1) % rateMax;
        ArrayList itemList = new ArrayList();
        foreach(ItemObject item in iol.items){
            if(item.rate >= rate) itemList.Add(item);
        }
        ItemObject itemObject = 
        (ItemObject)itemList[UnityEngine.Random.Range(min, max + 1) % itemList.Count];
        return iol.items.IndexOf(itemObject);
    }


}