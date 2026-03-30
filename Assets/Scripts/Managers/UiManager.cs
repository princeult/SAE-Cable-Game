using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        Controls.PauseEvent += TogglePauseMenu;
    }
    private void OnDisable()
    {
        
    }

    private void TogglePauseMenu(bool _paused)
    {
        pauseMenu.SetActive(_paused);
    }
}
