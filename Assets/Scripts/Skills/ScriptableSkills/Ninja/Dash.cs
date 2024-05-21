using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu (menuName = "Skills/ActiveSkill/Dash")]
public class Dash : ScriptableSkills
{
    public float dashImpulse;
    private DefaultDash _dash;
    public override void Init()
    {
        _Controller.SetActiveSkill(this);
        _Controller.AddComponent<DefaultDash>();
        _dash = _Controller.GetComponent<DefaultDash>();
        _dash.controller = _Controller;
        _dash.impulse = dashImpulse;
    }

    public override void Activate()
    {
        _dash.Play();
    }
}

public class DefaultDash: MonoBehaviour
{
    private Health _health;
    private Rigidbody2D rb;
    [HideInInspector] public Controller controller;
    [HideInInspector]public float impulse;
    private void Awake()
    {
        _health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Play()
    {
        rb.velocity = controller.GetMoveVector2() * impulse;
    }
}
