using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    // Start:0
    // Ch1:1
    // Ground:2
    // UnderGround:3
    // ch1_fail:4 遊戲失敗
    // ch1_win_first:5 第一次成功
    // ch1_win_two:6 第二次成功
    // over:7 最後一戰
    // unfind_treasure:8 沒有找到寶箱
    // find_treasure:9 找到寶箱
    // find_treasure3:10 找到三個寶箱
    // End:11 遊戲結束畫面
    public void Menu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void StartGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void StartGround(){
        SceneManager.LoadSceneAsync(2);
    }

    public void StartUnderGround(){
        SceneManager.LoadSceneAsync(3);
    }

    public void OverGame(){
        SceneManager.LoadSceneAsync(7);
    }

    public void EndGame()
    {
        SceneManager.LoadSceneAsync(11);
    }


}
