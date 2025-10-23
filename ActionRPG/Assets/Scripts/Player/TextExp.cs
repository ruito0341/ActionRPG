using UnityEngine;

public class TestExp : MonoBehaviour
{
    public PlayerManager playerManager;

    void Update()
    {
        // キー押しで経験値を加算してテスト
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerManager.GainExp(50);
        }
    }
}
