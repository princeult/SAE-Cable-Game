using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIenemy : MonoBehaviour
{
    [NonSerialized] public Quests QuestParent;
    public NavMeshAgent Agent;
    public IAIInteract.EnemyState CurrentEnemyState;
    [Range(0.2f, 5f)] public float WanderRadius = 3f;
    [Range(0.1f, 1f)] public float AcceptanceDistance = 0.3f;
    [SerializeField] private Rigidbody _rb;
    private readonly IAIInteract _selfControl = new ControlAi();
    bool _repeat = true;


    // private IEnumerator GoToLocation(Vector3 _location) while loop not working
    // {
    //     _repeat = true;
    //    Agent.SetDestination(_location);  
    //    while(1 == 1)
    //     {
    //         if (Vector3.Distance(transform.position, _location) < AcceptanceDistance)
    //         {
    //             //_selfControl.UpdateAiState(this, IAIInteract.EnemyState.wander);
    //             //_repeat = false;
    //         }
    //     yield return new WaitForEndOfFrame();
    //     }
    // }
    // public void StartGoToLocation(Vector3 _location)
    // {
    //     StartCoroutine(GoToLocation(_location));
    // }
}
