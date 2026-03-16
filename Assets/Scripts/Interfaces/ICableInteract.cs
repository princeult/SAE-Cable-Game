using UnityEngine;

public interface ICableInteract
{
    public enum InteractType {none, player, cable, enemy};
    public enum CurrentCablePoint {none, start, end};
    public CurrentCablePoint PlaceCable(CurrentCablePoint currentCablePoint);
    public bool Interact(InteractType type);
    public void MoveControl(Vector2 _direction);
    public void SetCableStateVisuals(Cable.CableState _newState);
}
