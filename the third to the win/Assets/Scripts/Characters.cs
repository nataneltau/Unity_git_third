using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    /**
     * Can add abstract methods if needed
     * 
     */

    //variables
    [SerializeField]
    protected float health = 100f;
    [SerializeField]
    protected float movement_speed = 5f;

    protected Vector2 movement_direction;

    //components
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected Animator anim;

    //const
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";


    protected virtual void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //This function move the character, should be called inside FixedUpdate() method
    protected void MoveCharacter()
    {
        rb.MovePosition(rb.position + movement_speed * Time.fixedDeltaTime * movement_direction);
        
    }
}
