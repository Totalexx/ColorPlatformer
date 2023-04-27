using System.Collections.Generic;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl;
using RougeBuilder.Model.Impl.Map;

namespace RougeBuilder.State;

public class GameLoadingState : GameState
{
    private readonly LinkedList<AbstractEntity> allEntities = new ();
    private bool _isGameLoaded = false;
    
    protected override void Start()
    {
        allEntities.AddLast(new Player());
        allEntities.AddLast(new Map());
        _isGameLoaded = true;
    }

    public override GameState Update()
    {
        if (_isGameLoaded) 
            return new OnGameState(allEntities);
        return this;
    }
}