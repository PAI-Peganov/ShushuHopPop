using UnityEngine;
using System.Collections.Generic;
using EntityBase;

public static class WorldManager
{

    private static Map Map;
    public static List<Vector2> HalfCellMoveVectors { get; private set; }
    public static Vector3 PlayerStart { get; private set; }

    public static void AddNewMap(Map map)
    {
        Map = map;
        MakeMoveVectors();
        PlayerStart = Map.GetGridPositionByGridCords(Map.GetPlayerStart);
    }

    private static void MakeMoveVectors()
    {
        var cell1 = Map.GetGridPositionByGridCords(new Vector3Int(0, 0, 0));
        var cell2 = Map.GetGridPositionByGridCords(new Vector3Int(1, 0, 0));
        var halfCellHeight = Mathf.Abs(cell2.y - cell1.y) / 2;
        var halfCellWidth = Mathf.Abs(cell2.x - cell1.x) / 2;
        HalfCellMoveVectors = new List<Vector2>
        {
            new(halfCellWidth, halfCellHeight),
            new(halfCellWidth, -halfCellHeight),
            new(-halfCellWidth, -halfCellHeight),
            new(-halfCellWidth, halfCellHeight)
        };
    }

    public static bool IsCellAvailableForEntity(Entity entity, Vector3 closePositionOfTargetCell) =>
        Map.IsCellAcceptedForMove(entity, Map.GetGridCordsByGridPosition(closePositionOfTargetCell));

    public static Vector3 ClarifyPosition(Vector3 position) => Map.ClarifyPosition(position);

    public static void UpdatePlayerLocation(Vector3 closePositionOfPlayer)
    {
        
    }
}
