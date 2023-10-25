using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Every character should have in his animator the HORIZONTAL and VERTICAL float variables to determine the position of the animation
//this what used in the code and that's what the character class and all his child are and should assume 
public abstract class Characters : MonoBehaviour, IDamageable
{
    //variables
    public CharactersStats stats;
    protected bool isDead = false;

    public float Health
    {
        get { return stats.health; }
        set
        {
            stats.health = Mathf.Clamp(value, 0, stats.maxHealth);
        }//end of set
    }//end of Health property

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
        stats.health = stats.maxHealth;
    }
    public Vector2 GetCharacterPosition()
    {
        return new Vector2(anim.GetFloat(HORIZONTAL), anim.GetFloat(VERTICAL));
    }

    //This function move the character, should be called inside FixedUpdate() method
    protected void MoveCharacter()
    {
        //rb.MovePosition(rb.position + movement_speed * Time.fixedDeltaTime * movement_direction);
        //rb.AddForce(movement_speed * Time.fixedDeltaTime * movement_direction, ForceMode2D.Impulse);
        rb.velocity = stats.movementSpeed * Time.fixedDeltaTime * movement_direction;

    }

    public abstract void DamageTaken(float damage);
    public abstract void Healing(float heal);
    public abstract void Die();


}//end of class Characters
