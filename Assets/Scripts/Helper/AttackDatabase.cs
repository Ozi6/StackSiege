using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackDatabase : Singleton<AttackDatabase>
{
    public List<Type> allAttackTypes;

    protected override bool Persistent => false;

    protected override void OnAwake()
    {
        allAttackTypes = new List<Type>
        {
            typeof(BasicAttack),
            typeof(RocketAttack),
            typeof(SpreadAttack),
            typeof(LaserWallAttack)
        };
    }
}