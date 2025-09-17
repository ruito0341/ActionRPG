using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStats : MonoBehaviour
{
    public int Level = 1;

    //Base Stats
    public int maxHP = 100;
    public int currentHP;

    public int maxMP = 50;
    public int currentMP;

    public int attack = 10;
    public int defense = 5;
    public int speed = 5;

    //Experiencce
    public int currentExp = 0;
    public int nextExp = 100;

    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    //add exp
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
        speed += 1;

        currentHP = maxHP;
        currentMP = maxMP;

        Debug.Log("Leveled Up! You are now level " + Level);
    }
}