using UnityEngine;

public interface ICharacterMotor
{
    public void SetCharacterMove(params Vector2[] controllerDirection);
}
