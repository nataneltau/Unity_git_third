using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacters : Characters, IDamageable
{
    public abstract void DamageTaken(float damage);
    public abstract void Healing(float heal);
    public abstract void Die();

    /**
* Can add abstract methods if needed
* 
*/
    protected void CalculateNormalizedMovementDirection()
    {
        Vector2 tmp_direction;
        //get the movement direction in which we want the player to move
        tmp_direction.x = Input.GetAxisRaw("Horizontal");
        tmp_direction.y = Input.GetAxisRaw("Vertical");
        //need to normalized, else diagonals are faster then horizontal or vertical
        tmp_direction = tmp_direction.normalized;

        movement_direction = tmp_direction;
    }
}
