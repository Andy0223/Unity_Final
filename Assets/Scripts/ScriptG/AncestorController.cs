using System.Collections;
using UnityEngine;

public class AncestorController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float maxRightXPosition;
    public float maxLeftXPosition;
    private int collisionCount = 0;
    private int maxCollisionCount = 5;
    private Rigidbody2D rb;

    // 取得 SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        string enemyBaseName = "Enemy_base 1";
        GameObject selectedEnemyBase = GameObject.Find(enemyBaseName);
        maxRightXPosition = selectedEnemyBase.transform.position.x;

        // 取得 SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        if (transform.position.x == maxRightXPosition)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            HandleCollision();
        }

        if (collision.gameObject.CompareTag("Sign"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
            // 獲取另一個物體的 Collider 和 Rigidbody
            Collider2D otherCollider = collision.gameObject.GetComponent<Collider2D>();

            // 確認物體是否有 Collider 和 Rigidbody
            if (otherCollider != null)
            {
                // 將 Collider 和 Rigidbody 設為 disabled
                otherCollider.enabled = false;


                // 一段時間後，將 Collider 和 Rigidbody 再次設回 disabled
                StartCoroutine(DisableColliderAndRigidbody(otherCollider, 2f)); // 假設 2 秒後設回 disabled
            }
        }
    }

    // 協程：延遲一段時間後將 Collider 和 Rigidbody 設回 disabled
    private IEnumerator DisableColliderAndRigidbody(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 將 Collider 和 Rigidbody 設回 abled
        collider.enabled = true;
    }

    private void HandleCollision()
    {
        if (collisionCount == maxCollisionCount)
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
        rb.AddForce(new Vector2(-5f, 3f), ForceMode2D.Impulse);

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
