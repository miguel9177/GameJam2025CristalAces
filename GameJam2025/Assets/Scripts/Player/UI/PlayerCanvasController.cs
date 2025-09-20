using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasController : MonoBehaviour
{
    [SerializeField] private Image aimImage;

    public void ChangeAimColor(Color color)
    {
        aimImage.color = color;
    }
}
