using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap[] layers;
 
    public int MapWidth = 24;
    public int MapHeight = 24;
    private int LayersCount;

    public CellNode[,,] map;

    public Vector3 GetGridPositionByGridCords(Vector3Int cords) => grid.GetCellCenterWorld(cords);

    // debug метод
    void GenerateEmptyMap()
    {
        map = new CellNode[LayersCount, MapHeight, MapWidth]; 
        for (var k = 0; k < LayersCount; k++) 
            for (var i = 0; i < MapHeight; i++)
                for (var j = 0; j < MapWidth; j++) 
                    map[k, i, j] = new CellNode();
    }

    // debug метод
    void GenerateRandomMap()
    {
        var rnd = new System.Random();

        map = new CellNode[LayersCount, MapHeight, MapWidth];
        for (var k = 0; k < LayersCount; k++)
            for (var i = 0; i < MapHeight; i++)
                for (var j = 0; j < MapWidth; j++)
                {
                    map[k, i, j] = new CellNode();
                    if (rnd.Next(3) == 0)
                        map[k, i, j].ChangeCellType("isometric_0056");
                    if (rnd.Next(3) == 0)
                        map[k, i, j].ChangeCellType("isometric_0057");
                }
    }



    void Start()
    {
        LayersCount = layers.Length;
        GenerateRandomMap();
        RenderMap();
        WorldManager.AddNewMap(this);
    }

    void RenderMap()
    {
        for (var k = 0; k < LayersCount; k++)
            for (var i = 0; i < MapHeight; i++)
                for (var j = 0; j < MapWidth; j++)
                {
                    // нужно учитывать что-чем выше уровень тем больше он уходит вверх
                    layers[k].SetTile(new Vector3Int(i + k, j + k, 0), map[k, i, j].CellTile);
                }
    }
}