using System.Collections;

class ItemObjectList{
    public ArrayList items;
    public ItemObjectList(){
        Setup();
    }

    void Setup(){
        items = new ArrayList();
        // ITEM AUTO USE
        items.Add(new ItemObject("Bomb", 85f));
        items.Add(new ItemObject("BinhNuoc", 75f));
        items.Add(new ItemObject("BongGai", 25f));
        items.Add(new ItemObject("GiayDo", 60f));
        items.Add(new ItemObject("GiayVang", 20f));
        items.Add(new ItemObject("GiayXanh", 35f));
        items.Add(new ItemObject("GreenDemoniacMask", 40f));
        items.Add(new ItemObject("RedDemoniacMask", 15f));
        items.Add(new ItemObject("VioletDemoniacMask", 45f));
        items.Add(new ItemObject("VisibleOvercoat", 10f));
        items.Add(new ItemObject("SuperShield", 15f));
        //ITEM USE
        items.Add(new ItemObject("Kim", 40f));
        items.Add(new ItemObject("Radar", 55f));
        items.Add(new ItemObject("Shield", 35f));
        items.Add(new ItemObject("TimeBomb", 45f));
        // COIN
        items.Add(new ItemObject("GoldCoin", 10f));
        items.Add(new ItemObject("BronzeCoin", 10f));
        items.Add(new ItemObject("SilverCoin", 10f));
        items.Add(new ItemObject("GoldenBag", 5f));

        
    }

    public float GetMaxRate(){
        float max = ((ItemObject)(items[0])).rate;
        for(int i = 0; i < items.Count; i++){
            if(max < ((ItemObject)items[i]).rate) max = ((ItemObject)items[i]).rate;
        }
        return max;
    }
}