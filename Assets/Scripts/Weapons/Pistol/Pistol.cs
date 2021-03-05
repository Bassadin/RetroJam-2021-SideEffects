using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    public int maxAmmo = 12;
    public float thrust = 100f;

    private int currentAmmo = 12;
    private bool shotFired = false;

    [SerializeField] private GameObject pistolProjectilePrefab;
    [SerializeField] private Camera firstPersonCamera;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ShootProjectile();
        }
    }
    void ShootProjectile () {
        Vector3 weaponPosition = gameObject.transform.position;
        GameObject projectile = Instantiate(pistolProjectilePrefab, weaponPosition, Quaternion.identity);
        projectile.transform.forward = firstPersonCamera.transform.forward;
        IProjectile projectileBehaviour = projectile.GetComponent<PistolProjectile>();
        Debug.Log(projectileBehaviour);
        projectileBehaviour.Shoot(thrust);

    }
}
