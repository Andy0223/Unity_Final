using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGround : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 7f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 隨機選擇一個 Enemy Base
            GameObject selectedEnemyBase = GetRandomEnemyBase();

            // 隨機選擇一個敵人的預製體
            GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();

            // 在該 Ground 上生成敵人
            SpawnEnemy(selectedEnemyBase, selectedEnemyPrefab);

            // 等待七秒再生成下一個敵人
            yield return new WaitForSeconds(spawnInterval);
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

    }
}
