using UnityEngine;

public class CableConnecter : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private ParticleSystem _particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(startPoint == null) Debug.Log("Start Point Missing Did you Forget to set one?");
        if(endPoint == null) Debug.Log("End Point Missing Did you Forget to set one?");
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        Vector3 _midpoint = Vector3.Lerp(startPoint.position, endPoint.position, 0.5f);

        transform.LookAt(startPoint);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _distance);
        transform.position = _midpoint;
    }
}
