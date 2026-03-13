using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
    }
}


