using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Quests : MonoBehaviour
{
    public UnityEvent CompletionEvent;
    public Cable.CableState CurrentCableState;

    [NonSerialized] public readonly ICableInteract CableControl = new ControlCable();
    
    [SerializeField] private GameObject[] _points;

    private bool _questCompleted; 

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dictionary<Cable.CableState, Color32> _psColour = GameManager.Instance.ParticleSystemColour;
        foreach(GameObject _point in _points)
        {
        var _psMain = _point.GetComponentInChildren<ParticleSystem>(true).main;
        _psMain.startColor =  new ParticleSystem.MinMaxGradient(_psColour[CurrentCableState]);
        }
        
    }

    // Update is called once per frame
    public void CompleteQuest()
    {
        if(!_questCompleted)
        {
            foreach(GameObject _point in _points)
            {
                _point.GetComponent<MeshRenderer>().material = GameManager.Instance.CableInstance.ConnectorStateMaterial[CurrentCableState];
                _point.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
            }

            UnityEvent _event = GameManager.Instance.QuestsToComplete[this];
            _event.Invoke();
            GameManager.Instance.QuestsToComplete.Remove(this);
            _questCompleted = true;
        }
    }

    public void LoadNewScene(string _sceneName)
    {
        GameManager _gm = GameManager.Instance;
        StartCoroutine(_gm.LoadScene(_sceneName, 3));
    }
}
