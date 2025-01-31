using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Animator animator;
    private int collisionCount = 0;
    private Rigidbody2D rb;
    public float stopXPosition;
    private GameManagerGround gameManagerGround;

    void Start()
    {
        string enemyBaseName = "Enemy_base 1";
        GameObject selectedEnemyBase = GameObject.Find(enemyBaseName);
        stopXPosition = selectedEnemyBase.transform.position.x;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManagerGround = GameObject.Find("GameManager").GetComponent<GameManagerGround>();

    }

    void Update()
    {
        // 如果 Enemy 的 x 位置大於特定值，就停止向右的移動
        if (transform.position.x > stopXPosition)
        {
            // 停止施加的任何力或其他移動
            rb.velocity = Vector2.zero;
        }
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ancestor"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            animator.SetTrigger("hurt");
            HandleCollision();
        }

        if (collision.gameObject.CompareTag("Sign"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            gameManagerGround.GameOver();
        }

    }

    

    private void HandleCollision()
    {
        if (collisionCount == ShareValues.SingleEnemyCollisionCount)
        {
            Destroy(gameObject);
            gameManagerGround.EnemyDestroyCounts++;
            Debug.Log("EnemyDestroyCounts: " + gameManagerGround.EnemyDestroyCounts);
        }
        else
        {
            StartCoroutine(BounceAndResume());
            collisionCount++;
        }
    }

    private IEnumerator BounceAndResume()
    {
        
        // 停止当前运动
        rb.velocity = Vector2.zero;

        // 記錄拋物線彈開前的位置
        Vector2 initialPosition = transform.position;

        // 施加一个向上和向右的力，以模拟拋物線彈開
        rb.AddForce(new Vector2(5f, 3f), ForceMode2D.Impulse);

        // 等待一段時間，你可以根据需要调整这个时间
        yield return new WaitForSeconds(1f);

        // 等待直到 Enemy 回到原本的 y 位置
        while (transform.position.y > initialPosition.y)
        {
            // 透過每一幀逐漸降低 Y 位置，模擬掉落效果
            transform.position -= new Vector3(0f, 5f * Time.deltaTime, 0f);

            yield return null;
        }

    }

}
