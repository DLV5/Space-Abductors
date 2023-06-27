using System.Collections;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected float _fireRate;
    public abstract float FireRate { get;  set; }

    protected abstract void Fire();

}
