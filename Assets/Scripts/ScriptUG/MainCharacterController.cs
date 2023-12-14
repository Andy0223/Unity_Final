using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollDistance;
    [SerializeField] private GameManager gameManager;

    private bool isGrounded=false;
    private bool isTrap=false;
    private Rigidbody2D rb;
    private Animator animator;
    public HealthController healthController; // 引用 HealthController
    public LanternManager lanternManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody2D not found on the character.");
        }
    }

    void Update()
    {
        // 跳
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded) && !(gameManager.isStop) )
        {
            Debug.LogWarning("Jumping!");
            // 使用 Rigidbody2D 的 velocity 來給予垂直速度
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetKey(KeyCode.D) && (!isTrap)&& !(gameManager.isStop) )
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false; 
            animator.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.A) && (!isTrap)&& !(gameManager.isStop) )
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true; 
            animator.SetBool("run", true);
        }
        // 翻滾迴避
        else if(Input.GetKeyDown(KeyCode.S)&& !(gameManager.isStop) ){
            Debug.LogWarning("Rollng!");
            // 使用 Rigidbody2D 的 velocity 來給予垂直速度
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
        }
        else{
            animator.SetBool("run", false);
        }

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

        // 檢查是否碰到Trap
        if (collision.gameObject.CompareTag("Trap"))
        {
            animator.SetTrigger("hurt");
            isTrap = true;
            // 將玩家向左和向上彈開
            rb.velocity = new Vector2(-3f, 3f);
            // 這裡減少 HealCurrent
            Debug.Log("HealCurrent1: " + HealthController.HealCurrent+"max"+HealthController.HealMax);
            HealthController.HealCurrent -= 10;
            Debug.Log("HealCurrent: " + HealthController.HealCurrent+"max"+HealthController.HealMax);
            // 啟動等待半秒的協程
            StartCoroutine(WaitForTrapReset());
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("Treasurebox"))
        {
            if (gameManager != null)
            {
                gameManager.SetWin();
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                Debug.LogError("GameManager is not assigned to MainCharacterController.");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lantern"))
        {
            // 獲取目前的燈籠名字
            string lanternName = other.gameObject.name;
            
            // 在燈籠名字後面加上 'on' 以及最末數字
            string newLanternName = "on" + GetLastDigit(lanternName);
            lanternManager.TurnOnLantern(newLanternName);
            
        }
    }
    // 取得字串的最末數字
    private string GetLastDigit(string input)
    {
        // 這個方法會將字串中的最末數字提取出來
        string result = string.Empty;
        for (int i = input.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(input[i]))
            {
                result = input[i] + result;
            }
            else
            {
                break;
            }
        }
        return result;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 離開地面
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    IEnumerator WaitForTrapReset()
    {
        // 等待半秒
        yield return new WaitForSeconds(0.5f);
        isTrap = false;
    }
}
