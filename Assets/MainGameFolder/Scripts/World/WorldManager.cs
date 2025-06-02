using System;
using UnityEngine;
using System.Collections.Generic;
using EntityBase;
using MainGameFolder.Scripts.UI.QuestsWindow;

public static class WorldManager
{

    private static Map Map;
    public static List<Vector2> HalfCellMoveVectors { get; private set; }
    public static Vector3 PlayerStart { get; private set; }
    public static Vector3 PlayerPosition { get; private set; }
    
    private static QuestListManager _questListManager;
    
    private static SubtilesController _subtitlesController;

    public static void AddNewMap(Map map)
    {
        Map = map;
        MakeMoveVectors();
        PlayerStart = Map.GetGridPositionByGridCords(Map.GetPlayerStart);
    }

    public static void AddQuestListManager(QuestListManager questListManager)
    {
        _questListManager = questListManager;
    }
    
    public static void AddSubtitlesController(SubtilesController subtitlesController)
    {
        _subtitlesController = subtitlesController;
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
        PlayerPosition = closePositionOfPlayer;
    }

    public static Vector3 GetWorldPositionFromCell(Vector2Int cell) =>
        Map.GetGridPositionByGridCords(new Vector3Int(cell.x, cell.y));

    public static void CompleteQuest(string name)
    {
        if (name is null)
        {
            Debug.LogException(new Exception("Quest name is null"));
            return;
        }

        if (_questListManager.TryMarkQuestAsCompleted(name, out var task))
            _subtitlesController.PlaySubtiles(task.Name);
    }
}
