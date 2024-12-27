using UnityEngine;
using UnityEngine.UI;

public class ImageWithSwappableColorSystem : Singleton<ImageWithSwappableColorSystem>
{
    [SerializeField] Image image;

    public void ChangeImageColor(Color newColor)
    {
        image.color = newColor;
    }
}
