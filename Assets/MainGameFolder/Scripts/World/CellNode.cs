using UnityEngine;
using UnityEngine.Tilemaps;
using EntityBase;
using System.Collections.Generic;

public class CellNode
{
    public HashSet<Entity> Entities { private set; get;}
    // тип должен совпадать с названием текстуры в файле (TODO Подумать над Variants и стоит ли делать кастомный тип)
    public string CellType { private set; get; }
    public TileBase CellTile { private set; get; }

    public CellNode() 
    { 
        Entities = new HashSet<Entity>();
        ChangeCellType("isometric_0000");
    }

    public void ChangeCellType(string cellType)
    {
        // TODO: сделать проверку на валидность
        CellType = cellType;
        CellTile = Resources.Load<TileBase>($"Tiles/Assets/{cellType}");
    }
    
    public bool TryAddEntity(Entity entity)
    {
        if (Cell.IsValid(entity, CellType))
        {
            Entities.Add(entity);
            return true;
        }
        return false;
    }

    public bool TryRemoveEntity(Entity entity)
    {
        return Entities.Remove(entity);
    }
}