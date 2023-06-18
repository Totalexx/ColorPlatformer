using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.System;
using RougeBuilder.System.Impl;

namespace RougeBuilder.State;

public class OnGameState : GameState
{
    private readonly LinkedList<AbstractEntity> allEntities;
    public List<AbstractEntity> entitiesToAdd;
    public List<AbstractEntity> entitiesToRemove;
    
    private List<ISystem> gameSystems = new();

    public bool isWin = false;
    public bool playerIsDead = false;

    public OnGameState(LinkedList<AbstractEntity> gameEntities)
    {
        allEntities = gameEntities;
    }

    protected override void Start()
    {
        InitializeGameSystems();
        Debug.WriteLine("OnGame");
    }

    public override GameState Update()
    {
        entitiesToAdd = new List<AbstractEntity>();
        entitiesToRemove = new List<AbstractEntity>();
        
        foreach (var system in gameSystems)
            system.Update(allEntities);
        
        foreach (var entity in entitiesToAdd)
            allEntities.AddLast(entity);
        
        foreach (var entity in entitiesToRemove)
            allEntities.Remove(entity);
        
        if (isWin)
        {
            Graphics.SpriteBatch.DrawString(Graphics.Font, "You win!", Graphics.Camera.Center - new Vector2(30, 10), Color.White);
        }

        if (playerIsDead)
        {
            Graphics.SpriteBatch.DrawString(Graphics.Font, "You dead!", Graphics.Camera.Center - new Vector2(35, 10), Color.White);
            Graphics.SpriteBatch.DrawString(Graphics.Font, "Press enter to restart!", Graphics.Camera.Center - new Vector2(65, -10), Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                return new TutorialState();
        }
        
        return this;
    }

    private void InitializeGameSystems()
    {
        var collisionCheckSystem = new CollisionsCheckSystem();
        
        gameSystems = new List<ISystem>
        {
            new PlayerControlSystem(),
            new ShootSystem(this),
            new CameraFollowSystem(),
            new MoveToTargetSystem(),
            new MapDrawSystem(),
            new DrawSystem(),
            collisionCheckSystem,
            new PhysicsBoundsSystem(collisionCheckSystem),
            new DamageSystem(collisionCheckSystem),
            new CheckWinSystem(collisionCheckSystem, this),
            new MoveSystem(),
            new KillSystem(this)
        };
    }
}
