using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.System.Impl;
using RougeBuilder.Utils;

namespace RougeBuilder.State;

public class GameLoadingState : GameState
{
    private readonly MapGenerationSystem mapGenerationSystem = new ();
    
    private readonly LinkedList<AbstractEntity> allEntities = new ();
    private bool _isGameLoaded = false;

    private BinaryTree<Rectangle> bsp;
    private List<Rectangle> rooms;

    protected override void Start()
    {
        allEntities.AddLast(new Player());
        allEntities.AddLast(new Map());
        
        mapGenerationSystem.Update(allEntities);
        rooms = mapGenerationSystem.GenerateRoomsTest();
        bsp = mapGenerationSystem.BSP;
    }

    public override GameState Update()
    {
        if (_isGameLoaded) 
            return new OnGameState(allEntities);
        foreach (var rectangle in bsp.GetLeafs())
        {
            var r = new Rectangle(rectangle.X * 16, rectangle.Y * 16, rectangle.Width * 16, rectangle.Height * 16);
            Graphics.SpriteBatch.DrawRectangle(r, Color.Aqua);
        }

        foreach (var room in rooms)
        {
            var r = new Rectangle(room.X * 16, room.Y * 16, room.Width * 16, room.Height * 16);
            Graphics.SpriteBatch.FillRectangle(r, Color.Gray);
        }
        
        return this;
    }
}