using UnityEngine;

public class CableConnecter : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private ParticleSystem _particleSystem;

    void Start() //Checks for inspector
    {
        if(startPoint == null) Debug.Log("Start Point Missing Did you Forget to set one?");
        if(endPoint == null) Debug.Log("End Point Missing Did you Forget to set one?");
    }

    // Update is called once per frame
    void Update()   // Set Z scale as distance between the points, set pos as midpoint, look at start point 
    {
        float _distance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        Vector3 _midpoint = Vector3.Lerp(startPoint.position, endPoint.position, 0.5f);

        transform.LookAt(startPoint);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _distance);
        transform.position = _midpoint;
    }

    private void OnCollisionEnter(Collision _collision) // if in correct state kill(Release) enemy
    {
        if (_collision.gameObject.CompareTag("Enemy") && GameManager.Instance.CableInstance.CurrentCurrentState == Cable.CableState.electrified)
        {
            AIenemy _enemy = _collision.gameObject.GetComponentInParent<AIenemy>();
            AiManager.Instance.AiEnemyPool.Release(_enemy);
        }
    }
}
