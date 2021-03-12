using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int maxAmmo;
    public float thrust;
    public float reloadTime;

    public int currentAmmo;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;

    void Awake() {
        currentAmmo = maxAmmo;
    }

    void Update() {
        
    }

    public void refillAmmo()
    {
        currentAmmo = maxAmmo;
    }
}
