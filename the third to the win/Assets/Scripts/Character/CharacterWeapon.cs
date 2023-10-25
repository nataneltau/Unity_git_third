using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterWeapon : MonoBehaviour
{
    //variables
    [SerializeField]
    private Characters character;
    [SerializeField]
    private Transform weapon_origin_vertical;
    [SerializeField]
    private Transform weapon_origin_horizontal;
    [SerializeField]
    private float attack_radius;
    [SerializeField]
    private LayerMask attack_mask;


    public const string WEAPON_POINT_HORIZONTAL = "WeaponPointHorizontal";
    public const string WEAPON_POINT_VERTICAL = "WeaponPointVertical";


    private void Awake()
    {
        weapon_origin_horizontal = this.transform.Find(WEAPON_POINT_HORIZONTAL);
        weapon_origin_vertical = this.transform.Find(WEAPON_POINT_VERTICAL);
        if(attack_mask == 0)//nothing layer
        {
            Debug.LogError("You don't add LayerMask in attack_mask");
        }
    }

    public void HitCharacter(Vector2 game_character_pos)
    {
        //Vector2 game_character_pos = game_character.GetComponent<StandartPlayer>().GetPlayerPosition();
        Transform weapon_origin = null;
        float damage;

        //In this part we prioritise vertical over horizontal (as the animation prioritise vertical over horizontal) if
        //we want it to change just replace between the if's
        if (Mathf.Abs(game_character_pos.y) > 0.5f)//the attack is vertical
        {
            //DeterminePositionHorizontal(weapon_origin_horizontal, game_character_pos.x);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_vertical, game_character_pos.y, AxisEnum.Y_axis);
            weapon_origin = weapon_origin_vertical;
        }
        else// if(Math.Abs(game_character_pos.x) > 0.5f) meaning the attack is horizontal
        {
            //DeterminePositionVertical(weapon_origin_vertical, game_character_pos.y);//if want to change sprites so it will have only sides one
            DeterminePosition(weapon_origin_horizontal, game_character_pos.x, AxisEnum.X_axis);
            weapon_origin = weapon_origin_horizontal;
        }

        //filter the istrigger colliders for memory preformance but increase time complexity?
        Collider2D[] objs = Physics2D.OverlapCircleAll(weapon_origin.position, attack_radius, attack_mask);

        foreach (Collider2D item in objs)
        {   
            if (!item.isTrigger && item.TryGetComponent(out IDamageable dmg) )
            {
                damage = CalculateAttackDamage();
                //Debug.Log($"<b>The attack power of <color=blue> {game_character.name} </color> is: {damage}</b>");
                dmg.DamageTaken(damage);//get the amount of damage as an argument
            }
        }

    }

    private void DeterminePosition(Transform weapon_origin, float game_character_direction, AxisEnum axisEnum)
    {
        Vector3 tmp = weapon_origin.localPosition;
        game_character_direction /= Mathf.Abs(game_character_direction); //now game_character direction is 1 or -1

        if (axisEnum == AxisEnum.Y_axis)//Do Y_axis logic
        {
            if (tmp.y * game_character_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(tmp.x, Mathf.Abs(tmp.y) * game_character_direction, tmp.z);

            }//end of inner if
        }//end of outer if
        else if (axisEnum == AxisEnum.X_axis)//Do X_axis logic
        {
            if (tmp.x * game_character_direction < 0)//there multiplication is negative, need to change direction
            {
                weapon_origin.localPosition = new Vector3(Mathf.Abs(tmp.x) * game_character_direction, tmp.y, tmp.z);
            }//end of if
        }//end of outer else if
    }

    private float CalculateAttackDamage()
    {
        int tmp = Random.Range((-1) * character.stats.maxRandomBiasToDamage, character.stats.maxRandomBiasToDamage + 1);
        return character.stats.damage + tmp;
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
