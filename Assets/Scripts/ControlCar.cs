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
        GameManager.Instance.Car.moveDirection = _direction;
    }

    public ICableInteract.CurrentCablePoint PlaceCable(ICableInteract.CurrentCablePoint currentCablePoint)
    {
        switch (currentCablePoint)
        {
            case ICableInteract.CurrentCablePoint.none:

                if(!GameManager.Instance.Cable._inLevel)
                {
                    GameManager.Instance.SpawnCable(GameManager.Instance.Cable);
                }
                else
                {
                    GameManager.Instance.Cable._startPoint.transform.position = GameManager.Instance.Car.CableSpawnPoint.transform.position;
                    GameManager.Instance.Cable.gameObject.SetActive(true);             
                }

                    GameManager.Instance.Cable._followPoint = GameManager.Instance.Cable._endPoint;

                return ICableInteract.CurrentCablePoint.start;

            case ICableInteract.CurrentCablePoint.start:

            GameManager.Instance.Cable._followPoint = null;

                return ICableInteract.CurrentCablePoint.end;

            case ICableInteract.CurrentCablePoint.end:

                GameManager.Instance.Cable.gameObject.SetActive(false);

                return ICableInteract.CurrentCablePoint.none;
        }
        return ICableInteract.CurrentCablePoint.none;
    }
}
