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
    private static Vector2 collisionBounds = new (MapAreaGenerator.MAP_TILES_WIDTH * MapTiles.TileSize.X,
        MapAreaGenerator.MAP_TILES_HEIGHT * MapTiles.TileSize.Y);
    private QuadTree quadTree = new (collisionBounds);
    private Dictionary<RectangleF, Collider> entityByCollider = new ();
    private Dictionary<Collider, RectangleF> colliderByEntity = new ();
    private readonly HashSet<RectangleF> staticEntities = new ();
    private bool mapTilesNotAdded = true;

    private LinkedList<(Collider, Collider)> collisions = new ();
    
    
    public override void Update(IEnumerable<AbstractEntity> entities)
    {
        if(AddMapColliders(entities))
            return;
        UpdateQuadTree(entities);
        UpdateCollisions();

        // foreach (var coll in colliderByEntity)
        // {
        //     Graphics.SpriteBatch.DrawRectangle(coll.Value, Color.Aqua);
        // }
    }

    public IEnumerable<(Collider, Collider)> GetCollisions()
    {
        return collisions;
    }
    
    private bool AddMapColliders(IEnumerable<AbstractEntity> entities)
    {
        if (!mapTilesNotAdded) return false;
        
        var map = entities.Where(e => e.HasComponent<MapMarker>()).First();
        var walls = map.GetComponent<EntityCollector<Tile>>().Collection.Where(e => e.HasComponent<Collider>());
        foreach (var wall in walls)
        {
            var collider = wall.GetComponent<Collider>();
            quadTree.Add(collider.Bounds);
            staticEntities.Add(collider.Bounds);
            entityByCollider[collider.Bounds] = collider;
            colliderByEntity[collider] = collider.Bounds;
        }

        mapTilesNotAdded = false;
        return true;
    }
    
    private void UpdateQuadTree(IEnumerable<AbstractEntity> entities)
    {
        foreach (var entity in GetProcessedEntities(entities))
        {
            var collider = entity.GetComponent<Collider>();
            if (collider.CollisionType == Collider.EntityCollisionType.STATIC)
            {
                if (!staticEntities.Contains(collider.Bounds))
                {
                    quadTree.Add(collider.Bounds);
                    staticEntities.Add(collider.Bounds);
                    entityByCollider[collider.Bounds] = collider;
                    colliderByEntity[collider] = collider.Bounds;
                }
                continue;
            }

            var isPresentEntity = colliderByEntity.ContainsKey(collider);
            if (isPresentEntity)
            {
                var previousCollider = colliderByEntity[collider];
                if (collider.Bounds == previousCollider) continue;
                quadTree.Remove(previousCollider);
                quadTree.Add(collider.Bounds);
                entityByCollider.Remove(previousCollider);
                entityByCollider[collider.Bounds] = collider;
                colliderByEntity[collider] = collider.Bounds;
            }
            else
            {
                quadTree.Add(collider.Bounds);
                entityByCollider[collider.Bounds] = collider;
                colliderByEntity[collider] = collider.Bounds;
            }
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