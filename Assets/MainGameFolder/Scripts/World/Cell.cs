using EntityBase;
using System.Collections.Generic;


public static class Cell
{
    // ��� ����� ��������� ������ ������ ���� ����� �������� ������ ������. TODO �������� ��� ��� ������� ���������
    static private Dictionary<string, string> validList = new Dictionary<string, string>();

    public static bool IsValid(Entity entity, string CellType)
    {
        return validList[CellType].Contains(entity.Id);
    }
}