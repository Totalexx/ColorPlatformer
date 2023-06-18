using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RougeBuilder.Component;

namespace RougeBuilder.Entity;

public abstract class AbstractEntity
{
    public readonly Guid id = Guid.NewGuid(); 
    
    private readonly Dictionary<Type, AbstractComponent> _components = new();

    public T GetComponent<T>() where T : AbstractComponent
    {
        return (T)_components[typeof(T)];
    }
    
    public bool HasComponent<T>() where T : AbstractComponent
    {
        return _components.ContainsKey(typeof(T));
    }
    
    public ImmutableDictionary<Type, AbstractComponent> GetAllComponents()
    {
        return _components.ToImmutableDictionary();
    }

    protected abstract IEnumerable<AbstractComponent> InitializeComponents();

    protected AbstractEntity()
    {
        AddComponents(InitializeComponents());
    }
    
    protected void AddComponent(AbstractComponent component)
    {
        component.Owner = this;
        component.CheckCorrectComponent();
        _components.Add(component.GetType(), component);
    }

    protected void AddComponents(IEnumerable<AbstractComponent> components)
    {
        foreach (var component in components)
            AddComponent(component);
    }

    protected bool Equals(AbstractEntity other)
    {
        return id.Equals(other.id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AbstractEntity)obj);
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}