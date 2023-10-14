using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttack : Characters, IDamageable
{
    public abstract void DamageTaken(float damage);
    public abstract void Die();
    public abstract void Healing(float heal);

    //variables
    protected CharacterWeapon characterWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
