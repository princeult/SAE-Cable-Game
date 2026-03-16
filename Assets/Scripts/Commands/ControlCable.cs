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

                GameManager.Instance.CarInstance.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = false;
                if(GameManager.Instance.CableInstance == null)
                {
                    GameManager.Instance.SpawnCable(GameManager.Instance.CableRefrence);
                    GameManager.Instance.CableInstance.StartPoint.transform.position = GameManager.Instance.CarInstance.CableSpawnPoint.transform.position;
                }
                    foreach(ParticleSystem  _ps in GameManager.Instance.CableInstance.ParticleSystems)
                    {
                        var _psEmission = _ps.emission;
                        _psEmission.rateOverTime = 0;

                    }
                    GameManager.Instance.CableInstance.Connector.GetComponent<MeshRenderer>().material = GameManager.Instance.CableInstance.ConnectorStateMaterial[Cable.CableState.none];
                    GameManager.Instance.CableInstance.StartPoint.transform.position = GameManager.Instance.CarInstance.CableSpawnPoint.transform.position;
                    GameManager.Instance.CableInstance.CurrentCurrentState = Cable.CableState.none;
                    GameManager.Instance.CableInstance.gameObject.SetActive(true);
                    GameManager.Instance.CableInstance._followPoint = GameManager.Instance.CableInstance.EndPoint;

                return ICableInteract.CurrentCablePoint.start;

            case ICableInteract.CurrentCablePoint.start:

            GameManager.Instance.CableInstance._followPoint = null;

                return ICableInteract.CurrentCablePoint.end;

            case ICableInteract.CurrentCablePoint.end:

                GameManager.Instance.CableInstance.gameObject.SetActive(false);
                GameManager.Instance.CarInstance.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = true;

                return ICableInteract.CurrentCablePoint.none;
        }
        return ICableInteract.CurrentCablePoint.none;
    }

    public void SetCableState(Cable.CableState _newState)
    {
        Dictionary<Cable.CableState, Color32> _psColour = GameManager.Instance.ParticleSystemColour;
        GameManager.Instance.CableInstance.Connector.GetComponent<MeshRenderer>().material = GameManager.Instance.CableInstance.ConnectorStateMaterial[_newState];
        if(_newState == Cable.CableState.none)
        {
            GameManager.Instance.CableInstance.CurrentCurrentState = Cable.CableState.none;
            foreach(ParticleSystem  _ps in GameManager.Instance.CableInstance.ParticleSystems)
            {
                var _psEmission = _ps.emission;
                _psEmission.rateOverTime = 0;

            }
        }
        else
        {

            GameManager.Instance.CableInstance.CurrentCurrentState = _newState;
            foreach(ParticleSystem  _ps in GameManager.Instance.CableInstance.ParticleSystems)
            {
                var _psMain = _ps.main;
                var _psEmission = _ps.emission;
                _psEmission.rateOverTime = GameManager.Instance.CableInstance.ParticleAmount;
               _psMain.startColor =  new ParticleSystem.MinMaxGradient(_psColour[_newState]);
            }
        }      
     }
}

