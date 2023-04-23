using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ArrayList listBlock;
    public ArrayList listBlockRectangle;
    public static bool running;
    public static ArrayList itemsMayDrop;// string: name prefabs of item
    private void Awake() {
        listBlock = new ArrayList();
        listBlockRectangle = new ArrayList();
        running = true;
        itemsMayDrop = new ArrayList();
        SetupItemsMayDrop();
    }
    // Setup items may drop
    void SetupItemsMayDrop(){
        // Item auto use
        itemsMayDrop.Add("ItemAutoUse/BinhNuoc");
        itemsMayDrop.Add("ItemAutoUse/BongGai");
        itemsMayDrop.Add("ItemAutoUse/GiayDo");
        itemsMayDrop.Add("ItemAutoUse/GiayVang");
        itemsMayDrop.Add("ItemAutoUse/GiayXanh");
        itemsMayDrop.Add("ItemAutoUse/GreenDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/RedDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/VioletDemoniacMask");
        itemsMayDrop.Add("ItemAutoUse/VisibleOvercoat");
        itemsMayDrop.Add("ItemAutoUse/SuperShield");

        // item use
        itemsMayDrop.Add("ItemUse/Kim");
        itemsMayDrop.Add("ItemUse/Radar");
        itemsMayDrop.Add("ItemUse/Shield");
        itemsMayDrop.Add("ItemUse/TimeBomb");
    }
}
