using Unity.VisualScripting;
using UnityEngine;

public class ControlCar : ICableInteract
{
    public bool Interact(ICableInteract.InteractType type)
    {
        throw new System.NotImplementedException();
    }

    public void MoveControl(Vector2 _direction)
    {
        GameManager.Instance.CarInstance.moveDirection = _direction;
    }

    public ICableInteract.CurrentCablePoint PlaceCable(ICableInteract.CurrentCablePoint currentCablePoint)
    {
        throw new System.NotImplementedException();
    }

    public void SetCableState(Cable.CableState _newState)
    {
        throw new System.NotImplementedException();
    }
}
