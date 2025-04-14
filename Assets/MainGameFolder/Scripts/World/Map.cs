using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;           // Сюда прикрепляем Tilemap из сцены
    public TileBase grassTile;        // Тайлы, которые будем ставить

    void Start()
    {
        RenderMap();
    }

    void RenderMap()
    {
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                TileBase tile = grassTile;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}