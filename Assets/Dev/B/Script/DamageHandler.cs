using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public void DealDamage(int amount, Character target)
    {
        target.ReceiveDamage(amount);
    }
}
