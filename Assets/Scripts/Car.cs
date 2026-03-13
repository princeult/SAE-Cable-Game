using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float _movePower = 1;
    [SerializeField, Range(1, 50)] private float _maxSpeed = 1;
    [SerializeField] private Rigidbody rb;
    [DoNotSerialize] public Vector2 moveDirection;
    public Transform CableSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        if (moveDirection.magnitude != 0)
        {
            if (Math.Abs(rb.linearVelocity.magnitude) > _maxSpeed) 
            {
                moveDirection = new(0,0);
            }
            rb.AddForce(moveDirection.x * _movePower, 0,  moveDirection.y * _movePower);
        }
    }
}
