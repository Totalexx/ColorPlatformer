using System.Collections.Generic;
using RougeBuilder.Model;

namespace RougeBuilder.Component.Impl;

public class EntityCollector<T> : AbstractComponent
{
    public LinkedList<T> Collection;
}