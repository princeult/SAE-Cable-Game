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
        if (moveDirection.magnitude != 0) // MoveDirection gets set by Control Car
        {
            if (Math.Abs(rb.linearVelocity.magnitude) > _maxSpeed) // if going to fast dont add more speed
            {
                moveDirection = new(0,0);
            }
            rb.AddForce(moveDirection.x * _movePower, 0,  moveDirection.y * _movePower);
        }
    }
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
