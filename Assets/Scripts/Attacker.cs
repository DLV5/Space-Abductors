using System.Collections;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [Tooltip("Position of muzzle, it should be child of that gameobject")]
    protected Transform _firePoint;

    [SerializeField] protected float _fireRate;
    public abstract float FireRate { get;  set; }

    protected abstract void Fire();

    protected abstract void SetFirePoint();

}
