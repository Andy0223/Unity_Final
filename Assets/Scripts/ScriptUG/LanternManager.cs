using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public GameObject[] openLanterns;
    public GameObject[] closeLanterns;
    private int currentLanternIndex = 0;

    void Start()
    {
        // 在啟動時關閉所有燈
        TurnOffAllLanterns();
    }

    void Update()
    {
        
    }

    void TurnOffAllLanterns()
    {
        // 關閉所有燈
        foreach (GameObject lantern in openLanterns)
        {
            lantern.SetActive(false);
        }
    }

    public void TurnOnLantern(string lanternName)
    {
        // 找到符合名字的燈籠
        GameObject lanternToTurnOn = null;
        foreach (GameObject lantern in openLanterns)
        {
            if (lantern.name == lanternName)
            {
                lanternToTurnOn = lantern;
                break;
            }
        }

        if (lanternToTurnOn != null)
        {
            // 獲取當前燈籠的位置
            Vector3 lanternPosition = lanternToTurnOn.transform.position;

            // 更新 shareValues.UGplayerPosition
            ShareValues.UGplayerPosition = lanternPosition;

            // 更新當前燈籠索引
            currentLanternIndex = System.Array.IndexOf(openLanterns, lanternToTurnOn);
            Debug.Log("更新");

            // 開啟指定的燈籠以及之前的所有燈籠
            for (int i = 0; i <= currentLanternIndex; i++)
            {
                Debug.Log("openLanterns[i]");
                openLanterns[i].SetActive(true);
                closeLanterns[i].SetActive(true);
            }
        }
        else
        {
            Debug.Log("燈籠名稱 " + lanternName + " 找不到。");
        }
    }
}
