using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponEnemy : MonoBehaviour
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

    private GameObject enemy;


    public const string WEAPON_POINT_HORIZONTAL = "WeaponPointHorizontal";
    public const string WEAPON_POINT_VERTICAL = "WeaponPointVertical";


    private void Awake()
    {
        enemy = this.transform.parent.gameObject;
        weapon_origin_horizontal = this.transform.Find(WEAPON_POINT_HORIZONTAL);
        weapon_origin_vertical = this.transform.Find(WEAPON_POINT_VERTICAL);
    }


    public void HitEnemy()
    {
        Vector2 enemy_pos = enemy.GetComponent<StandartEnemy>().GetCharacterPosition();
        Transform weapon_origin = null;

        //In this part we prioritise vertical over horizontal (as the animation prioritise vertical over horizontal) if
        //we want it to change just replace between the if's
        if (Math.Abs(enemy_pos.y) > 0.5f)//the attack is vertical
        {
            //DeterminePositionHorizontal(weapon_origin_horizontal, enemy_pos.x);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_vertical, enemy_pos.y, AxisEnum.Y_axis);
            weapon_origin = weapon_origin_vertical;
        }
        else// if(Math.Abs(enemy_pos.x) > 0.5f) meaning the attack is horizontal
        {
            //DeterminePositionVertical(weapon_origin_vertical, enemy_pos.y);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_horizontal, enemy_pos.x, AxisEnum.X_axis);
            weapon_origin = weapon_origin_horizontal;
        }

        //filter the istrigger colliders for memory preformance but increase time complexity?
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

    private void DeterminePosition(Transform weapon_origin, float enemy_direction, AxisEnum axisEnum)
    {
        //go over it again, it work
        Vector3 tmp = weapon_origin.localPosition;
        enemy_direction /= Math.Abs(enemy_direction); //now enemy direction is 1 or -1
        //Debug.Log(enemy_direction);
        if (axisEnum == AxisEnum.Y_axis)//Do Y_axis logic
        {
            if (tmp.y * enemy_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(tmp.x, Math.Abs(tmp.y) * enemy_direction, tmp.z);

            }//end of inner if
        }//end of outer if
        else if (axisEnum == AxisEnum.X_axis)//Do X_axis logic
        {
            if (tmp.x * enemy_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(Math.Abs(tmp.x) * enemy_direction, tmp.y, tmp.z);
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
}
