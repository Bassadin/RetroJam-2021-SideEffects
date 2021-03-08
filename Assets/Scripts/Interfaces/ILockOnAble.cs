using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockOnAble
{
    Vector3 GetMiddle();

    void SetScreenPoint(Vector3 _screenPoint);
    Vector3 GetScreenPoint();

    GameObject GetGameObject();
}
