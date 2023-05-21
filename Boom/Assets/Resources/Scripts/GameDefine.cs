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
    public static readonly int HORIZON_DIRECT = 1;
    public static readonly int VERTICAL_DIRECT = 2;
    public static readonly int FULL_DIRECT = 0;
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

    // TIME PICKUP ITEM OF LAST ROUND
    public static readonly float TIME_PICKUP_ITEM_OF_ROUND = 30f;
    public static readonly float TIME_PICKUP_ITEM_OF_ROUND_NORMAL = 5f;
    public static readonly float TIME_LOAD_SCENE = 5f;

    // BOSS MAP STRING
    public static readonly string TURTLE_BOSS_MAP_1 = "Scenes/TurtleBoss01";
    public static readonly string TURTLE_BOSS_MAP_2 = "Scenes/TurtleBoss02";
    public static readonly string TURTLE_BOSS_MAP_3 = "Scenes/TurtleBoss03";

    // PATH PREFABS ITEM
    public static readonly string PATH_PREFABS_ITEM_AUTO_USE = "Prefabs/ItemAutoUse/";
    public static readonly string PATH_PREFABS_ITEM_USE = "Prefabs/ItemUse/";
    public static readonly string PATH_PREFABS_COIN = "Prefabs/Coin/";
    
    // PATH SYSTEM DATA
    public static readonly string SYSTEM_FILE_NAME = "system.txt";
    public static readonly string ACCOUNT_PLAYER_FILE = "account.txt";
    public static readonly string LOGIN_STATE_FILE = "login_state.txt";
    public static readonly string PALYER_COIN_FILE = "player_coin.txt";

}
