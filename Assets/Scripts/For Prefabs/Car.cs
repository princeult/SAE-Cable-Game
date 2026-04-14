using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [DoNotSerialize] public Vector2 moveDirection;
    public Transform CableSpawnPoint;

    [SerializeField, Range(1, 50)] private float _movePower = 1;
    [SerializeField, Range(1, 50)] private float _maxSpeed = 1;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody[] rbPause; // CAB OBJECT LAST

    
    
    //Save Velocity for pasuing.
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

    private void TogglePaused(bool _paused)
    {
        if (_paused)
        {
            // Save Velocity
            _rbLinearVelocity = rb.linearVelocity;
            _rbAngularVelocity = rb.angularVelocity;

            foreach(Rigidbody _rb in rbPause)
            {//for all rigidboys in prefab
                //Clear Velocity
                _rb.linearVelocity = new(0,0,0);
                _rb.angularVelocity = new(0,0,0);

                //Freeze rigidboy
                _rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        else
        {
            for(int i = 0; i < rbPause.Length; i++)
            {//for all rigidboys in prefab
                //Clear Constraints
                if(i == rbPause.Length - 1)
                {//just for last in array this is for CAB
                    rbPause[i].constraints = RigidbodyConstraints.None;
                    rbPause[i].constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    rbPause[i].constraints = RigidbodyConstraints.None;
                }
                
            }

            //Reload Velocity
            //rb.linearVelocity = _rbLinearVelocity;
            //rb.angularVelocity = _rbAngularVelocity;
        }
    }
}
