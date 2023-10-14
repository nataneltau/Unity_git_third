using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class CharacterMovement : Characters
{
    //This function move the character, should be called inside FixedUpdate() method
    protected void MoveCharacter()
    {
        //rb.MovePosition(rb.position + movement_speed * Time.fixedDeltaTime * movement_direction);
        //rb.AddForce(movement_speed * Time.fixedDeltaTime * movement_direction, ForceMode2D.Impulse);
        rb.velocity = stats.movementSpeed * Time.fixedDeltaTime * movement_direction;
    }
}
