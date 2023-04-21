using System.Collections.Generic;
using System.Linq;
using RougeBuilder.Component;
using RougeBuilder.Model;

namespace RougeBuilder.System;

public abstract class AbstractSystem<ProcessedComponent> : ISystem where ProcessedComponent : AbstractComponent
{
    public virtual void Update(IEnumerable<AbstractEntity> entities)
    {
        var processedEntities = GetProcessedEntities(entities);
        foreach (var processedEntity in processedEntities)
        {
            UpdateEntity(processedEntity);
        }
    }

    protected abstract void UpdateEntity(AbstractEntity entity);
    
    private IEnumerable<AbstractEntity> GetProcessedEntities(IEnumerable<AbstractEntity> entities)
    {
        return entities == null 
            ? Enumerable.Empty<AbstractEntity>() 
            : entities.Where(e => e.HasComponent<ProcessedComponent>());
    }
    
}