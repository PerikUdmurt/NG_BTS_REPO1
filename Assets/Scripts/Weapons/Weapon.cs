using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class Weapon : MonoBehaviour
{
    public Joystick shootJoystick;

    [SerializeField] private WeaponStats _weaponStats;
    [Header ("WeaponOptions")]
    public GameObject bulletPrefab;
    public float damage;
    public float shootCoolDown;
    public int maxAmmo;
    public float reloadDuration;
    public int numOfBullets;
    public float bulletSpeed;
    public float spread;
    public float knockback;
    public int bounce;
    public float rotationSpeed;
    public float bulletDestroyTime;
    public bool piercing;
    public Transform source;
    public Transform weaponVisual;
    public List<Effect> effects;
    private int ammo;
    private bool CanShoot;
    private Vector3 targetPosition;
    [HideInInspector]public GameObject inputTarget;
    private bool playerMode;
    private bool attackTrigger;
    public Transform playerTransform;
    

    private void Awake()
    {
        bulletPrefab = _weaponStats.bulletPrefab;
        damage = _weaponStats.damage;
        shootCoolDown = _weaponStats.shootCoolDown;
        maxAmmo = _weaponStats.maxAmmo;
        reloadDuration = _weaponStats.reloadDuration;
        numOfBullets = _weaponStats.numOfBullets;
        bulletSpeed = _weaponStats.bulletSpeed;
        spread = _weaponStats.spread;
        knockback = _weaponStats.knockback;
        bounce = _weaponStats.bounce;
        rotationSpeed = _weaponStats.rotationSpeed;
        bulletDestroyTime = _weaponStats.bulletDestroyTime;
        piercing = _weaponStats.piercing;
        effects = _weaponStats.effects;
        ammo = maxAmmo;
        CanShoot = true;
        playerMode = _weaponStats.playerMode;
    }
    private void Update()
    {   
        WeaponRotation();
        
        targetPosition = PlayerModeCheck(playerMode);
        if (playerMode)
        {
            if (Input.GetButton("Fire1")) Shoot();
        }
        else {if (attackTrigger) Shoot(); }
    }

    private void Shoot()
    {
            if (ammo <= 0||Input.GetButtonDown("Reload"))
            {
                StartCoroutine(Reload(reloadDuration));
            }
            else if (ammo > 0 && CanShoot)
            {
            Vector2 direction;
                 direction = targetPosition - source.position;
  
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                for (int n = numOfBullets; n > 0; n--)
                {
                    Quaternion shootRotation = Quaternion.AngleAxis(angle + Random.Range(-spread, spread), Vector3.forward);
                    GameObject bullet = Instantiate(bulletPrefab, source.position, shootRotation);
                    Bullet bulletOptions = bullet.GetComponent<Bullet>();
                    bulletOptions.speed = bulletSpeed;
                    bulletOptions.damage = damage;
                    bulletOptions.piercing = piercing;
                    bulletOptions.destroyTime = bulletDestroyTime;
                    bulletOptions.angle = shootRotation;
                    bulletOptions.knockback = knockback;
                    bulletOptions.direction = targetPosition - source.position;
                    bulletOptions.bounce = bounce;
                }
                --ammo;
                StartCoroutine(CoolDownTimer(shootCoolDown));
            }
        
    }

    private void WeaponRotation() 
    {
        Vector2 direction;
        
        direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation;
        if (targetPosition.x >= playerTransform.position.x)
        { rotation = Quaternion.AngleAxis(angle, Vector3.forward); }
        else { rotation = Quaternion.AngleAxis(-angle+180f, Vector3.forward); }

        transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
    }
    public Vector2 GetWeaponRotation()
    {
        Vector2 direction = targetPosition - transform.position;
        return direction;
    }
    public void SetAttackTrigger(bool value)
    {
        attackTrigger = value;
    }
    private Vector3 PlayerModeCheck(bool isPlayerMode)
    {
        if (isPlayerMode) { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
        else { return inputTarget.transform.position; }
    }

    private IEnumerator Reload(float time)
    {
        ammo = 0;
        yield return new WaitForSeconds(time);
        ammo = maxAmmo;
    }

    private IEnumerator CoolDownTimer(float time)
    {
        if (!playerMode) time = time + Random.Range(-1, 1);
        CanShoot = false;
        yield return new WaitForSeconds(time);
        CanShoot = true;
    }
}
