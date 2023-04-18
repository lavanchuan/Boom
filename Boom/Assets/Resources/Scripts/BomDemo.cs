using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomDemo : MonoBehaviour
{
    int speed = 0;
    int speedMax = 10;
    Vector2 pos;
    GameDefine.DIRECT direct = GameDefine.DIRECT.NONE;

    private void Awake()
    {
        pos = transform.localPosition;
    }

    private void Update()
    {
        // Debug.Log(DateTime.Now + ": " + direct);
    }

    private void FixedUpdate()
    {
        if (direct != GameDefine.DIRECT.NONE)
        {
            switch (direct)
            {
                case GameDefine.DIRECT.UP:
                    pos.y = pos.y + speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.DOWN:
                    pos.y = pos.y - speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.LEFT:
                    pos.x = pos.x - speed * Time.deltaTime;
                    break;
                case GameDefine.DIRECT.RIGHT:
                    pos.x = pos.x + speed * Time.deltaTime;
                    break;
            }

            this.transform.localPosition = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        /// with player
        if (other.collider.tag == "Player")
        {
            if (speed == 0)
            {
                speed = speedMax;
            }
            else
            {
                speed = 0;
                direct = GameDefine.DIRECT.NONE;
            }

            UpdateDirect(other);
        }

        // with block, ...
        if (other.collider.tag == "Block")
        {
            speed = 0;
            direct = GameDefine.DIRECT.NONE;
        }
    }

    // void UpdateDirect()
    void UpdateDirect(Collision2D other)
    {
        if (speed > 0)
        {
            if (other.contacts[0].normal.x > 0)
            {
                direct = GameDefine.DIRECT.RIGHT;
            }
            else if (other.contacts[0].normal.x < 0)
            {
                direct = GameDefine.DIRECT.LEFT;
            }
            else if (other.contacts[0].normal.y > 0)
            {
                direct = GameDefine.DIRECT.UP;
            }
            else if (other.contacts[0].normal.y < 0)
            {
                direct = GameDefine.DIRECT.DOWN;
            }

        }
    }



}
