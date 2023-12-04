using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Animator animator;
    private int collisionCount = 0;
    public int maxCollisionCount = 3;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        if (collisionCount >= maxCollisionCount)
        {
            Destroy(gameObject);
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
        rb.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse);

        // 等待一段時間，你可以根据需要调整这个时间
        yield return new WaitForSeconds(1f);

        // 等待直到 Enemy 回到原本的 y 位置
        while (transform.position.y > initialPosition.y)
        {
            // 透過每一幀逐漸降低 Y 位置，模擬掉落效果
            transform.position -= new Vector3(0f, 5f * Time.deltaTime, 0f);

            yield return null;
        }

        // 恢复正常运动
        rb.velocity = new Vector2(movementSpeed, 0f);
    }

}
