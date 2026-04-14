using System;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{

    public GameObject StartPoint;
    public GameObject EndPoint;
    public enum CableState {none, electrified}
    [Range(0, 500)] public float ParticleAmount = 100;
    public ParticleSystem[] ParticleSystems;
    public GameObject Connector;
    public Dictionary<CableState, Material> ConnectorStateMaterial;


    [NonSerialized] public GameObject _followPoint;
    [NonSerialized] public bool _inLevel = false;
    [NonSerialized] public CableState CurrentCurrentState = CableState.none;


    [SerializeField] private Material[] _mats;



    private void PointFollow()//update pos of second cable point if follow point set by control cable
    {
        if(_followPoint != null)
        {
            _followPoint.transform.position = GameManager.Instance.CarInstance.CableSpawnPoint.transform.position;
        }
    }

    private void InitCable()//check for points set in inspector and init Dictionary fpr mats
    {
        if(StartPoint == null || EndPoint == null) Debug.Log("Start and End Points are not set correctly");
        ConnectorStateMaterial = new Dictionary<CableState, Material> 
        {
            {CableState.none, _mats[0]},
            {CableState.electrified, _mats[1]}
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

    private void OnEnable()
    {
        Controls.PauseEvent += TogglePaused;
    }
    private void OnDisable()
    {
        Controls.PauseEvent -= TogglePaused;
    }

    private void TogglePaused(bool _paused)
    {
        if (_paused)
        {
            foreach(ParticleSystem ps in ParticleSystems)
            {
                var _psMain = ps.main;
                _psMain.simulationSpeed = 0;
            }
        }
        else
        {
            foreach(ParticleSystem ps in ParticleSystems)
            {
                var _psMain = ps.main;
                _psMain.simulationSpeed = 1;
            }
        }
    }

}
