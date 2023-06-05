using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    public float Speed { get => speed; set => speed = value; }

    public Vector3 direction;

    public enum BulletTypes
    {
        NormalBullet,
        PlayerBullet
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
