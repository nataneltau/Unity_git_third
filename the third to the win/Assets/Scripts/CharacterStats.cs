using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStats : ScriptableObject
{
    private float health;
    public float maxHealth;
    public float Health
    {
        get { return health; }
        set 
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }//end of set
    }//end of Health property

    public float movementSpeed;
    public float damage;
    public int maxRandomBiasToDamage = 5;


}
