using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public const string TAG_LEFT_DIRECT = "LeftDirect";
    public const string TAG_RIGHT_DIRECT = "RightDirect";
    public const string TAG_UP_DIRECT = "UpDirect";
    public const string TAG_DOWN_DIRECT = "DownDirect";
    public const string TAG_STAND_DIRECT = "StandDirect";

    // PLAYER
    GameObject player;
    public bool leftPressed = false;
    public bool rightPressed = false;
    public bool upPressed = false;
    public bool downPressed = false;

    // constructor
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "TouchCheck"){
        //     switch (tag)
        //     {
        //         case TAG_LEFT_DIRECT:
        //             SetDirectPlayer(GameDefine.DIRECT.LEFT);
        //             break;
        //         case TAG_RIGHT_DIRECT:
        //             SetDirectPlayer(GameDefine.DIRECT.RIGHT);
        //             break;
        //         case TAG_UP_DIRECT:
        //             SetDirectPlayer(GameDefine.DIRECT.UP);
        //             break;
        //         case TAG_DOWN_DIRECT:
        //             SetDirectPlayer(GameDefine.DIRECT.DOWN);
        //             break;
        //         case TAG_STAND_DIRECT:
        //             SetDirectPlayer(GameDefine.DIRECT.NONE);
        //             break;
        //     }
        // }

        if(other.tag == "TouchCheck"){
            if(tag == TAG_LEFT_DIRECT) leftPressed = true;
            if(tag == TAG_RIGHT_DIRECT) rightPressed = true;
            if(tag == TAG_UP_DIRECT) upPressed = true;
            if(tag == TAG_DOWN_DIRECT) downPressed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other) {
       if(other.tag == "TouchCheck"){
            if(tag == TAG_LEFT_DIRECT) leftPressed = false;
            if(tag == TAG_RIGHT_DIRECT) rightPressed = false;
            if(tag == TAG_UP_DIRECT) upPressed = false;
            if(tag == TAG_DOWN_DIRECT) downPressed = false;
        }
    }

}
