using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class CanvasBackgroundFiller : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.preserveAspect = true;
        FitToScreen();
    }

    private void Update()
    {
        FitToScreen();
    }

    private void FitToScreen()
    {
        if (image.sprite == null) return;

        float screenRatio = (float)Screen.width / Screen.height;
        float imageRatio = (float)image.sprite.rect.width / image.sprite.rect.height;

        if (screenRatio >= imageRatio)
        {
            float scale = (float)Screen.width / image.sprite.rect.width;
            rectTransform.sizeDelta = new Vector2(Screen.width, image.sprite.rect.height * scale);
        }
        else
        {
            float scale = (float)Screen.height / image.sprite.rect.height;
            rectTransform.sizeDelta = new Vector2(image.sprite.rect.width * scale, Screen.height);
        }

        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
