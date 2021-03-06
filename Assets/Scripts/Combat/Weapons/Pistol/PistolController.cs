using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour {
    public int maxAmmo = 12;
    public float thrust = 100f;
    public float reloadTime = 2f;

    private bool reloading = false;
    private int currentAmmo = 12;

    [SerializeField] private GameObject pistolProjectilePrefab;
    [SerializeField] private Camera firstPersonCamera;

    public Transform projectileSpawnPosition;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if(currentAmmo > 0 && !reloading)
                ShootProjectile();
            else {
                //display reload tip
            }
        }
        else if(Input.GetKeyDown(KeyCode.R)) {
            if (!reloading)
                StartCoroutine(StartReload());
            else
                Debug.Log("ALREADY RELOADING");
        }
    }
    void ShootProjectile () {
        currentAmmo--;
        GameObject projectile = Instantiate(pistolProjectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
        projectile.transform.forward = firstPersonCamera.transform.forward;
        IProjectile projectileBehaviour = projectile.GetComponent<PistolProjectile>();

        projectileBehaviour.Shoot(thrust);
    }

    IEnumerator StartReload() {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        Reload();
    }

    void Reload() {
        currentAmmo = maxAmmo;
        reloading = false;
    }
}
