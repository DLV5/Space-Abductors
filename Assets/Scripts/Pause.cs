using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance { get; private set; }

    private Pause()
    {
        Instance = this;
    }

    public void EnterPause()
    {
        Time.timeScale = 0;
    }

    public void ExitPause()
    {
        Time.timeScale = 1.0f;
    }
}
