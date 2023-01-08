using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Item
{
    public PlayerInputHandler inputHandler;
    public Transform bulletSpawnPoint;
    public GameObject projectile;
    protected GameObject spawnedProjectileHandle;
    protected Ammo spawnedProjectileAmmoClass;

    public AudioSource gunSoundSource;
    public AudioClip equip;
    public AudioClip dequip;
    public AudioClip fire;
    public AudioClip reload;

    public Material UnEquippedMat;
    public Material EquippedMat;

    public float bulletVelocity = 10.0f;
    public float damage = 1.0f;
    public float fireRate = 0.2f;
    public float reloadTime = 3.0f;
    public float equipTime = 3.0f;
    public float timeTilCanFire;
    protected int maxAmmo = 10;
    public int currentAmmo;
    protected bool canReload = true;
    protected bool canFire = true;

    virtual public void Reload() { }
    virtual public void Fire() { }

    public override void OnEquip()
    {
        base.OnEquip();

        canFire = false;
        timeTilCanFire = equipTime;
        inputHandler = GetComponentInParent<PlayerInputHandler>();

        Collider[] collidersOnSelf = GetComponents<Collider>();
        Collider[] collidersOnChildren = GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersOnSelf)
        {
            collider.enabled = false;
        }
        foreach (Collider collider in collidersOnChildren)
        {
            collider.enabled = false;
        }

        Rigidbody itemRigidbodyOnSelf = GetComponent<Rigidbody>();
        Rigidbody itemRigidbodyOnChildren = GetComponentInChildren<Rigidbody>();
        if (itemRigidbodyOnSelf)
        {
            itemRigidbodyOnSelf.useGravity = false;
            itemRigidbodyOnSelf.velocity = Vector3.zero;
        }
        if (itemRigidbodyOnChildren)
        {
            itemRigidbodyOnChildren.useGravity = false;
            itemRigidbodyOnChildren.velocity = Vector3.zero;
        }

        Renderer rendererOnSelf = gameObject.GetComponent<Renderer>();
        Renderer rendererOnChildren = gameObject.GetComponentInChildren<Renderer>();

        if (rendererOnSelf)
        {
            rendererOnSelf.material = EquippedMat;
        } else if (rendererOnChildren)
        {
            rendererOnChildren.material = EquippedMat;
        }
    }

    public override void OnDequip()
    {
        base.OnDequip();

        Collider[] collidersOnSelf = GetComponents<Collider>();
        Collider[] collidersOnChildren = GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersOnSelf)
        {
            collider.enabled = true;
        }
        foreach (Collider collider in collidersOnChildren)
        {
            collider.enabled = true;
        }

        Rigidbody itemRigidbodyOnSelf = GetComponent<Rigidbody>();
        Rigidbody itemRigidbodyOnChildren = GetComponentInChildren<Rigidbody>();
        if (itemRigidbodyOnSelf)
        {
            itemRigidbodyOnSelf.useGravity = true;
            itemRigidbodyOnSelf.velocity = Vector3.zero;
        }
        if (itemRigidbodyOnChildren)
        {
            itemRigidbodyOnChildren.useGravity = true;
            itemRigidbodyOnChildren.velocity = Vector3.zero;
        }

        Renderer rendererOnSelf = gameObject.GetComponent<Renderer>();
        Renderer rendererOnChildren = gameObject.GetComponentInChildren<Renderer>();

        if (rendererOnSelf)
        {
            rendererOnSelf.material = UnEquippedMat;
        }
        else if (rendererOnChildren)
        {
            rendererOnChildren.material = UnEquippedMat;
        }
    }
}
