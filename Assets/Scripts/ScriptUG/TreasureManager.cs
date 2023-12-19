using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject backGround;
    public GameObject[] treasures;
    public GameObject[] Storys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseTreasure(string treasureName)
    {
        if(treasureName=="treasure1"){
            treasures[0].SetActive(false);
            ShareValues.treasure_1=false;
            //增加祖先範例
            ShareValues.ancestor1_counts += 2;
            ShareValues.ancestor2_counts += 2;
            ShareValues.ancestor3_counts += 2;
            ShareValues.ancestor4_counts += 2;
            //故事1
            Storys[0].SetActive(true);
            button.SetActive(true);
            backGround.SetActive(true);
            //遊戲勝利
            gameManager.isGameOver = true;
        }
        else if(treasureName=="treasure2"){
            treasures[1].SetActive(false);
            ShareValues.treasure_2=false;
            //增加祖先範例
            ShareValues.ancestor3_counts += 2;
            ShareValues.ancestor4_counts += 2;
            ShareValues.ancestor5_counts += 2;
            ShareValues.ancestor6_counts += 2;
            //故事2
            Storys[1].SetActive(true);
            button.SetActive(true);
            backGround.SetActive(true);
            //遊戲勝利
            gameManager.isGameOver = true;
        }
        else if(treasureName=="treasure3"){
            treasures[2].SetActive(false);
            ShareValues.treasure_3=false;
            //增加祖先範例
            ShareValues.ancestor5_counts +=2;
            ShareValues.ancestor6_counts += 2;
            ShareValues.ancestor1_counts += 2;
            ShareValues.ancestor2_counts += 2;
            //故事3
            Storys[2].SetActive(true);
            button.SetActive(true);
            backGround.SetActive(true);
            //遊戲勝利
            gameManager.isGameOver = true;
        }
    }
}
