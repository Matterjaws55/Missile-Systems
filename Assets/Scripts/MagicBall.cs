using UnityEngine;
using TMPro;
using System.Collections;
using System.Xml;

public class MagicBall : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI cursorText;
    public string[] textOptions;
    public float fadeDuration = 2f;
    public float yOffset = -70f;
    public float xOffset = 15f;

    [Header("Shake Settings")]
    public float intensity = 0.015f; 
    public float duration = 0.15f;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    bool isTextVisible = false;

    private void Start()
    {
        rectTransform = cursorText.GetComponent<RectTransform>();
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform.parent as RectTransform, Input.mousePosition, null, out mousePos);
        rectTransform.position = mousePos + new Vector3(xOffset, yOffset, 0);
    }

    private void OnMouseDown()
    {
        if(!isTextVisible)
        {
            isTextVisible = true;
            int randomIndex = Random.Range(0, textOptions.Length);
            cursorText.text = textOptions[randomIndex];
            StartCoroutine(ShakeObject());
            StartCoroutine(FadeOutText());
        }               
    }

    private IEnumerator FadeOutText()
    {
        Color originalColor = cursorText.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            cursorText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        cursorText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        isTextVisible = false;
    }

    private IEnumerator ShakeObject()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float offsetX = Random.Range(-intensity, intensity);
            float offsetY = Random.Range(-intensity, intensity);

            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
