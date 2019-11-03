using UnityEngine;
using UnityEngine.UI;

public interface ITileView { }

[RequireComponent(typeof(Image))]
public class TileView : MonoBehaviour, ITileView
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}