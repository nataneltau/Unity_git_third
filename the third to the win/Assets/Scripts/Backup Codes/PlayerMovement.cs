using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables
    [SerializeField]
    private float movement_speed = 5f;
    private Vector2 movement_direction;

    private static bool can_move = true;
    private PlayerAttack pa;

    public static void set_can_move(bool new_can_move)
    {
        can_move = new_can_move;
    }

    public void allow_movement()
    {
        set_can_move(true);
        StartCoroutine(pa.DelayAttack());

    }

    //constants
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string SPEED = "Speed";
    private Vector2 PLAYER_VISION_POSITION = new Vector2(0, -1);


    //components
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat(HORIZONTAL, PLAYER_VISION_POSITION.x);
        anim.SetFloat(VERTICAL, PLAYER_VISION_POSITION.y);
        pa = GetComponent<PlayerAttack>();
    }
    private void Start()
    {
        Debug.Log("1");
    }

    // Update is called once per frame
    void Update()
    {
        //get the movement direction in which we want the player to move
        movement_direction.x = Input.GetAxisRaw("Horizontal");
        movement_direction.y = Input.GetAxisRaw("Vertical");
        //need to normalized, else diagonals are faster then horizontal or vertical
        movement_direction = movement_direction.normalized;
        if(movement_direction.sqrMagnitude != 0)
        {
            anim.SetFloat(HORIZONTAL, movement_direction.x);
            anim.SetFloat(VERTICAL, movement_direction.y);
        }
        
        anim.SetFloat(SPEED, movement_direction.sqrMagnitude);//instead of magnitude use sqrMagnitude, its a nice preformence trick



    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (can_move)
        {
            rb.MovePosition(rb.position + movement_direction * movement_speed * Time.fixedDeltaTime);
        }
    }

}
