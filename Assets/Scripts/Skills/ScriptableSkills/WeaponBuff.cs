using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skills/WeaponBuff")]
public class WeaponBuff : ScriptableSkills
{
    private Weapon _weapon;

    public float extraDamage;
    public float extraShootCoolDown;
    public int extraMaxAmmo;
    public float extraReloadDuration;
    public int extraNumOfBullets;
    public float extraBulletSpeed;
    public float extraSpread;
    public float extraKnockback;
    public float defaultKnockback;
    public int extraBounce;
    public float extraBulletDestroyTime;
    public bool addPiercing;
    public List<Effect> effects;

    public override void Init()
    {
        _weapon = _Controller.GetComponentInChildren<Weapon>();
        _weapon.damage += _weapon.damage * extraDamage * 0.01f;
        _weapon.shootCoolDown += _weapon.shootCoolDown * extraShootCoolDown * 0.01f;
        _weapon.maxAmmo += extraMaxAmmo;
        _weapon.reloadDuration += _weapon.reloadDuration * extraReloadDuration * 0.01f;
        _weapon.numOfBullets += extraNumOfBullets;
        _weapon.bulletSpeed += _weapon.bulletSpeed * extraBulletSpeed * 0.01f;
        _weapon.spread += _weapon.spread * extraSpread * 0.01f;
        if (_weapon.knockback <= 0 ) { _weapon.knockback = defaultKnockback; }
        else { _weapon.knockback += _weapon.knockback * extraKnockback * 0.01f; }
        _weapon.knockback += _weapon.knockback * extraKnockback * 0.01f;
        _weapon.bounce += extraBounce;
        _weapon.bulletDestroyTime += _weapon.bulletDestroyTime * extraBulletDestroyTime * 0.01f;
        _weapon.piercing = addPiercing;
        if (effects.Count > 0)
        {
            foreach (Effect effect in effects)
            {
                _weapon.effects.Add(effect);
            }
        }
    }
}
