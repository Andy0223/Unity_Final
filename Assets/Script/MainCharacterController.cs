using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollDistance;

    private bool isGrounded=false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D not found on the character.");
        }
        
    }

    void Update()
    {
        // 蹬牆跳
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded))
        {
            Debug.LogWarning("Jumping!");
            // 使用 Rigidbody2D 的 velocity 來給予垂直速度
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        // 移動
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(moveDirection);


        // 翻滾迴避
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Roll();
        // }
    }

    void Roll()
    {
        // 在這裡實現翻滾的邏輯
        // 這可以是一個短暫的動畫或移動的行為
        // 例如：transform.Translate(Vector3.forward * rollDistance);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查是否在地面上
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        
        if (collision.gameObject.CompareTag("Trap"))
        {
            // 將玩家向左和向上彈開
            rb.velocity = new Vector2(-3f, 3f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 離開地面
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
