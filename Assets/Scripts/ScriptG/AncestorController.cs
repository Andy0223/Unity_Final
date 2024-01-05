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
    private PlayerController playerController;
    public int layerBelonging;

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

        // Find the PlayerController GameObject
        GameObject playerControllerObject = GameObject.Find("Player");
        playerController = playerControllerObject.GetComponent<PlayerController>();

        layerBelonging = playerController.GetCurrentLayer();
        Debug.Log("layerBelonging: " + layerBelonging);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        if (transform.position.x >= maxRightXPosition)
        {
            Destroy(gameObject);
            playerController.ancestorOnLayers[layerBelonging] = false;
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
        if (collisionCount == maxCollisionCount)
        {
            Destroy(gameObject);
            playerController.ancestorOnLayers[layerBelonging] = false;
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
    }
}
