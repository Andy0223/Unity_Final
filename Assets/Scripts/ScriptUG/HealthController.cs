using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Text = TMPro.TextMeshProUGUI;

public class HealthController : MonoBehaviour
{
    public static double HealCurrent=100;
    public static double HealMax=100;
    private Image healthBar;
    
    // public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar Image component not found on the object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)HealCurrent / (float)HealMax;
            // healthText.text = HealCurrent.ToString() + "/" + HealMax.ToString();
        }
        else
        {
            Debug.LogError("healthBar is null in the Update method.");
        }
    }

}
