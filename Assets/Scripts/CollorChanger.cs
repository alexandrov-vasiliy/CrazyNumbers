using UnityEngine;
using Zenject;

public class CollorChanger : MonoBehaviour
{


    [Header("Color settings")] [Space(5f)] public Material trailMaterial;

    [Space(5f)] public Color[] colorTable;


    private GameObject tempObstacle;

    private Vector2 tempPos;

    private Vector3 screenSize;

    private Color color;

    [Inject] private Player _player;
    private void Start()
    {
        RandomizePlayerColor();
    }

    private void RandomizePlayerColor()
    {
        color = colorTable[Random.Range(0, colorTable.Length)];
        _player.SetColor(color);
        trailMaterial.color = color;
    }
}