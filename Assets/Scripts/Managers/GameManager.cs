using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DoNotSerialize] public static GameManager Instance;
    [NonSerialized] public Car CarInstance;
    [NonSerialized] public Cable CableInstance;
    [DoNotSerialize] public Dictionary<Cable.CableState, Color32> ParticleSystemColour = new()
    {
        {Cable.CableState.none, new Color32(0, 0, 0, 0)},
        {Cable.CableState.electrified, new Color32(8, 191, 255, 255)}
    };
    
    public enum CarSpawnType {none, levelStart, atPlayerLocation};
    public Dictionary<Quests, UnityEvent> QuestsToComplete = new();
    public Car CarRefrence;
    public Cable CableRefrence;
    

    [SerializeField] private string _startSceneName;
    



    public void SpawnCar(Car _car, CarSpawnType carSpawnType, Vector3 _spawnPos = new Vector3(), Quaternion _spawnRot = new Quaternion())
    {
        switch (carSpawnType)
        {
            case CarSpawnType.levelStart:
                CarInstance = Instantiate(_car, _spawnPos, _spawnRot);
            break;
            case CarSpawnType.atPlayerLocation:
                _spawnPos = CarInstance.transform.position + new Vector3(0f,0.25f, 0f);
                _spawnRot = CarInstance.transform.rotation;
                CarInstance = Instantiate(_car, _spawnPos, _spawnRot);
            break;
        }

        
    }
    public void SpawnCable(Cable cable)
    {
        Vector3 _spawnPos = CarInstance.CableSpawnPoint.position;
        Quaternion _spawnRot = CarInstance.CableSpawnPoint.rotation;
        CableInstance = Instantiate(cable, _spawnPos, _spawnRot);
        CableInstance._inLevel = true;
    }
    private void GetAllQuests(string _scene)
    {
        if(_scene != "GlobalScene")
        {
            GameObject[] _quests = GameObject.FindGameObjectsWithTag("Quest");
            if(QuestsToComplete != null)
            {
                QuestsToComplete.Clear();
                
            }
            if(_quests != null)
            {
                foreach(GameObject item in _quests)
                {
                    Quests _quest = item.GetComponent<Quests>();
                    QuestsToComplete.Add(_quest, _quest.CompletionEvent);
                }
            }

        }
    }
    public IEnumerator LoadScene(string _scene, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void InitScene(Car _car, string _scene, Vector3 _carSpawnPos, Quaternion _carSpawnRot)
    {
        Controls.Instance.currentCablePoint = ICableInteract.CurrentCablePoint.none; 
        SpawnCar(_car, CarSpawnType.levelStart, _carSpawnPos, _carSpawnRot);
        GetAllQuests(_scene);
        Debug.Log("New Scene " + _scene + " Loaded");
    }
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {//Create singleton for gamemanager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        StartCoroutine(LoadScene(_startSceneName, 0));
    }
}


