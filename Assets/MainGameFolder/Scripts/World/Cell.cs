using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using EntityBase;
using System.Collections.Generic;
using UnityEngine.WSA;
using System;
using UnityEditor.U2D.Aseprite;

public static class Cell
{
    // Тут будет храниться список клеток куда может заходить данный ентити. TODO подумать как его сделать нормально
    static private Dictionary<string, string> validList = new Dictionary<string, string>();

    public static bool IsValid(Entity entity, string CellType)
    {
        return validList[CellType].Contains(entity.Id);
    }
}