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

}
