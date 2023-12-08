using System.Collections;
using UnityEngine;

public class AncestorController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float maxRightXPosition;
    public float maxLeftXPosition;
    private int collisionCount = 0;
    public int maxCollisionCount = 6;
    private Rigidbody2D rb;

    // 表示當前移動方向的列舉
    private enum MovementDirection
    {
        Right,
        Left
    }

    // 當前移動方向
    private MovementDirection currentDirection = MovementDirection.Right;

    // 取得 SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        string enemyBaseName = "Enemy_base 1";
        GameObject selectedEnemyBase = GameObject.Find(enemyBaseName);
        maxRightXPosition = selectedEnemyBase.transform.position.x;

        string playerBaseName = "dungeon_door_opened";
        GameObject selectedHomeBase = GameObject.Find(playerBaseName);
        maxLeftXPosition = selectedHomeBase.transform.position.x;

        // 取得 SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(spriteRenderer.flipX);

        // 根據當前移動方向進行移動
        switch (currentDirection)
        {
            case MovementDirection.Right:
                // 達到 maxRightXPosition 時，切換方向為左並開始向左移動
                if (transform.position.x > maxRightXPosition)
                {
                    currentDirection = MovementDirection.Left;
                    // 在切換方向時，將 SpriteRenderer 的 flipX 設為 true，即反轉
                    spriteRenderer.flipX = true;
                }
                // 在 Ancestor 未超過指定 x 位置時，才進行向右移動
                else if (spriteRenderer.flipX == false)
                {
                    transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                }
                break;

            case MovementDirection.Left:
                // 達到 maxLeftXPosition 時，切換方向為右並開始向右移動
                if (transform.position.x < maxLeftXPosition)
                {
                    currentDirection = MovementDirection.Right;
                    // 在切換方向時，將 SpriteRenderer 的 flipX 設為 false，即還原
                    spriteRenderer.flipX = false;
                }
                // 在 Ancestor 未超過指定 x 位置時，才進行向左移動
                else if (spriteRenderer.flipX == true)
                {
                    transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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
        rb.AddForce(new Vector2(-7f, 5f), ForceMode2D.Impulse);

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
