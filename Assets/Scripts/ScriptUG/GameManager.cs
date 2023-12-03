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
    public bool IsGameOver;
    [SerializeField] private GameObject LossPop;
    [SerializeField] private GameObject WinPop;
    [SerializeField] private GameObject house_lantern_off;
    [SerializeField] private GameObject house_lantern_on;
    void Start(){
        IsGameOver = false;
        HealthController.HealCurrent=100;
    }

    void Update()
    {
        if (IsGameOver)
        {
            Time.timeScale = 0f;  // 暫停遊戲
        }
        else
        {
            Time.timeScale = 1f;  // 恢復時間流逝
            
        }
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
        //偵測血量
        if (HealthController.HealCurrent <= 0){
            SetGameOver();
        }
        //重新開始
        //按下R重新開始
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Ground");  
        }
    }

    public void SetGameOver(){
        //ex馬力歐一樣往下掉

        //遊戲結束
        IsGameOver = true;
        LossPop.SetActive(true);
        
    }
    public void SetWin(){
        //遊戲結束
        IsGameOver = true;
        WinPop.SetActive(true);
        
    }

    public void SetlanternOn(){
        house_lantern_off.SetActive(false);
        house_lantern_on.SetActive(true);
    }

    // 增加計時器的方法
    public void IncreaseTime(float amount)
    {
        timer += amount;
    }

}
