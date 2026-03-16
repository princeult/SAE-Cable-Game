using UnityEngine;

public interface IAIInteract
{
    public enum EnemyState {none, dead, wander, run}

    public void UpdateAiState(AIenemy enemy, EnemyState enemyState);
}
