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
        SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectKey.placeCable);
        switch (currentCablePoint)
        {
            case ICableInteract.CurrentCablePoint.none: //ResetCable to starting setup. Place first Point of cable, set second Point to follow the Car.

                GameManager.Instance.CarInstance.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = false;
                if(GameManager.Instance.CableInstance == null)
                {
                    GameManager.Instance.SpawnCable(GameManager.Instance.CableRefrence);
                    GameManager.Instance.CableInstance.StartPoint.transform.position = GameManager.Instance.CarInstance.CableSpawnPoint.transform.position; //Yes you'll notice that im getting an Instance everytime. if i Try setting it as a local var it breaks
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

                return ICableInteract.CurrentCablePoint.start; //On return we update the state inside the Controls for this statemachine



            case ICableInteract.CurrentCablePoint.start: //Place Second point on Cable on the ground and stop following car

            GameManager.Instance.CableInstance._followPoint = null;

                return ICableInteract.CurrentCablePoint.end;



            case ICableInteract.CurrentCablePoint.end: //Pickup Cable and return to first state

                GameManager.Instance.CableInstance.gameObject.SetActive(false);
                GameManager.Instance.CarInstance.CableSpawnPoint.GetComponent<MeshRenderer>().enabled = true;

                return ICableInteract.CurrentCablePoint.none;


        }
        return ICableInteract.CurrentCablePoint.none;
    }

    public void SetCableStateVisuals(Cable.CableState _newState) //here we update the cable visuals to match the current "gameplay state" what kind of power is flowing though basicly
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
            SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectKey.electrified);
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

