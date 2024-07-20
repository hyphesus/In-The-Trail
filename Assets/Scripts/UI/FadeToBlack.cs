using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f; // Duration for the fade effect
    public PlayerController playerController;
    private void Start()
    {
        // Ensure the image is completely transparent at the start
        SetAlpha(0f);
    }

    // Call this method to start the fade-to-black effect
    public void FadeToBlack()
    {
        print("dead");
        StartCoroutine(FadeToBlackCoroutine());
    }

    private IEnumerator FadeToBlackCoroutine()
    {
        Time.timeScale = 0f;
        //playerController.enabled = false;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        // Ensure the image is completely black at the end
        SetAlpha(1f);

        // Wait for 2 seconds before fading back in
        yield return new WaitForSeconds(2f);

        // Start fading back to normal
        StartCoroutine(FadeToClearCoroutine());
    }

    private IEnumerator FadeToClearCoroutine()
    {
        
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        // Ensure the image is completely transparent at the end
        SetAlpha(0f);
        //playerController.enabled = true;
        Time.timeScale = 1f;
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
