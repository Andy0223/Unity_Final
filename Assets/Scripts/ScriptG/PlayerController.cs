using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float teleportDistance = 2f; // 瞬間移動的距離
    private Rigidbody2D rb;
    public GameObject baseground; // 用於存儲地面物體的參考
    private SelectSprite selectSprite;
    public SpriteRenderer ancestor1;
    public SpriteRenderer ancestor2;
    public SpriteRenderer ancestor3;
    public SpriteRenderer ancestor4;
    public SpriteRenderer ancestor5;
    public SpriteRenderer ancestor6;
    private List<SpriteRenderer> spriteRenderers;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 添加 SelectSprite 腳本並設置參考
        selectSprite = gameObject.AddComponent<SelectSprite>();
        spriteRenderers = new List<SpriteRenderer> { ancestor1, ancestor2, ancestor3, ancestor4, ancestor5, ancestor6 };
        animator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        HandleMovementInput();
        HandleSelectionInput();
        HandleSpawnInput();
    }

    void HandleMovementInput()
    {
/*
        float dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
        // 瞬間移動到上一層或下一層地面
        if (Input.GetKeyDown(KeyCode.W))
        {
            TeleportToLayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TeleportToLayer(-1);
        }

        // 水平移動
        if (dirx != 0)
        {
            animator.SetBool("run", true);
            //animator.SetInteger("state", 1);
        }
        else
        {
            animator.SetBool("run", false);
            //animator.SetInteger("state", 0);
        }
*/
        // 右移
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false; 
            animator.SetBool("run", true);
        }
        // 左移
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true; 
            animator.SetBool("run", true);
        }
        // 瞬間移動到上一層或下一層地面
        else if(Input.GetKeyDown(KeyCode.W)){
            TeleportToLayer(1);
        }
        else if(Input.GetKey(KeyCode.S)){
            TeleportToLayer(-1);
        }
        else{
            animator.SetBool("run", false);
        }
        
    }
/*
    void MoveHorizontally(float speed)
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // 左右移動
        float horizontalMovement = horizontalInput * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector2(horizontalMovement, 0f));
    }
*/
    void HandleSelectionInput()
    {
        // 根據按下的數字鍵選擇對應的9-sliced sprite
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectSprite.SelectAncestor(ancestor1, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectSprite.SelectAncestor(ancestor2, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectSprite.SelectAncestor(ancestor3, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectSprite.SelectAncestor(ancestor4, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectSprite.SelectAncestor(ancestor5, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectSprite.SelectAncestor(ancestor6, spriteRenderers);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectSprite.ClearAllColors(spriteRenderers);
        }
    }

    void HandleSpawnInput()
    {
        // 按下空白鍵生成Prefab
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        // 檢查Prefab名字是否存在
        if (!string.IsNullOrEmpty(selectSprite.selectedAncestorName))
        {
            // 根據Prefab名字生成Prefab
            GameObject prefab = Resources.Load<GameObject>(selectSprite.selectedAncestorName);
            if (prefab != null)
            {
                // 根據現在的位置生成Prefab，你可以根據需要更改位置
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("找不到Prefab：" + selectSprite.selectedAncestorName);
            }
        }
        else
        {
            Debug.LogError("Prefab名字未設置。請在腳本的Inspector中設置Prefab名字。");
        }
    }

    void TeleportToLayer(int layerOffset)
    {
        // 取得當前玩家的位置
        Vector2 currentPosition = rb.position;

        // 計算目標層的名稱
        string targetLayerName = "Ground " + (GetCurrentLayer() + layerOffset);
        Debug.Log(targetLayerName);

        // 使用目標層的名稱尋找對應的地面物件
        GameObject targetGround = GameObject.Find(targetLayerName);

        // 如果找到目標地面，進行瞬間移動
        if (targetGround != null)
        {
            // 計算目標位置，將玩家放置在目標地面上方
            Vector2 targetPosition = new Vector2(currentPosition.x, targetGround.transform.position.y + teleportDistance);

            // 更新玩家位置
            rb.position = targetPosition;

            // 更新 ground 參考
            baseground = targetGround;
        }
        else
        {
            Debug.LogError("找不到目標地面：" + targetLayerName);
        }
    }

    int GetCurrentLayer()
    {
        // 這個示例假設地面物體有命名為 "Ground1"、"Ground2"、"Ground3" 等等
        // 這樣可以通過解析物體名稱來獲取當前所在的地面層級

        if (baseground != null)
        {
            string[] nameParts = baseground.name.Split(' ');
            Debug.Log("1");
            Debug.Log(nameParts);
            if (nameParts.Length >= 2 && int.TryParse(nameParts[1], out int layerNumber))
            {
                Debug.Log(layerNumber);
                return layerNumber;
            }
        }

        return 0; // 如果解析失敗，預設在第一層地面
    }
}
