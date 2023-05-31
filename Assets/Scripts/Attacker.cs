using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public int damage;
    public int cooldown;
    public float spreadAngle;

    [SerializeField]
    private string _objectTag;

    protected static GameObjectsPool gameObjectsPool;
    protected bool _canShoot = true;

    protected void InstantiateObjectPool(string objectTag)
    {
        if (gameObjectsPool == null)
            gameObjectsPool = GameManager.Instance.GetGameObjectsPool(objectTag);
    }

    protected void Start()
    {
        InstantiateObjectPool(_objectTag);
    }

    public void Shoot()
    {
        foreach (var obj in gameObjectsPool.pool)
        {
            if (!obj.activeSelf)
            {

                obj.SetActive(true);
                obj.transform.position = transform.position;
                Quaternion spreadRotation = Quaternion.Euler(0f, 0f, Random.Range(-spreadAngle / 2, spreadAngle / 2));
                var target = (spreadRotation * (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                obj.GetComponent<Bullet>().direction = target.normalized;

                break;
            }
        }
        _canShoot = false;
        StartCoroutine(EnterCooldown());
    }

    private IEnumerator EnterCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
}
