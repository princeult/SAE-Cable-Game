using System;
using UnityEngine;
using UnityEngine.Pool;

public class AiEnemyPool : MonoBehaviour
{
    [SerializeField] private AIenemy _enemyPF;
    private ObjectPool<AIenemy> enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitAiPool()
    {
        enemies = new ObjectPool<AIenemy>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 20

        );
    }
    public ObjectPool<AIenemy> GetPool()
    {
        return enemies;
    }

    public void OnDestroyItem(AIenemy _enemy)
    {
        Destroy(_enemy);
    }

    private void OnRelease(AIenemy _enemy)
    {
        if(_enemy.QuestParent != null) _enemy.QuestParent.CheckIfEnemiesAreAllDead(_enemy);
        _enemy.gameObject.SetActive(false);
    }

    private void OnGet(AIenemy _enemy)
    {
        //_bullet.gameObject.SetActive(true);
        _enemy.gameObject.SetActive(true);
    }
    private AIenemy CreateItem()
    {
        AIenemy enemy = AIenemy.Instantiate(_enemyPF);
        enemy.InitAi();
        enemy.gameObject.SetActive(false);
        return enemy;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
