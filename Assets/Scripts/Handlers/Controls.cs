using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    [DoNotSerialize] public static Controls Instance;

    public ICableInteract.CurrentCablePoint currentCablePoint;

    private readonly ICableInteract _carControl = new ControlCar(); //Commands
    private readonly ICableInteract _cableControl = new ControlCable();
    private bool _interactOnce = true; //For only interacting once per button press
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
    {//Get Movement Axis and Interact Axis for method
        if(Input.GetKeyDown("e") && _interactOnce)
        {
            _interactOnce = false;
        }
        else if(!_interactOnce && (Input.GetAxis("Fire1") == 0))
        {
            _interactOnce = true;
        }

        if(Input.GetKeyDown("f") && _interactOnce)
        {
            currentCablePoint = _cableControl.PlaceCable(currentCablePoint);
            _interactOnce = false;
        }
        else if(!_interactOnce)
        {
            _interactOnce = true;
        }
        
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        if(_horizontalInput != 0 || _verticalInput != 0)
        {
            _carControl.MoveControl(new(_horizontalInput, _verticalInput)); 
        }
    }
}
