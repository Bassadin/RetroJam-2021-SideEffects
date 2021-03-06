using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController, ILockOnAble {

    private Vector3 screenPoint;


    override public void Start() {
        base.Start();
        SceneController.AddLockonableTarget(this);
    }

    public Vector3 GetMiddle() {
        return transform.position;
    }

    private void OnDestroy() {
        SceneController.RemoveLockonableTarget(this);
    }

    public void SetScreenPoint(Vector3 _screenPoint) {
        screenPoint = _screenPoint;
    }

    public Vector2 GetScreenPoint() {
        return screenPoint;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public override void ShootWeapon() {
        Debug.Log("SHOOT");
        GameObject projectileInstance = Instantiate(currentWeapon.projectilePrefab, currentWeapon.projectileSpawnPosition.position, transform.rotation);
        ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();
        projectileController.transform.forward = transform.forward;
        projectileController.rigidBody.AddForce(projectileController.transform.forward * currentWeapon.thrust, ForceMode.Impulse);
    }
}
