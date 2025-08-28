using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [Header("Settings")]
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, Input.mousePosition, null, out mousePos);

        rectTransform.anchoredPosition = mousePos;
    }
}