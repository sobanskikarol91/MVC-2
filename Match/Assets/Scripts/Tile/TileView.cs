using UnityEngine;
using UnityEngine.UI;

public interface ITileView { }

[RequireComponent(typeof(Image))]
public class TileView : MonoBehaviour, ITileView
{
    public Image Image { get; set; }

    private void Awake()
    {
        Image = GetComponent<Image>();
    }
}