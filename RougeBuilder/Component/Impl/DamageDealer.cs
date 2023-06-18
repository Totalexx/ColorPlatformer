using System;
using System.Collections.Generic;

namespace RougeBuilder.Component.Impl;

public class DamageDealer : AbstractComponent
{
    public int Damage = 30;
    public HashSet<Type> IgnoreComponents;

    public DamageDealer(int damage, HashSet<Type> ignore)
    {
        Damage = damage;
        IgnoreComponents = ignore;
    }
}