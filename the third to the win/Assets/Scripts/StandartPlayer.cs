using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartPlayer : PlayerCharacters
{
    /**
     * For now this script contain most of the player code but if we will add more "player types" then
     * all the common code should be in PlayerCharacters script
     */
    //variables
    [SerializeField]
    private float delay_attack = 0.3f;//should be only for players?
    private bool can_move = true;
    private bool attackBlocked = false;
    private GameObject weaponChild;


    //components

    //constants
    public const string SPEED = "Speed";
    public const string TRIGGER_ATTACK = "Attack";
    public const string WEAPON_PARENT = "WeaponParent";
    private Vector2 PLAYER_VISION_POSITION = new Vector2(0, -1);



    public Vector2 GetPlayerPosition()
    {
        return new Vector2(anim.GetFloat(HORIZONTAL), anim.GetFloat(VERTICAL));
    }

    private void Awake()
    {
        InitializeComponents();
        health = 100;
        weaponChild = this.gameObject.transform.Find(WEAPON_PARENT).gameObject;//should return the WeaponParent child game object
        anim.SetFloat(HORIZONTAL, PLAYER_VISION_POSITION.x);
        anim.SetFloat(VERTICAL, PLAYER_VISION_POSITION.y);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNormalizedMovementDirection();//this method update movement_direction variable

        if (movement_direction.sqrMagnitude != 0 && can_move)
        {
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }

        anim.SetFloat(SPEED, movement_direction.sqrMagnitude);//instead of magnitude use sqrMagnitude, its a nice preformence trick

        if (!attackBlocked)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))//Mouse0 is mouse left click
            {
                Attack();
            }
        }
        

    }//end of Update method

    private void FixedUpdate()
    {
        if (can_move)
        {
            MoveCharacter();
        }
        
    }//end of FixedUpdate method
    private void Attack()
    {
        can_move = false;
        attackBlocked = true;
        anim.SetTrigger(TRIGGER_ATTACK);
        
    }//end of Attack method

    public void AttackInAnimation()//used in animation
    {
        weaponChild.GetComponent<CharacterWeapon>().HitCharacter(GetPlayerPosition());
    }


    public void Allow_movement()//used in animation
    {
        can_move = true;
        StartCoroutine(DelayAttack());

    }//end of Allow_movement method


    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay_attack);
        attackBlocked = false;

    }//end of DelayAttack method

    public override void DamageTaken(float damage)
    {
        health -= damage;
        Debug.Log("Health of character is: "+ health);
        if(health < 0)
        {
            Die();
        }
    }
    public override void Healing(float heal)
    {
        Debug.Log("2");
    }
    public override void Die()
    {
        Debug.Log("Character is dead!!!");
    }
    
    

}
