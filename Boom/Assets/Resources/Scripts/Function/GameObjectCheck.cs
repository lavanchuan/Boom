using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameObjectCheck : MonoBehaviour
{
    public static bool CheckExplosiveArea(GameObject objCheck){
        // check up direct
        // foreach(GameObject obj in Camera.main.GetComponent<GameManager>().listBlock){
        //     if(obj != null){
        //         Rectangle rect1 = new Rectangle((int)obj.transform.position.x,
        //             (int)obj.transform.position.y, 
        //             (int)obj.GetComponent<BoxCollider2D>().size.x,
        //             (int)obj.GetComponent<BoxCollider2D>().size.y);
        //         Rectangle rect2 = new Rectangle((int)objCheck.transform.position.x,
        //             (int)objCheck.transform.position.y,
        //             (int)objCheck.GetComponent<BoxCollider2D>().size.x,
        //             (int)objCheck.GetComponent<BoxCollider2D>().size.y);
        //         if(rect1.IntersectsWith(rect2)) return false;
        //     }
        // }
        return true;


    }

    
}
