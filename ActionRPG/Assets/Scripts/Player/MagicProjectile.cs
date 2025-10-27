// ...existing code...
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 2f;

    // 進行方向（外部から設定）
    private Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(gameObject, lifetime);

        // Rigidbody2D がなければ追加して kinematic にする（接触イベントを確実に受けるため）
        var rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
        }

        // 初期回転を方向に合わせる
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void Update()
    {
        // 方向に沿って移動
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);
    }

    // PlayerManager から呼ばれるメソッド
    public void SetDirection(Vector2 dir)
    {
        if (dir == Vector2.zero) dir = Vector2.right;
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }


    }
}
// ...existing code...