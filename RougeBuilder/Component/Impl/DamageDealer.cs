using System;
using System.Collections.Generic;

namespace RougeBuilder.Component.Impl;

public class DamageDealer : AbstractComponent
{
    public int Damage = 30;
    public HashSet<Type> IgnoreComponents;

    public DamageDealer(HashSet<Type> ignore)
    {
        IgnoreComponents = ignore;
    }
}