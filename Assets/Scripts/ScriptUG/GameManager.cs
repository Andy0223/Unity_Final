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
    public Text healthText;
    private float timer = 0f;
    public bool isGameOver = false;
    public bool isStop = false;
    [SerializeField] private GameObject Story1;
    [SerializeField] private GameObject Story2;
    [SerializeField] private GameObject Story3;
    [SerializeField] private GameObject house_lantern_off;
    [SerializeField] private GameObject house_lantern_on;
    [SerializeField] private float timeForHealth;
    public HealthController healthController;
    public GameObject[] treasures;

    void Start(){
        HealthController.HealCurrent=100;
        ShareValues.UGSceneEntryCounts+=1;
        if(ShareValues.treasure_1==false){
            treasures[0].SetActive(false);
        }
        if(ShareValues.treasure_2==false){
            treasures[1].SetActive(false);
        }
        if(ShareValues.treasure_3==false){
            treasures[2].SetActive(false);
        }
        
    }

    void Update()
    {
        if (isGameOver || isStop)
        {
            Time.timeScale = 0f;  // 暫停遊戲
        }
        else
        {
            Time.timeScale = 1f;  // 恢復時間流逝   
        }


        // 更新計時器
        timer += Time.deltaTime;
        HealthController.HealCurrent -= Time.deltaTime/timeForHealth;

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
            GameOver();
        }

    }
    
    //遊戲結束
    public void GameOver(){
        isGameOver = true;
        
        //全都沒打開:unfind(因為時間結束或機關觸發死的)
        if(ShareValues.treasure_1 && ShareValues.treasure_2 && ShareValues.treasure_3){
            SceneManager.LoadSceneAsync(8);
        }
    }
    //碰到寶箱後，觸發故事卡並按下button後
    public void ButtonClick(){
        //全都打開:All
        if(!ShareValues.treasure_1 && !ShareValues.treasure_2 && !ShareValues.treasure_3){
            SceneManager.LoadSceneAsync(10);
        }
        //打開一個:1
        else{
            SceneManager.LoadSceneAsync(9);
        }
    }
    public void SetlanternOn(){
        house_lantern_off.SetActive(false);
        house_lantern_on.SetActive(true);
    }
    public void PauseGame()
    {
        isStop = true;
        Debug.Log("Game Paused");
    }
    public void ResumeGame()
    {
        isStop = false;
        Debug.Log("Game Resumed");
    }
}
