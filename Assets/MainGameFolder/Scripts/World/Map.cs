using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap[] layers;
 
    public int MapWidth = 24;
    public int MapHeight = 24;
    private int LayersCount = 0;

    public CellNode[,,] map;

    // debug метод
    void GenerateEmptyMap()
    {
        map = new CellNode[LayersCount, MapHeight, MapWidth]; 
        for (int k = 0; k < LayersCount; k++) 
            for  (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++) 
                    map[k, i, j] = new CellNode();
    }

    // debug метод
    void GenerateRandomMap()
    {
        var rnd = new System.Random();

        map = new CellNode[LayersCount, MapHeight, MapWidth];
        for (int k = 0; k < LayersCount; k++)
            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
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
    }

    void RenderMap()
    {
        for (int k = 0; k < LayersCount; k++)
            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                {
                    // нужно учитывать что-чем выше уровень тем больше он уходит вверх
                    layers[k].SetTile(new Vector3Int(i + k, j + k, 0), map[k, i, j].CellTile);
                }
    }
}