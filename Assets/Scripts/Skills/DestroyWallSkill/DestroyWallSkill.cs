using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.DestroyWallSkill
{
    public class DestroyWallSkill : RangeSkill<DestroyWallSkillScope>
    {
        private readonly Tilemap _tilemap;
        private readonly TileBase _roadTile;

        public DestroyWallSkill(Tilemap tilemap, Vector3Int currentPosition, TileBase roadTile)
            : base(new DestroyWallSkillScope(tilemap, currentPosition))
        {
            _tilemap = tilemap;
            _roadTile = roadTile;
        }

        public void DestroyAt(Vector3Int cell)
        {
            _tilemap.SetTile(cell, _roadTile);
        }
    }
}
