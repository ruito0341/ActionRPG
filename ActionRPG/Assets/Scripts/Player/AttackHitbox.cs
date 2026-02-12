using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour


{
    public int damage = 0;

    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
            rb.useFullKinematicContacts = true;
        }

        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null) return;

        // プレイヤーの攻撃力を取得（AttackHitbox がプレイヤーの子である想定）
        PlayerManager player = GetComponentInParent<PlayerManager>();

        // ここで合算方法を決める：
        // 1) プレイヤーの attack のみをダメージにしたい場合：
        int totalDamage = (player != null) ? player.attack : damage;

        // 2) 基礎ダメージ + プレイヤー攻撃力 にしたい場合は上を次のようにする：
        // int totalDamage = damage + (player != null ? player.attack : 0);

        Debug.Log($"AttackHitbox hit {enemy.name} baseDamage={damage} playerAttack={(player!=null?player.attack:-1)} totalDamage={totalDamage}");

        enemy.TakeDamage(totalDamage);
        }
    }    

