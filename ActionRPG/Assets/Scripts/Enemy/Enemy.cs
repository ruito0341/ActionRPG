using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager playerManager;
    public int maxHealth = 100;
    private int currentHealth;

    public int expOnDeath = 50;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 敵のHPを減らすメソッド
    public void TakeDamage(int damage)
    {
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
           playerManager = FindObjectOfType<PlayerManager>();
        }

        if (playerManager != null)
        {
            playerManager.GainExp(expOnDeath);
        }
        else
        {
            Debug.LogWarning("PlayerManager が割り当てられていません");
        }
        Debug.Log(gameObject.name + "は倒した!");
        Destroy(gameObject);
    }
}
