using UnityEngine;


[CreateAssetMenu(fileName = "ApplicationModel", menuName = "Game/Application/Model")]
public class ApplicationModel : ScriptableObject
{
    public int ColorsAmount { get => tileColors.Length; }
    public int Seed { get => seed; }
    public int Rows { get => height; }
    public int Columns { get => width; }
    public Color[] TileColors { get => tileColors; }
    public int MatchSequenceLength { get => matchSequenceLength; }
    public GameObject TilePrefab { get => tilePrefab; }

    [SerializeField] int seed;
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] int matchSequenceLength = 3;
    [SerializeField] Color[] tileColors;
    [SerializeField] GameObject tilePrefab;
}