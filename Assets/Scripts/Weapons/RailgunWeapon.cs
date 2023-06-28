using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunWeapon : Weapon
{
    [SerializeField] private RailgunRay _ray;
    [SerializeField] private Animator _rayAnimator;
    [SerializeField] private GameObject _rayObject;

    private void Start()
    {
        InputHandler.PressingShootButton += InputHandler_OnPressingShootButton;
        InputHandler.ReleasingShootButton += InputHandler_OnReleasingShootButton;
    }


    private void InputHandler_OnPressingShootButton()
    {
        Fire();
    }

    private void InputHandler_OnReleasingShootButton()
    {
        DisableRailgun();
    }

    protected override void Fire()
    {
        EnableRailgun();
        _ray.CastRayThroughMouseBosition();
    }

    private void EnableRailgun()
    {
        _rayObject.SetActive(true);
        _rayAnimator.SetTrigger("IsCharged");
    }
    private void DisableRailgun()
    {
        _rayAnimator.SetTrigger("IsReleased");
    }
}
