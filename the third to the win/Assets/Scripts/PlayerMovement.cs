using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : CharacterMovement
{
    //variables
    private bool canMove = true;

    //UnityEvent
    public UnityEvent DelayAttack;

    //constants
    public const string SPEED = "Speed";
    public const string PLAYER_WALK = "PlayerWalk";
    private Vector2 PLAYER_VISION_POSITION = new Vector2(0, -1);


    private void Awake()
    {
        InitializeComponents();
        anim.SetFloat(HORIZONTAL, PLAYER_VISION_POSITION.x);
        anim.SetFloat(VERTICAL, PLAYER_VISION_POSITION.y);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateNormalizedMovementDirection();//this method update movement_direction variable

        if (movement_direction.sqrMagnitude != 0 && canMove && !Pausemenu.isPaused && !isDead)
        {//if we use only left and right sprties (without up and down animation) then also don't update the 
            //HORIZONTAL and VERTICAL if the player only go up and down
            //(meaning Math.abs(movement_direction.y ) == 1 and movement_direction.x == 0)
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }

        anim.SetFloat(SPEED, movement_direction.sqrMagnitude);//instead of magnitude use sqrMagnitude, its a nice preformence trick

    }

    private void FixedUpdate()
    {
        if (canMove && !Pausemenu.isPaused && !isDead /*&& movement_direction != Vector2.zero need this?*/)
        {
            MoveCharacter();
        }
        /*else//add to enemy also?
        {
            //rb.AddForce(Vector2.zero);
            rb.velocity = Vector2.zero;
        }*/


    }//end of FixedUpdate method

    private void CalculateNormalizedMovementDirection()
    {
        Vector2 tmp_direction;
        //get the movement direction in which we want the player to move
        tmp_direction.x = Input.GetAxisRaw("Horizontal");
        tmp_direction.y = Input.GetAxisRaw("Vertical");
        //need to normalized, else diagonals are faster then horizontal or vertical
        tmp_direction = tmp_direction.normalized;

        movement_direction = tmp_direction;
    }//end of CalculateNormalizedMovementDirection method

    public void SoundInMovementAnimation(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        AudioManager.instance.PlayAudio(PLAYER_WALK);
    }

    public void AllowMovement()//used in animation
    {
        canMove = true;
        DelayAttack.Invoke();

    }//end of Allow_movement method

    public void BlockMovement()
    {
        canMove = false;
    }


}
