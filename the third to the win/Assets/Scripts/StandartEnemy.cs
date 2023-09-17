using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemy : EnemyCharacters
{




    private GameObject weaponChild;

    public const string WEAPON_PARENT = "WeaponParent";

    //should be in character class? **********************
    public Vector2 GetEnemyPosition()
    {
        return new Vector2(anim.GetFloat(HORIZONTAL), anim.GetFloat(VERTICAL));
    }
    //****************************************************





    private const RigidbodyConstraints2D freezeAll = RigidbodyConstraints2D.FreezeAll;

    //variables
    [SerializeField]
    private float delay_attack = 0.3f;//should be only for players?
    private bool can_move = true;
    private bool attackBlocked = false;


    //constants
    public const string IS_MOVING = "IsMoving";
    public const string TRIGGER_ATTACK = "Attack";



    private void Awake()
    {
        InitializeComponents();
        rb.constraints = freezeAll;//need this?
        anim.SetBool(IS_MOVING, true);

        //*********************
        weaponChild = this.gameObject.transform.Find(WEAPON_PARENT).gameObject;//should return the WeaponParent child game object
        //*********************

    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerPosition();

        if (movement_direction.sqrMagnitude != 0 && can_move)
        {
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }

        CalculateRenderer();

    }

    private void FixedUpdate()
    {
        if (curr_distance >= attack_distance && !IsCollider && can_move)
        {//Enter this block if the enemy should move
            anim.SetBool(IS_MOVING, true);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            MoveCharacter();
        }
        else
        {//enter this block if the enemy should attack
            anim.SetBool(IS_MOVING, false);
            rb.constraints = freezeAll;
            //TODO: add here if statement so the enemy attack will not happen consistently 
            if (!attackBlocked)
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

    //TODO
    protected IEnumerator StartAttackPlayer()
    {
        yield return new WaitForSeconds(delay_attack);
        Debug.Log("Start attack");
        if (curr_distance >= attack_distance && !IsCollider)
        {//Enter this block if the enemy should move
            attackBlocked = false;
            //Debug.Log("shouldn't attack");
        }
        else
        {
            //Debug.Log("attack");
            can_move = false;
            anim.SetTrigger(TRIGGER_ATTACK);
            //*******************************
            weaponChild.GetComponent<CharacterWeapon>().HitCharacter(GetEnemyPosition());
            //*******************************
        }
        

    }

    public void AttackInAnimation()//used in animation
    {
        Debug.Log("Hit");
    }

    public void AllowMovement()//used in animation
    {
        can_move = true;
        attackBlocked = false;

    }//end of Allow_movement method

    /*protected IEnumerator AttackPlayerAndAttackAnimation()
    {
        yield return new WaitForSeconds(delay_attack);
        if (!(curr_distance >= attack_distance && !IsCollider))
        {//should attack the player
            Debug.Log("Start attack");
        }
        else
        {//the player move so don't attack, just move torwards the players 
            Debug.Log("Start attack");
        }

    }*/

    public override void DamageTaken(float damage)
    {
        Destroy(this.gameObject);//can add another parameter second so it will destroy after X seconds
    }
    public override void Healing(float heal)
    {
        Debug.Log("4");
    }

    public override void Die()
    {
        Debug.Log("5");
    }

}
