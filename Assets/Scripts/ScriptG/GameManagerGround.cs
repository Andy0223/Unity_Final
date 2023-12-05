using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class GameManagerGround : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 7f;
    public float initialMinutes = 2f; // 初始分钟数
    public float initialSeconds = 0f; // 初始秒数

    private int EnemyCounts = 2;
    private int spawnEnemyCounts = 0;
    private float totalSeconds;
    private float timeRemaining;
    private int currentGmaeLevel;
    public bool isStop = false;
    public Text timerText;
    
    void Start()
    {
        totalSeconds = initialMinutes * 60 + initialSeconds; // 将初始时间转换为总秒数
        timeRemaining = totalSeconds;
        currentGmaeLevel = ShareValues.GameLevel;
        StartCoroutine(SpawnEnemies());
        isStop = false;

    }

    void Update()
    {
        if (ShareValues.GameLevel - currentGmaeLevel == 1)
        {
            EnemyCounts += 10;
            currentGmaeLevel = ShareValues.GameLevel;
        }
        // 更新倒數計時器
        timeRemaining -= Time.deltaTime;

        // 如果時間到，重設計時器
        if (timeRemaining <= 0f)
        {
            Debug.Log("Game Over");
            PauseGame();
            // 不再重設計時器，等待玩家决定是否进入下一关
            // timeRemaining = spawnInterval;
        }
        
        if (isStop)
        {
            Time.timeScale = 0f;  // 暫停遊戲
        }
        else
        {
            Time.timeScale = 1f;  // 恢復時間流逝
        }

        // 更新UI上的计时器显示
        UpdateTimerUI();
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnEnemyCounts < EnemyCounts)
        {
            GameObject selectedEnemyBase = GetRandomEnemyBase();
            GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();
            SpawnEnemy(selectedEnemyBase, selectedEnemyPrefab);

            spawnEnemyCounts++;
            yield return new WaitForSeconds(spawnInterval);
        }
        // 敌人生成达到上限，暂停游戏并询问是否进入下一关
        AskForNextLevel();
    }

    void AskForNextLevel()
    {
        // 显示询问进入下一关的 UI 或执行其他逻辑
        // 这里仅作为示例，你可能需要在你的游戏中使用自己的 UI 界面和逻辑
        Debug.Log("Do you want to go to the next level?");
        // 在这里你可以显示一个 UI，让玩家选择是否进入下一关
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

    void UpdateTimerUI()
    {
        // 将时间格式化为分和秒，并更新UI文本
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseGame()
    {
        StopCoroutine(SpawnEnemies());
        isStop = true;
        // Time.timeScale = 0f;  // 设置时间流逝速度为0，即暂停
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isStop = false;
        // Time.timeScale = 1f;  // 恢复正常的时间流逝速度
        Debug.Log("Game Resumed");
    }

}
