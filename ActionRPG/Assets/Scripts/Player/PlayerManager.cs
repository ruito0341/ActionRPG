using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down; // 最初は下向き

    [Header("Attack")]
    public GameObject attackHitbox;   // 子オブジェクトをアサイン
    public float attackDuration = 0.2f;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackHitbox.SetActive(false);
    }

    void Update()
    {
        // 移動入力
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;

        // 移動方向を保存（攻撃時の向き用）
        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        // 攻撃入力
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        // ヒットボックスを向きに応じて配置
        attackHitbox.transform.localPosition = lastMoveDir;

        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.SetActive(false);

        isAttacking = false;
    }

    void OnDrawGizmos()
    {
        // ヒットボックスの位置を視覚化
        if (attackHitbox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackHitbox.transform.position, attackHitbox.GetComponent<BoxCollider2D>().size);
        }
    }
}
