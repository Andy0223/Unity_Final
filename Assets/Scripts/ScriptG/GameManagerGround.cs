using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPro = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerGround : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;
    public float initialMinutes = 2f; // 初始分鐘
    public float initialSeconds = 0f; // 初始秒數
    public int EnemyDestroyCounts = 0;
    public TextPro LevelText;
    public TextPro TotalEnemyText;
    public GameObject teachingBox;
    public GameObject background;
    public Text ancestor1_addon;
    public Text ancestor2_addon;
    public Text ancestor3_addon;
    public Text ancestor4_addon;
    public Text ancestor5_addon;
    public Text ancestor6_addon;
    public bool isStop = false;

    private int spawnEnemyCounts = 0;
    private int currentGameLevel;
    private bool isTreasure1Found = false;
    private bool isTreasure2Found = false;
    private bool isTreasure3Found = false;

    void Start()
    {
        ancestor1_addon.enabled = false;
        ancestor2_addon.enabled = false;
        ancestor3_addon.enabled = false;
        ancestor4_addon.enabled = false;
        ancestor5_addon.enabled = false;
        ancestor6_addon.enabled = false;
        currentGameLevel = ShareValues.UGSceneEntryCounts;
        Debug.Log("currentGameLevel: " + currentGameLevel);
        ShareValues.EnemyCounts = 5 * (currentGameLevel + 1);
        spawnInterval = ShareValues.spawnInterval - currentGameLevel;
        LevelText.text = "Level: " + currentGameLevel;
        TotalEnemyText.text = "Total Enemy: " + ShareValues.EnemyCounts;
        isStop = false;
        CheckAndShowTreasure(ShareValues.treasure_1, isTreasure1Found, ancestor1_addon, ancestor2_addon, ancestor3_addon, ancestor4_addon);
        CheckAndShowTreasure(ShareValues.treasure_2, isTreasure2Found, ancestor3_addon, ancestor4_addon, ancestor5_addon, ancestor6_addon);
        CheckAndShowTreasure(ShareValues.treasure_3, isTreasure3Found, ancestor5_addon, ancestor6_addon, ancestor1_addon, ancestor2_addon);
        StartCoroutine(SpawnEnemies());
        if (currentGameLevel != 0)
        {
            teachingBox.SetActive(false);
            background.SetActive(false);
        }
        else
        {
            PauseGame();
        }
    }

    void Update()
    {
        TotalEnemyText.text = "Total Enemy: " + (ShareValues.EnemyCounts- EnemyDestroyCounts);

        if (isStop)
        {
            Time.timeScale = 0f;  // 暫停遊戲
        }
        else
        {
            Time.timeScale = 1f;  // 恢復時間流逝
        }


        if (EnemyDestroyCounts == ShareValues.EnemyCounts)
        {
            // 判斷是不是最後一關 Scene8
            if (ShareValues.treasure_1 == false && ShareValues.treasure_2 == false && ShareValues.treasure_3 == false)
            {
                SceneManager.LoadSceneAsync(8);
            }
            ChangetoUnderGround();
        }
    }

    void CheckAndShowTreasure(bool treasureFound, bool isFoundFlag, Text text1, Text text2, Text text3, Text text4)
    {
        if (treasureFound && !isFoundFlag)
        {
            Debug.LogWarning("Treasure found");

            text1.enabled = true;
            text2.enabled = true;
            text3.enabled = true;
            text4.enabled = true;

            StartCoroutine(DisableTextAfterDelay(text1, text2, text3, text4));
            isFoundFlag = true; // 設置為已找到
        }
    }

    IEnumerator DisableTextAfterDelay(Text text1, Text text2, Text text3, Text text4)
    {
        yield return new WaitForSeconds(10f);

        // 在這裡等待三秒後設置 Text 為 false
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnEnemyCounts < ShareValues.EnemyCounts)
        {
            GameObject selectedEnemyBase = GetRandomEnemyBase();
            GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();
            SpawnEnemy(selectedEnemyBase, selectedEnemyPrefab);

            spawnEnemyCounts++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void ChangetoUnderGround()
    {
        if(ShareValues.UGSceneEntryCounts == 0){
            SceneManager.LoadSceneAsync(5);
        }
        else{
            SceneManager.LoadSceneAsync(6);
        }
    }

    GameObject GetRandomEnemyBase()
    {
        int randomIndex = Random.Range(1, 4); // 生成 1, 2, 或 3
        string enemyBaseName = "Enemy_base " + randomIndex;
        GameObject selectedEnemyBase = GameObject.Find(enemyBaseName);
        Debug.Log(selectedEnemyBase);

        return selectedEnemyBase;
    }

    GameObject GetRandomEnemyPrefab()
    {
        // 隨機選擇一個敵人的預製體
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    void SpawnEnemy(GameObject selectedEnemyBase, GameObject enemyPrefab)
    {
        Vector3 spawnPosition = selectedEnemyBase.transform.position;

        // 實例化敵人
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        Debug.Log("Enemy Spawned!");

    }

    public void PauseGame()
    {
        StopCoroutine(SpawnEnemies());
        isStop = true;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isStop = false;
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        isStop = true;
        StopCoroutine(SpawnEnemies());
        Debug.Log("GameOver");
        ShareValues.ResetValues();
        SceneManager.LoadSceneAsync(4);
    }

}
