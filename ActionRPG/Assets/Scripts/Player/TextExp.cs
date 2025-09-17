using UnityEngine;

public class TestExp : MonoBehaviour
{
    public PlayerStats playerStats;

    void Update()
    {
        // キー押しで経験値を加算してテスト
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerStats.GainExp(50);
        }
    }
}
