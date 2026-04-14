using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    [DoNotSerialize] public static Controls Instance;
    public delegate void UiEvents(bool _paused);
    public static event UiEvents PauseEvent;
    public ICableInteract.CurrentCablePoint currentCablePoint;

    private readonly ICableInteract _carControl = new ControlCar(); //Commands
    private readonly ICableInteract _cableControl = new ControlCable();
    private bool _interactOncePlace = true; //For only interacting once per button press
    private bool _interactOncePause = true; //For only interacting once per button press
    private bool _paused = false; //To know if game is currently pasued
    private float _horizontalInput;
    private float _verticalInput;

    // Update is called once per frame
    private void Update()
    {
        Inputs();
    }

private void Awake()
    {//Create singleton for Controls (for Later use not currently implmented)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Inputs()
    {//For Interact if needed later, currently not implmented
        // if(Input.GetKeyDown("e") && _interactOnce) //used for an iteract system if i had time
        // {
        //     _interactOnce = false;
        // }
        // else if(!_interactOnce && (Input.GetAxis("Fire1") == 0))
        // {
        //     _interactOnce = true;
        // }

        if((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("f"))  && _interactOncePlace)
        {
            currentCablePoint = _cableControl.PlaceCable(currentCablePoint);
            _interactOncePlace = false;
        }
        else if(!_interactOncePlace)
        {
            _interactOncePlace = true;
        }

        if((Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 6")) && _interactOncePause)
        {
            TogglePause();
        }
        else if(!_interactOncePause)
        {
            _interactOncePause = true;
        }
        
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if((_horizontalInput != 0 || _verticalInput != 0) && !_paused && !GameManager.Instance.Loading)
        {
            _carControl.MoveControl(new(_horizontalInput, _verticalInput)); 
        }
    }

    public void TogglePause()
    {
        _paused = !_paused;
        PauseEvent?.Invoke(_paused);
        _interactOncePause = false;
    }
}
