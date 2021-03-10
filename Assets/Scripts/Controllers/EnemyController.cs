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
        Debug.Log("Cube Object Destroyed");
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
}
