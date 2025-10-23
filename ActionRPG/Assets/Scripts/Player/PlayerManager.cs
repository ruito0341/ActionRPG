using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]

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

    public int Level = 1;

    //Base Stats
    public int maxHP = 100;
    public int currentHP;

    public int maxMP = 50;
    public int currentMP;

    public int attack = 10;
    public int defense = 5;
    public int Speed = 5;

    //Experiencce
    public int currentExp = 0;
    public int nextExp = 100;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackHitbox.SetActive(false);

        currentHP = maxHP;
        currentMP = maxMP;

        //最初にHPとMPのテキストを更新

        UpdateHPText();
        UpdateMPText();
    }

    void UpdateHPText()
    {
        hpText.text = "HP: " + currentHP.ToString();
    }
    void UpdateMPText()
    {
        mpText.text = "MP: " + maxMP.ToString();
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

    public void GainExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= nextExp)
        {
            LevelUp();
        }
    }

    //Level Up process
    void LevelUp()
    {
        Level++;
        currentExp -= nextExp;
        nextExp = Mathf.RoundToInt(nextExp * 1.5f);//increase next exp by 50%

        //Increase Stats
        maxHP += 20;
        maxMP += 10;
        attack += 5;
        defense += 5;
        Speed += 1;

        currentHP = maxHP;
        currentMP = maxMP;

        Debug.Log("Leveled Up! You are now level " + Level);
    }
    void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(0, currentHP);
        UpdateHPText();

        if (currentHP <= 0)
        {
            Debug.Log("Player Dead");
            Destroy(gameObject); // 必要に応じて死亡処理を差し替えてください
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
         int AttackHitbox = LayerMask.NameToLayer("PlayerAttack");
        if (AttackHitbox != -1 && other.gameObject.layer == AttackHitbox) return;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("OnTriggerEnter2D hit: " + other.name);
            TakeDamage(10);
        }
    }


        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
        Debug.Log("OnCollisionEnter2D hit: " + collision.collider.name);
        TakeDamage(10);
        }
    }
}
