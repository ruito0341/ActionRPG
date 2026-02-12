using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager playerManager;
    public int maxHealth = 100;
    private int currentHealth;

    public int expOnDeath = 50;

    public float speed = 3f;

    private Transform player;

    

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
            {
                player = playerObj.transform;
            }
            return;
        }

        // プレイヤーとの距離が0.1f未満になったらそれ以上実行しない
        if (Vector2.Distance(transform.position, player.position) < 0.1f)
            return;

             // プレイヤーに向けて進む
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, player.position.y), 
            speed * Time.deltaTime);
    }

    // 敵のHPを減らすメソッド
    public void TakeDamage(int damage)
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        Vector3 scale = transform.localScale;
        scale.x = (player.position.x < transform.position.x) ? -1 : -1;
        transform.localScale = scale;


        currentHealth -= damage;
        Debug.Log(gameObject.name + "はダメージを受けた! 残りHP:" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (playerManager == null)
        {
           playerManager = FindFirstObjectByType<PlayerManager>();
        }

        if (playerManager != null)
        {
            playerManager.GainExp(expOnDeath);
        }
        
        Debug.Log(gameObject.name + "は倒した!");
        Destroy(gameObject);
    }
}
