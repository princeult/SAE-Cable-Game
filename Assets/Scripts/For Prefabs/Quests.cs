using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Quests : MonoBehaviour
{
    public UnityEvent CompletionEvent;
    public enum QuestType {none, cable, enemies};
    public QuestType CurrentQuestType;
    public Cable.CableState CurrentCableState;

    [NonSerialized] public readonly ICableInteract CableControl = new ControlCable();
    
    [SerializeField, Range(1,20)] private int enemieCount;
    [SerializeField, Range(0.001f, 5)] private float _spawnRange = 2;
    [SerializeField] private GameObject[] _points; // the colliders for cable type quest

    private bool _questCompleted; 
    private List<GameObject> _aIenemies = new (); // this is for checking if all enemies are dead before completing the quest

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (CurrentQuestType) // Init based on what type the quest should be
        {
            case QuestType.cable: //Set partcles to correct color based on the type
                Dictionary<Cable.CableState, Color32> _psColour = GameManager.Instance.ParticleSystemColour;
                foreach(GameObject _point in _points)
                {
                    var _psMain = _point.GetComponentInChildren<ParticleSystem>(true).main;
                    _psMain.startColor =  new ParticleSystem.MinMaxGradient(_psColour[CurrentCableState]);
                }
                break;


            case QuestType.enemies: //spawn enemies
                for(int i = 0; i < enemieCount; i++)
                    {
                        GameObject _enemy = AiManager.Instance.AiEnemyPool.Get().gameObject;
                        
                        Vector3 _pos = new (UnityEngine.Random.Range(-_spawnRange, _spawnRange), 0.5f ,UnityEngine.Random.Range(-_spawnRange, _spawnRange));
                        _pos = _pos + transform.position;
                        _enemy.transform.position = _pos;
                        _enemy.GetComponent<AIenemy>().QuestParent = this;
                        _aIenemies.Add(_enemy);               
                    }
                break;
        }
        
        
    }

    // Update is called once per frame
    public void CompleteQuest()
    {
        if(!_questCompleted)
        {
            switch (CurrentQuestType)
            {
                case QuestType.cable:
                    foreach(GameObject _point in _points)
                    {
                        _point.GetComponent<MeshRenderer>().material = GameManager.Instance.CableInstance.ConnectorStateMaterial[CurrentCableState];
                        _point.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
                    }
                break;
            }
            

            UnityEvent _event = GameManager.Instance.QuestsToComplete[this];
            _event.Invoke();
            GameManager.Instance.QuestsToComplete.Remove(this);
            _questCompleted = true;
        }
    }
    public void CheckIfEnemiesAreAllDead(AIenemy _enemy)
    {
        _aIenemies.Remove(_enemy.gameObject);
        if(_aIenemies.Count == 0)
        {
            CompleteQuest();
        }
    }

    public void LoadNewScene(string _sceneName)// this is for when the quest wants to load a new scene and a way to do that from the inspector
    {
        GameManager _gm = GameManager.Instance;
        StartCoroutine(_gm.LoadScene(_sceneName, 3));
    }
}
