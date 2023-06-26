using System.Collections;
using UnityEngine;

public class HealingMovingEnemy : MovingEnemy
{
    private EnemyAttacker[] _targets = new EnemyAttacker[]{};

    protected override void Awake()
    {
        base.Awake();
        _targets = GameObject.FindObjectsOfType<EnemyAttacker>(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable(); 

    }
        protected override IEnumerator ShootAccordingToFireRate()
    {
        while (true)
        {
            StartCoroutine(ChooseRandomEnemy());
            yield return new WaitForSeconds(1 / _fireRate);
            Shoot();
        }
    }
    protected override void Shoot()
    {
        GameObject obj = gameObjectsPool.GetPooledObjectByTag("HealingBullet");
        obj.GetComponent<HomingBullet>().Target = _target;
        obj.transform.position = transform.position;
    }
    private IEnumerator ChooseRandomEnemy()
    {
        int rand = UnityEngine.Random.Range(0, _targets.Length);
        while (_targets[rand].CompareTag("HealingEnemy"))
        {
            yield return new WaitForSeconds(.1f);
            rand = UnityEngine.Random.Range(0, _targets.Length);
            _target = _targets[rand].gameObject;
        }
    }
}

