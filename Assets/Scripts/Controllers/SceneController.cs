using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneController
{
    public static List<ILockOnAble> lockonableTargets = new List<ILockOnAble>();

    static public void AddLockonableTarget(ILockOnAble target) {
        lockonableTargets.Add(target);
    }
    static public void RemoveLockonableTarget(ILockOnAble target) {
        lockonableTargets.Remove(target);
    }
}
