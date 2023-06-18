using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RougeBuilder.System.Impl;

public class CollisionsCheckSystem : AbstractSystem<Collider>
{
    private static Vector2 collisionSize = new (MapAreaGenerator.MAP_TILES_WIDTH * MapTiles.TileSize.X,
        MapAreaGenerator.MAP_TILES_HEIGHT * MapTiles.TileSize.Y);

    private static RectangleF collisionBounds = new(Vector2.Zero, collisionSize);
    
    private QuadTree quadTree = new (collisionSize);
    private Dictionary<RectangleF, Collider> entityByCollider = new ();
    private Dictionary<Collider, RectangleF> colliderByEntity = new ();
    private readonly HashSet<RectangleF> staticEntities = new ();
    private bool mapTilesNotAdded = true;

    private LinkedList<(Collider, Collider)> collisions = new ();
    
    
    public override void Update(IEnumerable<AbstractEntity> entities)
    {
        quadTree = new QuadTree(collisionSize);
        entityByCollider = new Dictionary<RectangleF, Collider>();
        colliderByEntity = new Dictionary<Collider, RectangleF>();
        
        var abstractEntities = entities.ToList();
        AddMapColliders(abstractEntities);
        UpdateQuadTree(abstractEntities);
        UpdateCollisions();

        // foreach (var coll in colliderByEntity)
        // {
        //     Graphics.SpriteBatch.DrawRectangle(coll.Value, Color.Aqua);
        // }
        // foreach (var bound in quadTree.GetBounds())
        // {
        //     Graphics.SpriteBatch.DrawRectangle(bound, Color.Red);
        // }
    }

    public IEnumerable<(Collider, Collider)> GetCollisions()
    {
        return collisions;
    }
    
    private void AddMapColliders(IEnumerable<AbstractEntity> entities)
    {
        var map = entities.Where(e => e.HasComponent<MapMarker>()).First();
        var walls = map.GetComponent<EntityCollector<Tile>>().Collection.Where(e => e.HasComponent<Collider>());
        foreach (var wall in walls)
        {
            var collider = wall.GetComponent<Collider>();
            quadTree.Add(collider.Bounds);
            entityByCollider[collider.Bounds] = collider;
            colliderByEntity[collider] = collider.Bounds;
        }
    }
    
    private void UpdateQuadTree(IEnumerable<AbstractEntity> entities)
    {
        foreach (var entity in GetProcessedEntities(entities))
        {
            var collider = entity.GetComponent<Collider>();
            
            if (!collisionBounds.Contains(collider.Bounds.TopLeft) || !collisionBounds.Contains(collider.Bounds.BottomRight))
                continue;
            
            quadTree.Add(collider.Bounds);
            entityByCollider[collider.Bounds] = collider;
            colliderByEntity[collider] = collider.Bounds;
        }
    }

    private void UpdateCollisions()
    {
        collisions = new LinkedList<(Collider, Collider)>();
        
        foreach (var intersection in quadTree.GetIntersections())
        {
            try
            {
                var firstEntity = entityByCollider[intersection.Item1];
                var secondEntity = entityByCollider[intersection.Item2];
                collisions.AddLast((firstEntity, secondEntity));
            }
            catch { }
        }
    }
}