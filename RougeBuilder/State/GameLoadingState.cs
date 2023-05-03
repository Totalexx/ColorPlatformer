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
    private LinkedList<LinkedList<Vector2>> corridors;

    protected override void Start()
    {
        allEntities.AddLast(new Player());
        allEntities.AddLast(new Map());
        
        mapGenerationSystem.Update(allEntities);
        mapGenerationSystem.TestGenerate();
        rooms = mapGenerationSystem.Room.Select(d => d.Value).ToList();
        corridors = mapGenerationSystem.Corr;
        bsp = mapGenerationSystem.BSP;
    }

    public override GameState Update()
    {
        if (_isGameLoaded) 
            return new OnGameState(allEntities);
        foreach (var rectangle in bsp.GetLeafs())
        {
            var r = new Rectangle(rectangle.Value.X * 16, rectangle.Value.Y * 16, rectangle.Value.Width * 16, rectangle.Value.Height * 16);
            Graphics.SpriteBatch.DrawRectangle(r, Color.Aqua);
        }

        foreach (var corridor in corridors)
        {
            foreach (var tile in corridor)
            {
                var r = new Rectangle((int)(tile.X) * 16 - 8, (int)(tile.Y) * 16 - 8, 16, 16);
                Graphics.SpriteBatch.FillRectangle(r, Color.Bisque);
            }
        }
        
        foreach (var room in rooms)
        {
            var r = new Rectangle(room.X * 16, room.Y * 16, room.Width * 16, room.Height * 16);
            Graphics.SpriteBatch.FillRectangle(r, Color.Gray);
        }
        
        return this;
    }
}