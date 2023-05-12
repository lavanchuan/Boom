using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    // GameObject player;
    public static readonly string TAG = "TouchCheck";

    private void Awake()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     switch (other.collider.tag)
    //     {
    //         case "UpDirect":
    //             player.GetComponent<Player>().direct = Player.GetUP();
    //             break;
    //         case "DownDirect":
    //             player.GetComponent<Player>().direct = Player.GetDOWN();
    //             break;
    //         case "LeftDirect":
    //             player.GetComponent<Player>().direct = Player.GetLEFT();
    //             break;
    //         case "RightDirect":
    //             player.GetComponent<Player>().direct = Player.GetRIGHT();
    //             break;
    //     }
    // }
}
