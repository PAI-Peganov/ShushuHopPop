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
    [SerializeField]
    private Tilemap[] structsLayers;
    [SerializeField]
    private Vector2Int PlayerStart;
 
    public int MapWidth = 24;
    public int MapHeight = 24;
    private int LayersCount;

    public CellNode[,,] map;

    public Vector3 GetGridPositionByGridCords(Vector3Int cords) => grid.GetCellCenterWorld(cords);

    public Vector3 ClarifyPosition(Vector3 position) => grid.GetCellCenterWorld(grid.WorldToCell(position));

    public Vector3Int GetPlayerStart => new Vector3Int(PlayerStart.x, PlayerStart.y, 0);

    void Awake()
    {
        WorldManager.AddNewMap(this);
    }

    void Start()
    {
        LayersCount = layers.Length;
        GenerateRandomMap();
        RenderMap();
    }

    void RenderMap()
    {
        for (var k = 0; k < LayersCount; k++)
            for (var i = 0; i < MapHeight; i++)
                for (var j = 0; j < MapWidth; j++)
                {
                    // ����� ��������� ���-��� ���� ������� ��� ������ �� ������ �����
                    layers[k].SetTile(new Vector3Int(i + k, j + k, k), map[k, i, j].CellTile);
                }
    }

    // debug �����
    void GenerateEmptyMap()
    {
        map = new CellNode[LayersCount, MapHeight, MapWidth];
        for (var k = 0; k < LayersCount; k++)
            for (var i = 0; i < MapHeight; i++)
                for (var j = 0; j < MapWidth; j++)
                    map[k, i, j] = new CellNode();
    }

    // debug �����
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
}