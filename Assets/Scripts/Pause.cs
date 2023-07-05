using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
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
