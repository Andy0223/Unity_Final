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
    // ch1_fail:4
    // ch1_win_first:5
    // ch1_win_two:6

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




}
