using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Controller : NetworkBehaviour
{
    public bool androidMode;
    public Joystick transformJoystick;
    
    [HideInInspector]public Rigidbody2D rb;
    private FrameInput _frameInput;
    private Animator _animator;
    public float speed;

    private float skillReloadCooficient = 1f;
    private ScriptableSkills FirstActiveSkill;
    private ScriptableSkills SecondActiveSkill;
    private ScriptableSkills ThirdActiveSkill;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if (!IsOwner) return;
        GatherInput();
        AnimatorSwitcher();
        
    }

    private void FixedUpdate()
    {
        //if (!IsOwner) return;
        Move();
    }

    private void GatherInput()
    {
            _frameInput = new FrameInput()
            {
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
            };
        

        if (Input.GetButtonDown("FirstSkill"))
        {
            SkillActivationCheck(FirstActiveSkill);
        }
        if (Input.GetButtonDown("SecondSkill"))
        {
            SkillActivationCheck(SecondActiveSkill);
        }
        if (Input.GetButtonDown("ThirdSkill"))
        {
            SkillActivationCheck(ThirdActiveSkill);
        }
    }

    private void Move()
    {
        rb.AddForce(_frameInput.Move.normalized * speed, ForceMode2D.Force);
    }

    private void AnimatorSwitcher()
    {
        _animator.SetFloat("Move", _frameInput.Move.magnitude);
    }

    public void AddSkillReloadCoof(float coof)
    {
        skillReloadCooficient += skillReloadCooficient * coof * 0.01f;
    }
    public void SetActiveSkill(ScriptableSkills skill)
    {
        if (FirstActiveSkill == null) { FirstActiveSkill = skill; skill.currentActivateNum = skill.maxActivateNum; return; }
        if (SecondActiveSkill == null) { SecondActiveSkill = skill; skill.currentActivateNum = skill.maxActivateNum; return; }
        if (ThirdActiveSkill == null) { ThirdActiveSkill = skill; skill.currentActivateNum = skill.maxActivateNum; return; }
    }
    public void SkillActivationCheck(ScriptableSkills skill)
    {
        if (skill != null && skill.currentActivateNum > 0) 
        {
            skill.currentActivateNum--;
            skill.Activate(); 
            StartCoroutine(ReloadSkill(skill, skill.ReloadDuration));
        }
        else { Debug.Log("Невозможно использовать навык"); }
    }

    public Vector2 GetMoveVector2()
    {
        return _frameInput.Move;
    }
    public struct FrameInput
    {
        public Vector3 Move;
        public bool FastAttackDown;
        public bool SlowAttackDown;
        public bool DashDown;
        public bool FirstAbilityDown;
        public bool SecondAbilityDown;
    }

    public IEnumerator ReloadSkill(ScriptableSkills skill,float time)
    {
        yield return new WaitForSeconds(time * skillReloadCooficient);
        skill.currentActivateNum++;
        if (skill.currentActivateNum < skill.maxActivateNum) { StartCoroutine(ReloadSkill(skill, time)); }
    }
}
