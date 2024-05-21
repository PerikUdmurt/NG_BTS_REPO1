using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public string decription;
    public GameObject weaponPrefab;
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
    public List<Effect> effects;
    public bool playerMode;
}
