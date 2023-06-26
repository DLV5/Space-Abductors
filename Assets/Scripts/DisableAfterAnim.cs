using UnityEngine;

public class DisableAfterAnim : MonoBehaviour // I should rename this class...
{
    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
