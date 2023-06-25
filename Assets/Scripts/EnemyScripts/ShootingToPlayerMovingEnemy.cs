using System.Collections;
using UnityEngine;

public class ShootingToPlayerMovingEnemy : MovingEnemy
{
    private void Awake()
    {
        _minHeight = Camera.main.ScreenToWorldPoint(Vector2.zero);
        _maxHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootAccordingToFireRate());
        StartingFunction();
    }

    protected override IEnumerator ShootAccordingToFireRate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
