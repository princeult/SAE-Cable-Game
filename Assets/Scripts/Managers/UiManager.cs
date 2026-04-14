using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [DoNotSerialize] public static UiManager Instance;
    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {// make singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        Controls.PauseEvent += TogglePauseMenu;
    }
    private void OnDisable()
    {
        Controls.PauseEvent -= TogglePauseMenu;
    }

    public void UnpauseButton()
    {
        Controls.Instance.TogglePause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void TogglePauseMenu(bool _paused) 
    {// Toggle enabled of UI based on paused bool in Game Manager
        pauseMenu.SetActive(_paused);
    }
}
