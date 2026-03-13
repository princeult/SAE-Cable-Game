using UnityEngine;

public class Controls : MonoBehaviour
{
    private ICableInteract _carControl;
    ICableInteract.CurrentCablePoint currentCablePoint;
    private bool _interactOnce = true; //For only interacting once per button press
    private float _horizontalInput;
    private float _verticalInput;

    // Update is called once per frame
    private void Update()
    {
        Inputs();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _carControl = new ControlCar();
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
            currentCablePoint = _carControl.PlaceCable(currentCablePoint);
            Debug.Log(currentCablePoint);
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
