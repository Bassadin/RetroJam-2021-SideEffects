using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController, ILockOnAble {
    override public void Start() {
        base.Start();
        SceneController.AddLockonableTarget(this);
    }

    void Update() {

    }
    public Vector3 GetMiddle() {
        return transform.position;
    }

    private void OnDestroy() {
        SceneController.RemoveLockonableTarget(this);
    }
}
