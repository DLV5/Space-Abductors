using System.Collections;
using UnityEngine;

public class ShootingToPlayerMovingEnemy : MovingEnemy
{
    protected override void Awake()
    {
        base.Awake();
        SetFirePoint();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootAccordingToFireRate());
    }

    protected virtual IEnumerator ShootAccordingToFireRate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / _fireRate);
            Fire();
        }
    }
}
