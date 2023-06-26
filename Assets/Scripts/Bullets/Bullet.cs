using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    public Vector3 Direction;

    public enum BulletTypes
    {
        NormalBullet,
        PlayerBullet
    }
    protected void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
         StartCoroutine(CheckIsInTheBoundOfTheScreen());       
    }
    IEnumerator CheckIsInTheBoundOfTheScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos)) gameObject.SetActive(false);
        }
    }
}
