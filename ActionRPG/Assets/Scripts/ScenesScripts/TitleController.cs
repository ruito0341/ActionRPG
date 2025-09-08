using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TapToNextSceneWithFade : MonoBehaviour
{
    public Image fadeImage;      // 黒い Image をアタッチ
    public string nextSceneName = "SelectScene"; // 次のシーン名
    public float fadeDuration = 1f;

    void Start()
    {
        // フェードイン（黒 → 透明）
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            StartCoroutine(Fade(1f, 0f));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 画面タップでフェードアウトして次のシーンへ
            if (fadeImage != null)
            {
                StartCoroutine(FadeOutAndLoadScene());
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
              if (Input.GetMouseButtonDown(0))
    {
        Debug.Log("タップされました → " + nextSceneName + " に移動します");
        StartCoroutine(FadeOutAndLoadScene());
    }
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        yield return Fade(0f, 1f);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
