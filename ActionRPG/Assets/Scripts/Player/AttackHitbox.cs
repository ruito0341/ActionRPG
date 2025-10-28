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
        if(rb != null)
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
        if (enemy != null)
        {
            //プレイヤーの攻撃力を取得
            PlayerManager player = GetComponentInParent<PlayerManager>();
            int totalDamage = damage;
            if (player != null)
            {
                totalDamage += player.attack;
            }
            else
            {
                //playerが取れない場合はbaseDamageのみ
                Debug.LogWarning("playermanagerがいません");
            }
            enemy.TakeDamage(totalDamage);
        }
    }    
}
