using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Every character should have in his animator the HORIZONTAL and VERTICAL float variables to determine the position of the animation
//this what used in the code and that's what the character class and all his child are and should assume 
public abstract class oldCharacters : MonoBehaviour
{
    //variables
    [SerializeField]
    private float health = 100f;
    private float maxHealth;

    public float Health
    {
        get { return health; }
        protected set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            /*if (value <= 0)
            {
                health = 0;
            }//end of if
            else
            {
                health = value;
            }//end of else*/

        }//end of set
    }//end of Health property

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
        maxHealth = health;//*****************************************************shouldn't be here, maybe move to start and use inheretince (virtual start)
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
        rb.velocity = movement_speed * Time.fixedDeltaTime * movement_direction;
    }

    /**
     * Can add abstract methods if needed 
     */

}//end of class Characters
