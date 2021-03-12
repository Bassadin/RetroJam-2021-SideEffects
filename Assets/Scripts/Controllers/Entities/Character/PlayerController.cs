using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

    [SerializeField] private Camera firstPersonCamera;
    private bool reloading = false;

    override public void Start()
    {
        base.Start();
        updateAmmoInUIController();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("StartReload");
        }
    }

    public override void ShootWeapon()
    {
        if (reloading)
        {
            Debug.Log("Currently reloading");
            return;
        }

        if (currentWeapon.currentAmmo > 0)
        {
            currentWeapon.currentAmmo--;
            updateAmmoInUIController();
            GameObject projectileInstance = Instantiate(currentWeapon.projectilePrefab, currentWeapon.projectileSpawnPosition.position, firstPersonCamera.transform.rotation);
            ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();
            projectileController.transform.forward = firstPersonCamera.transform.forward;
            projectileController.rigidBody.AddForce(projectileController.transform.forward * currentWeapon.thrust, ForceMode.Impulse);
        }

    }

    public void fillAmmo()
    {
        currentWeapon.refillAmmo();
    }

    public void refillHP()
    {
        currentHealth = maxHealth;
    }

    public IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        reloading = false;
        updateAmmoInUIController();
    }
    private void updateAmmoInUIController()
    {
        UIMasterController.Instance.setAmmunitionAmount(currentWeapon.currentAmmo, currentWeapon.maxAmmo);
    }
}
