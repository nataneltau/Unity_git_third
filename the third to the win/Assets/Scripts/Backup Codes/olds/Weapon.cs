using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //variables
    [SerializeField]
    private Transform weapon_origin_vertical;
    [SerializeField]
    private Transform weapon_origin_horizontal;
    [SerializeField]
    private float attack_radius;
    [SerializeField]
    private LayerMask attack_mask;

    private GameObject player;


    public const string WEAPON_POINT_HORIZONTAL = "WeaponPointHorizontal";
    public const string WEAPON_POINT_VERTICAL = "WeaponPointVertical";


    private void Awake()
    {
        player = this.transform.parent.gameObject;
        weapon_origin_horizontal = this.transform.Find(WEAPON_POINT_HORIZONTAL);
        weapon_origin_vertical = this.transform.Find(WEAPON_POINT_VERTICAL);
    }


    public void HitEnemy()
    {
        Vector2 player_pos = player.GetComponent<StandartPlayer>().GetCharacterPosition();
        Transform weapon_origin = null;

        //In this part we prioritise vertical over horizontal (as the animation prioritise vertical over horizontal) if
        //we want it to change just replace between the if's
        if (Math.Abs(player_pos.y) > 0.5f)//the attack is vertical
        {
            //DeterminePositionHorizontal(weapon_origin_horizontal, player_pos.x);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_vertical, player_pos.y, AxisEnum.Y_axis);
            weapon_origin = weapon_origin_vertical;
        }
        else// if(Math.Abs(player_pos.x) > 0.5f) meaning the attack is horizontal
        {
            //DeterminePositionVertical(weapon_origin_vertical, player_pos.y);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_horizontal, player_pos.x, AxisEnum.X_axis);
            weapon_origin = weapon_origin_horizontal;
        }

        Collider2D[] objs = Physics2D.OverlapCircleAll(weapon_origin.position, attack_radius, attack_mask);
        //Debug.Log(objs.Length);
        foreach (Collider2D item in objs)
        {
            if (!item.isTrigger)
            {
                Debug.Log(item.name);
            }
        }
       
    }

    private void DeterminePosition(Transform weapon_origin, float player_direction, AxisEnum axisEnum)
    {
        //go over it again, it work
        Vector3 tmp = weapon_origin.localPosition;
        player_direction /= Math.Abs(player_direction); //now player direction is 1 or -1
        //Debug.Log(player_direction);
        if(axisEnum == AxisEnum.Y_axis)//Do Y_axis logic
        {
            if (tmp.y * player_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(tmp.x, Math.Abs(tmp.y) * player_direction, tmp.z);

            }//end of inner if
        }//end of outer if
        else if(axisEnum == AxisEnum.X_axis)//Do X_axis logic
        {
            if (tmp.x * player_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(Math.Abs(tmp.x) * player_direction, tmp.y, tmp.z);
            }//end of if
        }//end of outer else if
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position_v = weapon_origin_vertical == null ? Vector3.zero : weapon_origin_vertical.position;
        Gizmos.DrawWireSphere(position_v, attack_radius);

        Gizmos.color = Color.blue;
        Vector3 position_h = weapon_origin_horizontal == null ? Vector3.zero : weapon_origin_horizontal.position;
        Gizmos.DrawWireSphere(position_h, attack_radius);
    }//end of OnDrawGizmosSelected method 


    //Enums
    public enum AxisEnum
    {
        Y_axis,
        X_axis,
    }

}//end of class Weapon
