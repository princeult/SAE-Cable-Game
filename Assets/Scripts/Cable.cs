using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public enum CableState {none, electrified};
    
    public GameObject StartPoint;
    public GameObject EndPoint;

    [System.NonSerialized] public GameObject _followPoint;
    [System.NonSerialized] public bool _inLevel = false;

    [SerializeField] private GameObject _connecter;
    [SerializeField] private Material[] _mats;
    [SerializeField] private ParticleSystem[] _particleSystems;

    private Dictionary<CableState, Material> _stateColour;
    private CableState _currentState = CableState.none;
    private GameObject[] _cableParts;


    public void SetCableState(CableState _newState)
    {
        switch (_newState)
        {
            case CableState.none:

                foreach(ParticleSystem _ps in _particleSystems)
                {
                    _ps.gameObject.SetActive(false);
                }

            break;
            case CableState.electrified:

                foreach(ParticleSystem _ps in _particleSystems)
                {
                    _ps.gameObject.SetActive(true);
                }

            break;
        }
        foreach(GameObject _part in _cableParts)
        {
            _part.GetComponent<MeshRenderer>().material = _stateColour[_newState];
        }
        _currentState = _newState;
    }
    private void PointFollow()
    {
        if(_followPoint != null)
        {
            _followPoint.transform.position = GameManager.Instance.Car.CableSpawnPoint.transform.position;
        }
    }

    private void InitCable()
    {
        if(StartPoint == null || EndPoint == null) Debug.Log("Start and End Points are not set correctly");
       _stateColour = new Dictionary<CableState, Material> 
        {
            {CableState.none, _mats[0]},
            {CableState.electrified, _mats[1]}
        };

        _cableParts = new GameObject[]
        {
            StartPoint,
            EndPoint,
            _connecter
        };
    }
    private void Awake()
    {
        InitCable();
    }

    private void FixedUpdate()
    {
        PointFollow();
    }

}
