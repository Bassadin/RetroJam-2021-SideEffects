using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : EnemyController
{
    public Animator turretAnimator;

    private Transform lookAtTarget;

    public Transform towerRotationPartTransform;
    private bool battleMode = false;
    private float shootingTimer = 0;

    public float shootingDelayTime = 3f;
    private float shootingDelayTimer = 0f;

    private void Update()
    {
        if (battleMode)
        {
            Vector3 lookPos = lookAtTarget.position - transform.position;
            lookPos.y = 0;
            Quaternion lookAtRotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * 5);

            shootingDelayTimer += Time.deltaTime;
            shootingTimer += Time.deltaTime;

            if (shootingTimer >= currentWeapon.reloadTime && shootingDelayTimer >= shootingDelayTime)
            {
                ShootWeapon();
                shootingTimer = 0;
            }
        }
        else
        {
            shootingTimer = 0;
            shootingDelayTimer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            setBattleMode(true);
            lookAtTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            setBattleMode(false);
            lookAtTarget = null;
        }
    }

    private void setBattleMode(bool battleModeBoolean)
    {
        turretAnimator.SetBool("BattleMode", battleModeBoolean);
        battleMode = battleModeBoolean;

        if (battleMode)
        {
            shootingTimer = currentWeapon.reloadTime;
        }
    }
}
