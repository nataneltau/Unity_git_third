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

    private float alphaTransparentValue = 0.5f;
    [SerializeField]
    private float invulnerabilityCooldown = 3f;
    private bool canDamageable = true;
    private bool isDead = false;


    //components
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //constants
    public const string SPEED = "Speed";
    public const string TRIGGER_ATTACK = "Attack";
    public const string WEAPON_PARENT = "WeaponParent";
    public const string SWORD_SWING = "SwordSwing";
    public const string PLAYER_WALK = "PlayerWalk";
    public const string DIE = "Die";
    public const string PLAYER_DEATH = "PlayerDeath";
    private Vector2 PLAYER_VISION_POSITION = new Vector2(0, -1);



    private void Awake()
    {
        InitializeComponents();
        weaponChild = this.gameObject.transform.Find(WEAPON_PARENT).gameObject;//should return the WeaponParent child game object
        anim.SetFloat(HORIZONTAL, PLAYER_VISION_POSITION.x);
        anim.SetFloat(VERTICAL, PLAYER_VISION_POSITION.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateNormalizedMovementDirection();//this method update movement_direction variable

        if (movement_direction.sqrMagnitude != 0 && can_move && !Pausemenu.isPaused && !isDead)
        {//if we use only left and right sprties (without up and down animation) then also don't update the 
            //HORIZONTAL and VERTICAL if the player only go up and down
            //(meaning Math.abs(movement_direction.y ) == 1 and movement_direction.x == 0)
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }
        

        anim.SetFloat(SPEED, movement_direction.sqrMagnitude);//instead of magnitude use sqrMagnitude, its a nice preformence trick

        if (!attackBlocked && !Pausemenu.isPaused && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))//Mouse0 is mouse left click
            {
                Attack();
            }
        }
        

    }//end of Update method

    private void FixedUpdate()
    {
        if (can_move && !Pausemenu.isPaused && !isDead && movement_direction != Vector2.zero)
        {
            MoveCharacter();
        }
        else//add to enemy also?
        {
            //rb.AddForce(Vector2.zero);
            rb.velocity = Vector2.zero;
        }
       
        
    }//end of FixedUpdate method

    private void Attack()
    {
        can_move = false;
        attackBlocked = true;
        anim.SetTrigger(TRIGGER_ATTACK);
        
    }//end of Attack method

    public void SoundInMovementAnimation(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        AudioManager.instance.PlayAudio(PLAYER_WALK);
    }

    public void AttackInAnimation(AnimationEvent evt)//used in animation
    {
        //Debug.Log(evt.animatorClipInfo.weight);
        if (evt.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        AudioManager.instance.PlayAudio(SWORD_SWING);
        weaponChild.GetComponent<CharacterWeapon>().HitCharacter(GetCharacterPosition());
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
        if (!canDamageable || isDead)
        {
            return;
        }
        canDamageable = false;
        
        Health -= damage;
        //Debug.Log($"Health of <color=green> {gameObject.name} </color> is: " + Health);
        if (Health <= 0)
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
        AudioManager.instance.PlayAudio(PLAYER_DEATH);
        CallGameOver();


    }

    private void CallGameOver()
    {
        //TODO
    }

}
