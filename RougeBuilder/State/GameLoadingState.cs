using System.Collections.Generic;
using RougeBuilder.Entity;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.System.Impl;

namespace RougeBuilder.State;

public class GameLoadingState : GameState
{
    private MapGenerationSystem mapGenerationSystem;
    private readonly LinkedList<AbstractEntity> allEntities = new ();
    public readonly List<AbstractEntity> toAdd = new ();
    private bool _isGameLoaded = false;

    protected override void Start()
    {
        mapGenerationSystem = new MapGenerationSystem(this);
        allEntities.AddLast(new Map());
        mapGenerationSystem.Update(allEntities);
        foreach (var entity in toAdd)
            allEntities.AddLast(entity);            
        _isGameLoaded = true;
    }

    public override GameState Update()
    {
        if (_isGameLoaded) 
            return new OnGameState(allEntities);
        
        return this;
    }
}