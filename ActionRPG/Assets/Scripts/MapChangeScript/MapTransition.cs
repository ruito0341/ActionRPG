using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransition : MonoBehaviour
{
    [SerializeField] string nextSceneName; // 行き先のシーン名

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーに触れたら
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
