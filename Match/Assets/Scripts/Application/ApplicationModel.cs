using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationModel", menuName = "Game/Application/Model")]
public class ApplicationModel : ScriptableObject
{
    public int ColorsAmount { get => colorsAmount; }
    public int Seed { get => seed; }
    public int Rows { get => height; }
    public int Columns { get => width; }
    public Color[] TileColors { get => tileColors; }
    public int MinMatchesAmount { get => minMatchesAmount; }
    public GameObject TilePrefab { get => tilePrefab; }

    // TODO: Create custom inspector for this parametr
    [SerializeField, Range(0, 6)] int colorsAmount;
    [SerializeField] int seed;
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] Color[] tileColors;
    [SerializeField] int minMatchesAmount = 3;
    [SerializeField] GameObject tilePrefab;
}