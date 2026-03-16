using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class AiManager : MonoBehaviour
{
    [NonSerialized] public static AiManager Instance;
    [NonSerialized] public ObjectPool<AIenemy> AiEnemyPool;

    [SerializeField] private AiEnemyPool _aiEnemyPoolRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Awake()
    {//Create singleton for gamemanager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        _aiEnemyPoolRef.InitAiPool();
        AiEnemyPool = _aiEnemyPoolRef.GetPool();
        
    }
}
