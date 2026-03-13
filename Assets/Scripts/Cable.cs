using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public GameObject _startPoint;
    public GameObject _endPoint;
    [DoNotSerialize] public GameObject _followPoint;
    public BoxCollider connecterCollider;
    public bool _inLevel = false;

    private void FixedUpdate()
    {
        PointFollow();
    }

    private void PointFollow()
    {
        if(_followPoint != null)
        {
            _followPoint.transform.position = GameManager.Instance.Car.CableSpawnPoint.transform.position;
        }
    }

}
