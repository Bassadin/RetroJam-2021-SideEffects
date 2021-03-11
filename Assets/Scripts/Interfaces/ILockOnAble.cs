using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockOnAble
{
    Vector3 GetMiddle();

    void SetScreenPoint(Vector3 _screenPoint);
    Vector2 GetScreenPoint();

    GameObject GetGameObject();
}
