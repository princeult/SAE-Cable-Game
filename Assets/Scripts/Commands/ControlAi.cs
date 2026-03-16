using UnityEngine;
using UnityEngine.AI;

public class ControlAi : IAIInteract
{
    public void UpdateAiState(AIenemy enemy, IAIInteract.EnemyState enemyState)
    {
        switch (enemyState)
        {
            case IAIInteract.EnemyState.dead:

                SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectKey.good);
                AiManager.Instance.AiEnemyPool.Release(enemy);
                break;


            case IAIInteract.EnemyState.wander: 
                
                Vector3 _wanderPostion = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                enemy.Agent.speed = 3f;
                enemy.CurrentEnemyState = enemyState;
                //enemy.StartGoToLocation(_wanderPostion); while loop not working


                break;
        }
    }
}
