using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StandartEnemy : EnemyCharacters
{

    private GameObject weaponChild;

    public const string WEAPON_PARENT = "WeaponParent";


    private const RigidbodyConstraints2D freezeAll = RigidbodyConstraints2D.FreezeAll;

    //variables
    [SerializeField]
    private float delay_attack = 0.3f;//should be only for players?
    private bool can_move = true;
    private bool attackBlocked = false;
    [SerializeField]
    private float numOfAttacksAnimations = 2;
    [SerializeField]
    private float dieAfter = 2f;
    [SerializeField]
    private float knockbackDelay = 0.3f;
    [SerializeField]
    private float knockbackStrength = 10f;
    private bool knockback = false;

    private Coroutine knockbackCoroutine;


    //constants
    public const string IS_MOVING = "IsMoving";
    public const string TRIGGER_ATTACK = "Attack";
    public const string ATTACK_ANIMATION_NUMBER = "AttackAnimationNumber";
    public const string ENEMY_DEATH = "EnemyDeath";
    public const string DIE = "Die";


    private void Awake()
    {
        InitializeComponents();
        weaponChild = this.gameObject.transform.Find(WEAPON_PARENT).gameObject;//should return the WeaponParent child game object

    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerPosition();

        if (movement_direction.sqrMagnitude != 0 && can_move && !isDead && !Pausemenu.isPaused)
        {
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }

        CalculateRenderer();

    }

    private void FixedUpdate()
    {
        if (curr_distance >= attack_distance && !IsCollider && can_move && !isDead && !Pausemenu.isPaused)
        {//Enter this block if the enemy should move
            anim.SetBool(IS_MOVING, true);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            MoveCharacter();
        }
        else
        {//enter this block if the enemy should attack
            anim.SetBool(IS_MOVING, false);
            if (!knockback)
            {
                rb.constraints = freezeAll;
            }
            
            //TODO: add here if statement so the enemy attack will not happen consistently 
            if (!attackBlocked && !isDead && !Pausemenu.isPaused)
            {
                attackBlocked = true;
                StartCoroutine(StartAttackPlayer());
            }
            
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(player))
        {
            //Debug.Log("WOW");
            IsCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(player))
        {
            //Debug.Log("OMG");
            IsCollider = false;
        }
    }

    protected IEnumerator StartAttackPlayer()
    {
        yield return new WaitForSeconds(delay_attack/2);
        can_move = false;
        yield return new WaitForSeconds(delay_attack/2);
        //Debug.Log("Start attack");
        if (curr_distance >= attack_distance && !IsCollider)
        {//Enter this block if the enemy should move
            attackBlocked = false;
            can_move = true;
            //Debug.Log("shouldn't attack");
        }
        else
        {
            //Debug.Log("attack");
            can_move = false;
            float chooseAttack = Random.Range(0, numOfAttacksAnimations-1);
            anim.SetFloat(ATTACK_ANIMATION_NUMBER, chooseAttack);
            anim.SetTrigger(TRIGGER_ATTACK);
            
        }
        

    }

    public void AttackInAnimation(AnimationEvent evt)//used in animation
    {
        if (evt.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        weaponChild.GetComponent<CharacterWeapon>().HitCharacter(GetCharacterPosition());
    }//end of AttackInAnimation method

    public void AllowMovement()//used in animation
    {
        can_move = true;
        attackBlocked = false;

    }//end of Allow_movement method

    //*******************************
    public IEnumerator ResetKnockback()
    {
        Debug.Log("HI");
        yield return new WaitForSeconds(knockbackDelay);
        Debug.Log("bye");
        rb.velocity = Vector2.zero;
        knockback = false;
        can_move = true;
    }

    public void Knockback()
    {
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
            rb.velocity = Vector2.zero;
        }
        Vector3 vec = transform.position - playerCenter.transform.position;
        knockback = true;
        can_move = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(vec.normalized * knockbackStrength, ForceMode2D.Impulse);
        knockbackCoroutine = StartCoroutine(ResetKnockback());
    }
    //******************************
    public override void DamageTaken(float damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Knockback();
        }
    }
    public override void Healing(float heal)
    {
        Health += heal;
    }

    public override void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        anim.SetTrigger(DIE);
        AudioManager.instance.PlayAudio(ENEMY_DEATH);
        Destroy(gameObject, dieAfter);//can add another parameter second so it will destroy after X seconds
        EnemyDying();
    }

    
}
