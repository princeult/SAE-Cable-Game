using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Controls.UiEvents Unpause;
    public void UnPause()
    {
        Debug.Log("sad");
        Unpause?.Invoke(false);
    }
}
