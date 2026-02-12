using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 5f;
    public float dashSpeed = 10f;
    private float currentMoveSpeed; // 現在の速度を保持

    [Header("Magic")]
    public GameObject magicPrefab;
    public float magicCost = 10f;
    public float magicCooldown = 1f;
    private bool canCastMagic = true;

    [Header("Attack")]
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;
    private bool isAttacking = false;

    [Header("Stats")]
    public int Level = 1;
    public int maxHP = 100;
    public int currentHP;
    public int maxMP = 50;
    public int currentMP;
    public int attack = 10;
    public int magicPower = 20;

    [Header("Exp")]
    public int currentExp = 0;
    public int nextExp = 100;

    [Header("UI")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;

    // ダブルタップ用
    public float doubleTapTime = 0.3f;
    private float lastTapTime = 0f;
    private KeyCode lastKey;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackHitbox.SetActive(false);
        currentHP = maxHP;
        currentMP = maxMP;
        UpdateHPText();
        UpdateMPText();
        currentMoveSpeed = Speed;
    }

    void Update()
    {
        // 1. 入力の取得
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;

        // 2. ダブルタップによるダッシュ判定 (左右のみ)
        if (Input.GetKeyDown(KeyCode.A)) CheckDash(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.D)) CheckDash(KeyCode.D);
        
        // 何も押していない時は速度を戻す（または減速処理）
        if (moveInput == Vector2.zero) currentMoveSpeed = Speed;

        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        // 3. 攻撃・魔法入力
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.E) && canCastMagic && currentMP >= magicCost)
        {
            StartCoroutine(CastMagic());
        }
    }

    void FixedUpdate()
    {
        // 物理移動はFixedUpdateで一括処理
        rb.velocity = moveInput * currentMoveSpeed;
    }

    void CheckDash(KeyCode key)
    {
        if (lastKey == key && Time.time - lastTapTime < doubleTapTime)
        {
            currentMoveSpeed = dashSpeed;
            lastTapTime = 0; // リセット
        }
        else
        {
            currentMoveSpeed = Speed;
            lastTapTime = Time.time;
        }
        lastKey = key;
    }

    private IEnumerator CastMagic()
    {
        canCastMagic = false;
        currentMP -= (int)magicCost;
        UpdateMPText();

        if (magicPrefab != null)
        {
            // 4方向への丸め処理
            Vector2 shootDir = lastMoveDir;
            if (Mathf.Abs(shootDir.x) > Mathf.Abs(shootDir.y))
                shootDir = new Vector2(Mathf.Sign(shootDir.x), 0f);
            else
                shootDir = new Vector2(0f, Mathf.Sign(shootDir.y));

            Vector3 spawnPos = transform.position + (Vector3)shootDir * 0.6f;
            GameObject proj = Instantiate(magicPrefab, spawnPos, Quaternion.identity);

            if (proj.TryGetComponent<MagicProjectile>(out var mp))
            {
                mp.SetDirection(shootDir);
                mp.SetPower(magicPower);
            }
        }

        yield return new WaitForSeconds(magicCooldown);
        canCastMagic = true;
    }

    private IEnumerator Attack()

    
    {
        isAttacking = true;
        attackHitbox.transform.localPosition = (Vector3)lastMoveDir * 0.5f; // 少し離す
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);
        isAttacking = false;
    }

    public void GainExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= nextExp)
        {
            LevelUp();
        }
    }
    public int GetLevel()
    {
        return Level;
    }

    // --- 以下、ステータス関連 ---
    public void UpdateHPText() => hpText.text = $"HP: {currentHP} / {maxHP}";
    public void UpdateMPText() => mpText.text = $"MP: {currentMP} / {maxMP}";

    public void TakeDamage(int dmg)
    {
        currentHP = Mathf.Max(0, currentHP - dmg);
        UpdateHPText();
        if (currentHP <= 0) Debug.Log("Player Dead");
    }

    public void LevelUp()
    {
        Level++;
        currentExp -= nextExp;
        nextExp = Mathf.RoundToInt(nextExp * 1.5f);

        maxHP += 20;
        maxMP += 10;
        attack += 10;
        magicPower += 10;

        currentHP = maxHP;
        currentMP = maxMP;
        UpdateHPText();
        UpdateMPText();
    }
}