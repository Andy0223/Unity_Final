using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareValues : MonoBehaviour
{
    public static int EnemyCounts;
    public static float spawnInterval = 7f;
    // default ancestor counts for Level 0
    public static int ancestor1_counts = 2;
    public static int ancestor2_counts = 2;
    public static int ancestor3_counts = 2;
    public static int ancestor4_counts = 2;
    public static int ancestor5_counts = 2;
    public static int ancestor6_counts = 2;

    // Collision Counts for one enemy, execeed the counts than destroy
    public static int SingleEnemyCollisionCount = 2;
    // Total Enemy Counts for each Level
    public static int EachLevelEnemyTotalCount = 3;

    public static int boxOpenCounts = 0;
    public static int UGSceneEntryCounts = 0;

    public static int UGSceneTempPosition;

    // start from level 0
    public static int GameLevel = 0;

    //Ground各個layer有沒有祖先
    public static List<bool> LayerDetect = new List<bool>() { false, false, false };

    //UG_Trolly
    public static bool treasure_1=true;
    public static bool treasure_2=true;
    public static bool treasure_3=true;
    //UG_PlayerPosition
    // 儲存玩家初始位置的變數
    public static Vector3 UGplayerPosition = new Vector3(0, 2.4f, 0);
    
    //重整數值
    public static void ResetValues()
    {
        ancestor1_counts = 2;
        ancestor2_counts = 2;
        ancestor3_counts = 2;
        ancestor4_counts = 2;
        ancestor5_counts = 2;
        ancestor6_counts = 2;

        spawnInterval = 7f;

        SingleEnemyCollisionCount = 2;
        EachLevelEnemyTotalCount = 3;

        boxOpenCounts = 0;
        UGSceneEntryCounts = 0;

        UGSceneTempPosition = 0;

        GameLevel = 0;

        treasure_1 = true;
        treasure_2 = true;
        treasure_3 = true;

        UGplayerPosition = new Vector3(0, -2, 0);
    }

}
