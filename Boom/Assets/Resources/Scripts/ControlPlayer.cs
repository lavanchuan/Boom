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

    // constructor
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TouchCheck")
            switch (tag)
            {
                case TAG_LEFT_DIRECT:
                    SetDirectPlayer(GameDefine.DIRECT.LEFT);
                    break;
                case TAG_RIGHT_DIRECT:
                    SetDirectPlayer(GameDefine.DIRECT.RIGHT);
                    break;
                case TAG_UP_DIRECT:
                    SetDirectPlayer(GameDefine.DIRECT.UP);
                    break;
                case TAG_DOWN_DIRECT:
                    SetDirectPlayer(GameDefine.DIRECT.DOWN);
                    break;
                case TAG_STAND_DIRECT:
                    SetDirectPlayer(GameDefine.DIRECT.NONE);
                    break;
            }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }

    void SetDirectPlayer(GameDefine.DIRECT direct)
    {
        try{
            player.GetComponent<Player>().SetDirect(direct);
        } catch(Exception e){}
    }


}
