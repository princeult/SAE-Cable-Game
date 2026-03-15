using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DoNotSerialize] public static GameManager Instance;
    public enum CarSpawnType {none, levelStart, atPlayerLocation}
    public Dictionary<Quests, UnityEvent> QuestsToComplete;
    public Car Car;
    public Cable Cable;
    [SerializeField] private string _startSceneName;
    



    public void SpawnCar(Car _car, CarSpawnType carSpawnType, Vector3 _spawnPos = new Vector3(), Quaternion _spawnRot = new Quaternion())
    {
        switch (carSpawnType)
        {
            case CarSpawnType.levelStart:
                Car = Instantiate(_car, _spawnPos, _spawnRot);
            break;
            case CarSpawnType.atPlayerLocation:
                _spawnPos = Car.transform.position + new Vector3(0f,0.25f, 0f);
                _spawnRot = Car.transform.rotation;
                Car = Instantiate(_car, _spawnPos, _spawnRot);
            break;
        }

        
    }
    public void SpawnCable(Cable cable)
    {
        Vector3 _spawnPos = Car.CableSpawnPoint.position;
        Quaternion _spawnRot = Car.CableSpawnPoint.rotation;
        Cable = Instantiate(cable, _spawnPos, _spawnRot);
        Cable._inLevel = true;
    }
    private void GetAllQuests(string _scene)
    {
        if(_scene != "GlobalScene")
        {
            if(QuestsToComplete != null)
            {
                QuestsToComplete.Clear();
                GameObject[] _quests = GameObject.FindGameObjectsWithTag("Quest");
                
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
    }
    IEnumerator LoadScene(string _scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void InitScene(Car _car, string _scene, Vector3 _carSpawnPos, Quaternion _carSpawnRot)
    {
        GetAllQuests(_scene);
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
        StartCoroutine(LoadScene(_startSceneName));
    }
}


