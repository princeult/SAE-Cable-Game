using System;
using UnityEngine;
using UnityEngine.Pool;

public class AiEnemyPool : MonoBehaviour
{
    [SerializeField] private AIenemy _enemyPF;
    private ObjectPool<AIenemy> enemies;
    private readonly IAIInteract _enemyControl = new ControlAi();
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
        _enemy.gameObject.SetActive(true);
        //_enemyControl.UpdateAiState(_enemy, IAIInteract.EnemyState.wander); while loop not working
    }
    private AIenemy CreateItem()
    {
        AIenemy _enemy = AIenemy.Instantiate(_enemyPF);
        _enemy.gameObject.SetActive(false);
        return _enemy;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
