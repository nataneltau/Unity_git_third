using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //public void InitializeHealth(); //need it?
    public void DamageTaken(float damage);
    public void Healing(float heal);
    public void Die();
}
