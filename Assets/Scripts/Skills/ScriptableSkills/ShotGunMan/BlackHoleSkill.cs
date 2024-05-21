using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Skills/ActiveSkill/Dash")]
public class BlackHoleSkill : ScriptableSkills
{
    public GameObject prefab;
    public float impulse;
    public float attractionForce;
    public float instaRafius;
    public float originRadius;
    public float damage;
    public float duration;
    private Weapon _weapon;
    private DefaultBlackHole _blackHole;
    public override void Init()
    {
        _Controller.SetActiveSkill(this);
        _weapon = _Controller.GetComponentInChildren<Weapon>();
        _blackHole = _Controller.AddComponent<DefaultBlackHole>();
    }

    public override void Activate()
    {
        Vector2 direction = _weapon.GetWeaponRotation();
        _blackHole.StartCoroutine(_blackHole.Create(prefab, direction, impulse, attractionForce, instaRafius, originRadius, damage, duration));
    }
}

public class DefaultBlackHole: MonoBehaviour
{
    public IEnumerator Create(GameObject prefab, Vector2 direction, float impulse, float attractForce, float instRadius, float originalRadius, float damage, float time)
    {
        GameObject obj = Instantiate(prefab, gameObject.transform.position,Quaternion.identity);
        BlackHole _bh = obj.GetComponent<BlackHole>();
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        _bh.SetConfiguration(instRadius, attractForce, damage);
        rb.AddForce(direction * impulse, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        _bh.SetConfiguration(originalRadius, attractForce, damage);
        yield return new WaitForSeconds(time);
        _bh.SetConfiguration(0f, 0f, 0f);
        yield return new WaitForSeconds(5f);
        Destroy(obj.gameObject);
    }

}
