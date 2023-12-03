using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSprite : MonoBehaviour
{
    public string selectedAncestorName;
    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SelectAncestor(SpriteRenderer selectedAncestor, List<SpriteRenderer> spriteRenderers, List<Text> ancestors_remainCounts)
    {
        // 將所有9-sliced sprite的顏色設置為白色（或其他顏色）
        ClearAllColors(spriteRenderers);
        ClearAllRemainText(ancestors_remainCounts);

        // 將選擇的9-sliced sprite的顏色設置為R:95, G:95, B:95
        selectedAncestor.color = new Color(95 / 255f, 95 / 255f, 95 / 255f);
        

        // 取得選擇的9-sliced sprite的子GameObject的名稱
        if (selectedAncestor.transform.childCount > 0)
        {
            Transform firstChild = selectedAncestor.transform.GetChild(0);
            SpriteRenderer childRenderer = firstChild.GetComponent<SpriteRenderer>();
            selectedAncestorName = firstChild.name;
            childRenderer.color = new Color(95 / 255f, 95 / 255f, 95 / 255f);
            foreach (var ancestor_remainCounts in ancestors_remainCounts)
            {
                if (ancestor_remainCounts != null && ancestor_remainCounts.name.Contains(selectedAncestorName))
                {
                    ancestor_remainCounts.enabled = true;
                }
            }
        }
        else
        {
            selectedAncestorName = "No Child";
        }
    }

    // 接受 SpriteRenderer 列表並清除它們的顏色
    public void ClearAllColors(List<SpriteRenderer> spriteRenderers)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.white;

            // 如果存在子物件
            if (spriteRenderer.transform.childCount > 0)
            {
                Transform firstChild = spriteRenderer.transform.GetChild(0);
                SpriteRenderer childRenderer = firstChild.GetComponent<SpriteRenderer>();
                childRenderer.color = Color.white;
            }
        }
    }


    public void ClearAllRemainText(List<Text> ancestors_remainCounts)
    {
        foreach (var ancestor_remainCounts in ancestors_remainCounts)
        {
            ancestor_remainCounts.enabled = false;
        }
    }
}
