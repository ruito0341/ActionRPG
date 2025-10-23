using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 20;

    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
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
        Debug.Log("Hitbox triggered with " + collision.name);

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }    
}
