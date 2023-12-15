using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Story1;
    [SerializeField] private GameObject Story2;
    [SerializeField] private GameObject Story3;
    public GameObject[] trolleyes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseTrolley(string trolleyName)
    {
        if(trolleyName=="trolley1"){
            trolleyes[0].SetActive(false);
            ShareValues.trolley_1=false;
            //增加祖先範例
            ShareValues.ancestor1_counts +=2;
            //故事1
            Story1.SetActive(true);
            //遊戲勝利
            gameManager.SetWin();
        }
        else if(trolleyName=="trolley2"){
            trolleyes[1].SetActive(false);
            ShareValues.trolley_2=false;
            //增加祖先範例
            ShareValues.ancestor1_counts +=2;
            //故事2
            Story2.SetActive(true);
            //遊戲勝利
            gameManager.SetWin();
        }
        else if(trolleyName=="trolley3"){
            trolleyes[2].SetActive(false);
            ShareValues.trolley_3=false;
            //增加祖先範例
            ShareValues.ancestor1_counts +=2;
            //故事3
            Story3.SetActive(true);
            //遊戲勝利
            gameManager.SetWin();
        }
    }
}
