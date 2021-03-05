using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakesDamage
{
    void TakeDamage(float damage);

    void CheckDeath();
}
