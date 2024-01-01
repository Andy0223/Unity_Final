using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    private Text popupText;
    public float popupDuration = 3f; // 提示顯示時間

    // Start is called before the first frame update
    void Start()
    {
        popupText = GetComponent<Text>();
        HidePopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup()
    {
        popupText.enabled = true;
        Invoke("HidePopup", popupDuration);
    }

    private void HidePopup()
    {
        popupText.enabled = false;
    }
}
