using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : CharacterAttack
{
    //variables
    [SerializeField]
    private float delayAttack = 0.3f;//should be only for players?
    private bool attackBlocked = false;
    private bool canDamageable = true;

    private float alphaTransparentValue = 0.5f;
    [SerializeField]
    private float invulnerabilityCooldown = 3f;

    //UnityEvent
    public UnityEvent BlockMove;

    //components
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //constants
    public const string TRIGGER_ATTACK = "Attack";
    public const string SWORD_SWING = "SwordSwing";
    public const string DIE = "Die";
    public const string PLAYER_DEATH = "PlayerDeath";


    private void Awake()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackBlocked && !Pausemenu.isPaused && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))//Mouse0 is mouse left click
            {
                Attack();
            }
        }
    }//end of update

    private void Attack()
    {
        BlockMove.Invoke();
        attackBlocked = true;
        anim.SetTrigger(TRIGGER_ATTACK);

    }//end of Attack method

    public void AttackInAnimation(AnimationEvent evt)//used in animation
    {
        //Debug.Log(evt.animatorClipInfo.weight);
        if (evt.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        AudioManager.instance.PlayAudio(SWORD_SWING);
        characterWeapon.HitCharacter(GetCharacterPosition());
    }

    public void CallDelayAttack()
    {
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delayAttack);
        attackBlocked = false;

    }//end of DelayAttack method

    public override void DamageTaken(float damage)
    {
        if (!canDamageable || isDead)
        {
            return;
        }
        canDamageable = false;

        stats.Health -= damage;
        //Debug.Log($"Health of <color=green> {gameObject.name} </color> is: " + Health);
        if (stats.Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(MakeTransparent());
        }
    }

    private IEnumerator MakeTransparent()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, alphaTransparentValue);//50% transparent
        yield return new WaitForSeconds(invulnerabilityCooldown);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);//0% transparent, normal sprite color
        canDamageable = true;
    }

    public override void Healing(float heal)
    {
        stats.Health += heal;
    }
    public override void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        anim.SetTrigger(DIE);
        AudioManager.instance.PlayAudio(PLAYER_DEATH);
        CallGameOver();


    }

    private void CallGameOver()
    {
        //TODO
    }

}
