using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultManual : DefaultGun
{
    InputPackage fireInputPackage;
    InputPackage reloadInputPackage;
    public override void OnEquip()
    {
        base.OnEquip();
        InputPackage.InputPackageHandler(fireInputPackage, inputHandler.OnLeftClickDownPackages, true);
        InputPackage.InputPackageHandler(reloadInputPackage, inputHandler.OnRDownPackages, true);
    }

    public override void OnDequip()
    {
        base.OnDequip();
        InputPackage.InputPackageHandler(fireInputPackage, inputHandler.OnLeftClickDownPackages, false);
        InputPackage.InputPackageHandler(reloadInputPackage, inputHandler.OnRDownPackages, false);
    }

    void Start()
    {
        // Set defaults for a default manual gun

        maxAmmo = 6;
        currentAmmo = maxAmmo;
        canReload = true;
        timeTilCanFire = fireRate;

        // Set InputPackages

        fireInputPackage = new InputPackage(Fire, 2);
        reloadInputPackage = new InputPackage(Reload, 1);
    }

    void Update()
    {

        if (!canFire)
        {
            timeTilCanFire -= Time.deltaTime;
        }
        if (timeTilCanFire < 0)
        {
            timeTilCanFire = fireRate;
            canFire = true;
        }
    }
    public override void Fire()
    {
        if ((currentAmmo == 0) && (canReload) && (canFire))
        {
            Reload();
        } else if (canFire)
        {
            currentAmmo--;
            spawnedProjectileHandle = Instantiate(projectile, bulletSpawnPoint);
            spawnedProjectileHandle.transform.SetParent(null);
            spawnedProjectileAmmoClass = spawnedProjectileHandle.GetComponent<Ammo>();
            if (spawnedProjectileAmmoClass)
            {
                spawnedProjectileAmmoClass.bulletDirection = gameObject.transform.forward;
                spawnedProjectileAmmoClass.bulletVelocity = bulletVelocity;
            }
            gunSoundSource.PlayOneShot(fire);
            canFire = false;
        }
    }

    public override void Reload()
    {
        currentAmmo = maxAmmo;
        canFire = false;
        timeTilCanFire = reloadTime;
        gunSoundSource.PlayOneShot(reload);
    }
}
