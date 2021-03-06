using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController, ILockOnAble {
    override public void Start() {
        base.Start();
    }

    void Update() {

    }
    public Vector3 GetMiddle() {
        return transform.position;
    }
}
