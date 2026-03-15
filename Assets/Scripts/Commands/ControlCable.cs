using System.Collections.Generic;
using UnityEngine;

public class ControlCable : ICableInteract
{
    public bool Interact(ICableInteract.InteractType type)
    {
        throw new System.NotImplementedException();
    }

    public void MoveControl(Vector2 _direction)
    {
        throw new System.NotImplementedException();
    }

    public ICableInteract.CurrentCablePoint PlaceCable(ICableInteract.CurrentCablePoint currentCablePoint)
    {
        switch (currentCablePoint)
        {
            case ICableInteract.CurrentCablePoint.none:

                GameManager.Instance.Car.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = false;
                if(!GameManager.Instance.Cable._inLevel)
                {
                    GameManager.Instance.SpawnCable(GameManager.Instance.Cable);
                    GameManager.Instance.Cable.StartPoint.transform.position = GameManager.Instance.Car.CableSpawnPoint.transform.position;
                }
                else
                {
                    GameManager.Instance.Cable.StartPoint.transform.position = GameManager.Instance.Car.CableSpawnPoint.transform.position;
                    GameManager.Instance.Cable.gameObject.SetActive(true);             
                }
                    GameManager.Instance.Cable._followPoint = GameManager.Instance.Cable.EndPoint;

                return ICableInteract.CurrentCablePoint.start;

            case ICableInteract.CurrentCablePoint.start:

            GameManager.Instance.Cable._followPoint = null;

                return ICableInteract.CurrentCablePoint.end;

            case ICableInteract.CurrentCablePoint.end:

                GameManager.Instance.Cable.gameObject.SetActive(false);
                GameManager.Instance.Car.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = true;

                return ICableInteract.CurrentCablePoint.none;
        }
        return ICableInteract.CurrentCablePoint.none;
    }

    public void SetCableState(Cable.CableState _newState)
    {
        Dictionary<Cable.CableState, Color32> _psColour = GameManager.Instance.ParticleSystemColour;
        GameManager.Instance.Cable.Connector.GetComponent<MeshRenderer>().material = GameManager.Instance.Cable.ConnectorStateMaterial[_newState];
        if(_newState == Cable.CableState.none)
        {
            GameManager.Instance.Cable.CurrentCurrentState = Cable.CableState.none;
            foreach(ParticleSystem  _ps in GameManager.Instance.Cable.ParticleSystems)
            {
                var _psEmission = _ps.emission;
                _psEmission.rateOverTime = 0;

            }
        }
        else
        {

            GameManager.Instance.Cable.CurrentCurrentState = _newState;
            foreach(ParticleSystem  _ps in GameManager.Instance.Cable.ParticleSystems)
            {
                var _psMain = _ps.main;
                var _psEmission = _ps.emission;
                _psEmission.rateOverTime = GameManager.Instance.Cable.ParticleAmount;
               _psMain.startColor =  new ParticleSystem.MinMaxGradient(_psColour[_newState]);
            }
        }      
     }
}

