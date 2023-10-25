using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacters : Characters
{
    //variables
    [SerializeField]
    protected float attack_distance = 1f;

    protected float curr_distance;
    protected bool IsCollider = false;

    //components
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject playerCenter;
    [SerializeField]
    protected SpriteRenderer rend;

    //const
    public const string PLAYER_CENTER = "PlayerCenter";
    public const string PLAYER = "Player";

    protected override void InitializeComponents()
    {
        base.InitializeComponents();//call parent method, meaning call InitializeComponents() of Characters class
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag(PLAYER);
        playerCenter = player.transform.Find(PLAYER_CENTER).gameObject;
    }

    protected void CalculatePlayerPosition()
    {
        Vector3 player_trans, this_trans;
        player_trans = playerCenter.transform.position;
        this_trans = this.transform.position;

        movement_direction = player_trans - this_trans; 
        curr_distance = Vector3.Distance(player_trans, this_trans);

        movement_direction = movement_direction.normalized;
    }

    protected void CalculateRenderer()//check another way to do it through unity, add to onenote before deletation
    {
        if (playerCenter.transform.position.y >= this.transform.position.y)
        {//the enemy is below the player so the sorting order in the sprite renderer should be greater than player
            rend.sortingOrder = 1;
        }
        else
        {//the enemy is above the player so the sorting order in the sprite renderer should be smaller than player
            rend.sortingOrder = -1;
        }
    }

    protected void EnemyDying()
    {
        ScoreAndWaves.instance.IncreaseEnemiesKilled();
    }



}//end of class EnemyCharacters
