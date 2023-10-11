using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    //public void DropItem(); //need it?
    public void PickItem();//give implementation to it? maybe add abstract class?
    public IEnumerator ItemEffect();//each item effect has limited time
}
