using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIenemy : MonoBehaviour
{
    [NonSerialized] public Quests QuestParent;
    public NavMeshAgent Agent;
    [SerializeField] private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitAi()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
