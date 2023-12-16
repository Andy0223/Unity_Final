using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;

public class GameManagerGround : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;
    public float initialMinutes = 2f; // 初始分鐘
    public float initialSeconds = 0f; // 初始秒數
    public int EnemyDestroyCounts = 0;
    public Text LevelText;
    public Text TotalEnemyText;

    private int spawnEnemyCounts = 0;
    private int currentGameLevel;
    public bool isStop = false;
    
    void Start()
    {
        currentGameLevel = ShareValues.UGSceneEntryCounts;
        Debug.Log("currentGameLevel: " + currentGameLevel);
        ShareValues.EnemyCounts = 5 * (currentGameLevel + 1);
        spawnInterval = ShareValues.spawnInterval - currentGameLevel;
        LevelText.text = "Level: " + currentGameLevel;
        TotalEnemyText.text = "Total Enemy: " + ShareValues.EnemyCounts;
        isStop = false;
        StartCoroutine(SpawnEnemies());
        PauseGame();
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
