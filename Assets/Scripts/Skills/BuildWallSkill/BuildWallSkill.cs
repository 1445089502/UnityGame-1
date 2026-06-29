using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.BuildWallSkill
{
    public class BuildWallSkill : RangeSkill<BuildWallSkillScope>
    {
        private readonly Tilemap _tilemap;
        private readonly TileBase _wallTile;

        public BuildWallSkill(Tilemap tilemap, Vector3Int currentPosition, TileBase wallTile)
            : base(new BuildWallSkillScope(tilemap, currentPosition))
        {
            _tilemap = tilemap;
            _wallTile = wallTile;
        }

        public void BuildAt(Vector3Int cell)
        {
            _tilemap.SetTile(cell, _wallTile);
            _tilemap.SetColliderType(cell, Tile.ColliderType.Sprite);
            _tilemap.SetColor(cell, Color.white);
        }
    }
}
