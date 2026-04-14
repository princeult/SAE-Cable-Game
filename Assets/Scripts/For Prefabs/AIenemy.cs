using System;
using UnityEngine;
using UnityEngine.AI;

public class AIenemy : MonoBehaviour
{
    
    //dependencies for prototyping
    [NonSerialized] public Quests QuestParent;
    public NavMeshAgent Agent;
    public IAIInteract.EnemyState CurrentEnemyState;
    [Range(0.2f, 5f)] public float WanderRadius = 3f;
    [Range(0.1f, 1f)] public float AcceptanceDistance = 0.3f;
    private readonly IAIInteract _selfControl = new ControlAi();
    bool _repeat = true;
    //---------------------------------
    
    
    
    [SerializeField] private Rigidbody rb;
    private Vector3 _rbLinearVelocity;
    private Vector3 _rbAngularVelocity;
    private void OnEnable()
    {
        Controls.PauseEvent += TogglePaused;
    }
    private void OnDisable()
    {
        Controls.PauseEvent -= TogglePaused;
    }

    private void TogglePaused(bool _paused)
    {
        if (_paused)
        {
            // Save Velocity
            _rbLinearVelocity = rb.linearVelocity;
            _rbAngularVelocity = rb.angularVelocity;

            //stop moving
            rb.linearVelocity = new(0,0,0);
            rb.angularVelocity = new(0,0,0);

            //Freeze Rigidboy
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
            //Clear Constraints 
            rb.constraints = RigidbodyConstraints.None;


            //Reload Velocity
            rb.linearVelocity = _rbLinearVelocity;
            rb.angularVelocity = _rbAngularVelocity;
        }
    }
}
