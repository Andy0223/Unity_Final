using System.Collections.Generic;
using UnityEngine;

public class SelectSprite : MonoBehaviour
{
    public string selectedAncestorName;
    
    void Update()
    {
        
    }

    public void SelectAncestor(SpriteRenderer selectedAncestor, List<SpriteRenderer> spriteRenderers)
    {
        // 將所有9-sliced sprite的顏色設置為白色（或其他顏色）
        ClearAllColors(spriteRenderers);

        // 將選擇的9-sliced sprite的顏色設置為R:128, G:128, B:128
        selectedAncestor.color = new Color(128 / 255f, 128 / 255f, 128 / 255f);

        // 取得選擇的9-sliced sprite的子GameObject的名稱
        if (selectedAncestor.transform.childCount > 0)
        {
            Transform firstChild = selectedAncestor.transform.GetChild(0);
            selectedAncestorName = firstChild.name;
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
        }
    }
}
