using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // 單例實例
    private static GameManager _instance;

    // 取得單例實例的方法
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 如果實例不存在，則在場景中尋找 GameManager 並設置實例
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public Text timerText;
    private float timer = 0f;

    void Update()
    {
        // 更新計時器
        timer += Time.deltaTime;

        // 將時間格式化為分:秒
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        // 更新 UI Text
        if (timerText != null)
        {
            timerText.text = "Time: " + minutes + ":" + seconds;
        }

        // 偵測 HealCurrent 是否為 0
        if (HealthController.HealCurrent <= 0)
        {
            // 停止遊戲
            Time.timeScale = 0f;

            // 其他遊戲邏輯ex:介面

            // 按下 R 键重新開始
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // 重設定遊戲時間
                HealthController.HealCurrent=100;
                Time.timeScale = 1f;
            }
        }
    }

    // 增加計時器的方法
    public void IncreaseTime(float amount)
    {
        timer += amount;
    }

}
