using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneController
{
    private static List<ILockOnAble> lockonableTargets = new List<ILockOnAble>();

    static public List<ILockOnAble> getLockonableTargets () {
        return lockonableTargets;
    }
    static public void AddLockonableTarget(ILockOnAble target) {
        lockonableTargets.Add(target);
    }
    static public void RemoveLockonableTarget(ILockOnAble target) {
        lockonableTargets.Remove(target);
    }
}
