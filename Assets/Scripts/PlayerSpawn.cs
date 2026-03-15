using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameManager.Instance.InitScene(GameManager.Instance.Car, SceneManager.GetActiveScene().name, transform.position, transform.rotation);
    }


}
