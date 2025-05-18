using UnityEngine;
using UnityEngine.EventSystems;

public interface ICharacterMotor
{
    public Vector3 MoveDirection { get; }
    public void SetCharacterMove(params Vector2[] controllerDirection);
}
