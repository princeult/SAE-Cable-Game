using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameManager.Instance.InitScene(GameManager.Instance.CarRefrence, SceneManager.GetActiveScene().name, transform.position, transform.rotation);
    }


}
