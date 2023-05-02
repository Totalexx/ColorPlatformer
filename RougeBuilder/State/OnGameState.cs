using System.Collections.Generic;
using System.Diagnostics;
using RougeBuilder.Model;
using RougeBuilder.System;
using RougeBuilder.System.Impl;

namespace RougeBuilder.State;

public class OnGameState : GameState
{
    private readonly LinkedList<AbstractEntity> allEntities = new();
    
    private readonly List<ISystem> gameSystems = new()
    {
        new PlayerControlSystem(),
        new MoveSystem(),
        new CameraFollowSystem(),
        new MapDrawSystem(),
        new DrawSystem(),
    };
    
    public OnGameState(LinkedList<AbstractEntity> gameEntities)
    {
        allEntities = gameEntities;
    }

    protected override void Start()
    {
        Debug.WriteLine("OnGame");
    }

    public override GameState Update()
    {
        foreach (var system in gameSystems)
            system.Update(allEntities);
        
        return this;
    }
    
}