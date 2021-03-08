using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Animator turretAnimator;

    private Transform lookAtTarget;

    public Transform towerRotationPartTransform;
    private bool battleMode = false;

    private void Update()
    {
        if (battleMode)
        {
            Vector3 lookPos = lookAtTarget.position - transform.position;
            lookPos.y = 0;
            Quaternion lookAtRotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * 3f);
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
    }
}
