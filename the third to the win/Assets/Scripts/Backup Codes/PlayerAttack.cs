using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //variables 
    [SerializeField]
    private float delay = 0.5f;

    private bool attackBlocked = false;


    private Vector2 view_direction;

    //constants
    public const string HORIZONTAL_ATTACK = "Horizontal_Attack";
    public const string VERTICAL_ATTACK = "Vertical_Attack";
    public const string TRIGGER_ATTACK = "Attack";

    [SerializeField]
    protected Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log("2");
        view_direction = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tmp_vector;
        //get the movement direction in which we want the player to move
        tmp_vector.x = Input.GetAxisRaw("Horizontal");
        tmp_vector.y = Input.GetAxisRaw("Vertical");
        //need to normalized, else diagonals are faster then horizontal or vertical
        tmp_vector = tmp_vector.normalized;

        if (attackBlocked)
        {
            //Debug.Log("f");
            return;
        }
        if(tmp_vector.sqrMagnitude > 0)
        {
            view_direction = tmp_vector;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))//Mouse0 is mouse left click
        {
            anim.SetFloat(HORIZONTAL_ATTACK, view_direction.x);
            anim.SetFloat(VERTICAL_ATTACK, view_direction.y);
            Attack();
        }
    }

    private void Attack()
    {
        PlayerMovement.set_can_move(false);
        attackBlocked = true;
        anim.SetTrigger(TRIGGER_ATTACK);
        //Debug.Log("Attack");
        //StartCoroutine(DelayAttack());
        
    }
    
    


    public IEnumerator DelayAttack()
    {
        //Debug.Log("1");
        yield return new WaitForSeconds(delay);
        //Debug.Log("2");
        attackBlocked = false;
        
    }
}
