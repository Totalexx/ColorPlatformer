using System.Collections.Generic;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl;
using RougeBuilder.System;
using RougeBuilder.System.Impl;

namespace RougeBuilder.State;

public class GameState : IState
{
    public LinkedList<AbstractEntity> AllEntities = new();

    private List<ISystem> gameSystems = new()
    {
        new DrawSystem(GlobalHolder.SpriteBatch),
        new PlayerControlSystem(),
    };

    public GameState()
    {
        AllEntities.AddLast(new Player());
    }
    
    public void Update()
    {
        foreach (var system in gameSystems)
            system.Update(AllEntities);
    }
}