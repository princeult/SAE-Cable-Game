using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Dictionary<Quests, UnityEvent> QuestsToComplete;
    public Car Car;
    public Cable Cable;

    public void SpawnCar(Car _car)
    {
        Vector3 _spawnPos = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
        Quaternion _spawnRot = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.rotation;
        Car = Instantiate(_car, _spawnPos, _spawnRot);
    }
    public void SpawnCable(Cable cable)
    {
        Vector3 _spawnPos = Car.CableSpawnPoint.position;
        Quaternion _spawnRot = Car.CableSpawnPoint.rotation;
        Cable = Instantiate(cable, _spawnPos, _spawnRot);
        Cable._inLevel = true;
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
        SpawnCar(Car);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "GlobalScene")
        {
            Debug.Log("New scene loaded " + scene.name);
            QuestsToComplete.Clear();
            GameObject[] _quests = GameObject.FindGameObjectsWithTag("Quest");
            foreach(GameObject item in _quests)
            {
                Quests _quest = item.GetComponent<Quests>();
                QuestsToComplete.Add(_quest, _quest.CompletionEvent);
            }
        }
    }
}


