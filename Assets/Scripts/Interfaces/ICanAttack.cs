using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanAttack {
    void AttackWithWeapon();
    IEnumerator StartReload();
}
