using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 3f; // 設定移動速度

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 向左移動
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }
}
