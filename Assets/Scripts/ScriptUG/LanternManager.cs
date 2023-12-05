using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public GameObject[] lanterns;  // 保存所有燈的陣列
    private int currentLanternIndex = 0;

    void Start()
    {
        // 在啟動時關閉所有燈
        TurnOffAllLanterns();
    }

    void Update()
    {
        // 在 Update 中檢測按鍵並開啟下一盞燈
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TurnOnLantern();
        // }
    }

    void TurnOffAllLanterns()
    {
        // 關閉所有燈
        foreach (GameObject lantern in lanterns)
        {
            lantern.SetActive(false);
        }
    }

    public void TurnOnLantern(string lanternName)
    {
        // 找到符合名字的燈籠
        GameObject lanternToTurnOn = null;
        foreach (GameObject lantern in lanterns)
        {
            if (lantern.name == lanternName)
            {
                lanternToTurnOn = lantern;
                break;
            }
        }

        if (lanternToTurnOn != null)
        {
            // 關閉當前燈
            // lanterns[currentLanternIndex].SetActive(false);
            //Debug.Log("關閉");

            // 更新當前燈籠索引
            currentLanternIndex = System.Array.IndexOf(lanterns, lanternToTurnOn);
            Debug.Log("更新");

            // 開啟指定的燈籠以及之前的所有燈籠
            for (int i = 0; i <= currentLanternIndex; i++)
            {
                Debug.Log("lanterns[i]");
                lanterns[i].SetActive(true);
            }
        }
        else
        {
            Debug.Log("燈籠名稱 " + lanternName + " 找不到。");
        }
    }
}
