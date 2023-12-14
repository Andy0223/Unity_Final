using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
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
        Debug.Log(trolleyName);
        // gameManager.SetWin();

        if(trolleyName=="trolley1"){
            trolleyes[0].SetActive(false);
            ShareValues.trolley_1=false;
            gameManager.SetWin();
        }
        else if(trolleyName=="trolley2"){
            trolleyes[1].SetActive(false);
            ShareValues.trolley_2=false;
            gameManager.SetWin();
        }
        else if(trolleyName=="trolley3"){
            trolleyes[2].SetActive(false);
            ShareValues.trolley_3=false;
            gameManager.SetWin();
        }
    }
}
