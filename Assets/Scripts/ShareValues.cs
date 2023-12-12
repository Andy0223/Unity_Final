using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareValues : MonoBehaviour
{
    // default ancestor counts for Level 0
    public static int ancestor1_counts = 2;
    public static int ancestor2_counts = 2;
    public static int ancestor3_counts = 2;
    public static int ancestor4_counts = 2;
    public static int ancestor5_counts = 2;
    public static int ancestor6_counts = 2;

    // Collision Counts for one enemy, execeed the counts than destroy
    public static int SingleEnemyCollisionCount = 3;
    // Total Enemy Counts for each Level
    public static int EachLevelEnemyTotalCount = 3;

    public static int boxOpenCounts = 0;
    public static int UGSceneEntryCounts = 0;

    public static int UGSceneTempPosition;

    // start from level 0
    public static int GameLevel = 0;
}
