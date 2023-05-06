using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine : MonoBehaviour
{
    public enum DIRECT {LEFT, RIGHT, UP, DOWN, NONE};
    public static readonly int STAND = 0;
    public static readonly int DOWN = 1;
    public static readonly int UP = 2;
    public static readonly int RIGHT = 3;
    public static readonly int LEFT = 4;
    public static readonly float X_MIN = -10f; // -10
    public static readonly float X_MAX = 9f; // 9
    public static readonly float Y_MIN = -3; // -3
    public static readonly float Y_MAX = 3f; // 4

    // GAME STATE IN GAMEPLAY
    public enum GAMEPLAY_STATE {PLAYING, PAUSE, ENDGAME}
    public enum PLAYER_STATE {}
    
    // Block tiles
    public static readonly string TAG_BLOCK_NOT_BROKEN = "BlockNotBroken";
    public static readonly string TAG_BLOCK_MAY_BROKEN = "BlockMayBroken";
    public static readonly string TAG_BLOCK_LIMIT = "Limit";

}
