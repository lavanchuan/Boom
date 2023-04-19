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
    public static readonly float X_MIN = -11f; // -10
    public static readonly float X_MAX = 10f; // 9
    public static readonly float Y_MIN = -5; // -5
    public static readonly float Y_MAX = 5f; // 4

}
