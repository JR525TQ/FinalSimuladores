using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBlastGameManager : MonoBehaviour
{

    public static void UnitTakeDamage(Unit attackingUnit, Unit attackedUnit)
    {
        var damage = attackingUnit.unitStats.attackDamage;

        attackedUnit.TakeDamage(attackingUnit, damage);
    }


}
