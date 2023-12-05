using System.Collections;
using UnityEngine;

public class AncestorController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float maxRightXPosition;
    public float maxLeftXPosition;

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
}
