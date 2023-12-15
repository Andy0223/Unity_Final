using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;

public class GameManagerGround : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 7f;
    public float initialMinutes = 2f; // 初始分鐘
    public float initialSeconds = 0f; // 初始秒數

    private int EnemyCounts;
    private int spawnEnemyCounts = 0;
    private float totalSeconds;
    private float timeRemaining;
    private int currentGameLevel;
    public bool isStop = false;
    public Text timerText;
    
    void Start()
    {
        totalSeconds = initialMinutes * 60 + initialSeconds; // 将初始时间转换为总秒数
        timeRemaining = totalSeconds;
        currentGameLevel = ShareValues.UGSceneEntryCounts;
        EnemyCounts += 10 * (currentGameLevel + 1);
        isStop = false;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShareValues.GameLevel += 1;
            ResumeGame();
        }
        if (ShareValues.GameLevel - currentGameLevel == 1)
        {
            //ShareValues.ancestor1_counts += 2;
            //ShareValues.ancestor2_counts += 2;
            //ShareValues.ancestor3_counts += 2;
            //ShareValues.ancestor4_counts += 2;
            //ShareValues.ancestor5_counts += 2;
            //ShareValues.ancestor6_counts += 2;
            EnemyCounts += 10;
            currentGameLevel = ShareValues.GameLevel;
            StartCoroutine(SpawnEnemies());
        }

        
        // 更新倒數計時器
        timeRemaining -= Time.deltaTime;

        // 確保時間不為負數
        timeRemaining = Mathf.Max(timeRemaining, 0f);

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
        ChangetoUnderGround();
    }

    void ChangetoUnderGround()
    {
        PauseGame();
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
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isStop = false;
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        //isStop = true;
        //StopCoroutine(SpawnEnemies());
        Debug.Log("GameOver");
        SceneManager.LoadSceneAsync(4);
    }

}
