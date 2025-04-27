using UnityEngine;
using System.Collections.Generic;

public static class WorldManager
{

    private static Map Map;
    public static List<Vector2> HalfCellMoveVectors { get; private set; }

    public static void AddNewMap(Map map)
    {
        Map = map;
        MakeMoveVectors();
    }

    private static void MakeMoveVectors()
    {
        var cell1 = Map.GetGridPositionByGridCords(new Vector3Int(0, 0, 0));
        var cell2 = Map.GetGridPositionByGridCords(new Vector3Int(1, 0, 0));
        var halfCellHeight = cell2.y - cell1.y / 2;
        var halfCellWidth = cell2.x - cell1.x / 2;
        HalfCellMoveVectors = new List<Vector2>
        {
            new(halfCellWidth, halfCellHeight),
            new(halfCellWidth, -halfCellHeight),
            new(-halfCellWidth, -halfCellHeight),
            new(-halfCellWidth, halfCellHeight)
        };
    }


    public static bool IsCellAvailableForPlayer(Vector3Int position)
    {
        //TODO: переписать
        return true;
    }
}
