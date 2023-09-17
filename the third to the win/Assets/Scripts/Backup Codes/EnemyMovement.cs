using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Characters
{
    
    [SerializeField]
    private float attack_distance = 1f;

    private float curr_distance;

    [SerializeField]
    private GameObject player;
    



    private void Awake()
    {
        InitializeComponents();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_trans, this_trans;
        player_trans = player.transform.position;
        this_trans = this.transform.position;

        movement_direction = player_trans - this_trans; //try with rb.position?
        curr_distance = Vector3.Distance(player_trans, this_trans);

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // need for rotation, probably won't fit us
        movement_direction = movement_direction.normalized;

    }
    private void FixedUpdate()
    {
        if (curr_distance >= attack_distance)
        {
            MoveCharacter();
        }
           
    }

}
