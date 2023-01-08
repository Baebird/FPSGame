using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAutomatic : DefaultGun
{
    public override void OnEquip()
    {
        base.OnEquip();
        InputPackage.InputPackageHandler(new InputPackage(Fire, 2), inputHandler.OnLeftClickHoldPackages, true);
        InputPackage.InputPackageHandler(new InputPackage(ReloadR, 1), inputHandler.OnRDownPackages, true);
        InputPackage.InputPackageHandler(new InputPackage(ReloadLeftClick, 2), inputHandler.OnLeftClickDownPackages, true);
    }

    public override void OnDequip()
    {
        base.OnDequip();
        InputPackage.InputPackageHandler(new InputPackage(Fire, 2), inputHandler.OnLeftClickHoldPackages, false);
        InputPackage.InputPackageHandler(new InputPackage(ReloadR, 1), inputHandler.OnRDownPackages, false);
        InputPackage.InputPackageHandler(new InputPackage(ReloadLeftClick, 2), inputHandler.OnLeftClickDownPackages, false);
    }

    void Start()
    {
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        canReload = true;
        timeTilCanFire = fireRate;
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
        if (canFire && currentAmmo > 0)
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
        
    }

    public void ReloadR()
    {
        if (currentAmmo != 30)
        {
            currentAmmo = maxAmmo;
            canFire = false;
            timeTilCanFire = reloadTime;
            gunSoundSource.PlayOneShot(reload);
        }
    }

    public void ReloadLeftClick()
    {
        if (currentAmmo == 0)
        {
            ReloadR();
        }
    }
}
