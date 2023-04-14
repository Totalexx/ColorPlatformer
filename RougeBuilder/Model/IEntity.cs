using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RougeBuilder.Component;

namespace RougeBuilder.Model;

public abstract class AbstractEntity
{
    private readonly Dictionary<Type, IComponent> _components = new();

    protected void AddComponent(IComponent component)
    {
        _components.Add(component.GetType(), component);
    }
    
    public TComponent GetComponent<TComponent>() where TComponent : IComponent
    {
        return (TComponent)_components[typeof(TComponent)];
    }
    
    public ImmutableList<KeyValuePair<Type, IComponent>> GetComponents()
    {
        return _components.ToImmutableList();
    }
}