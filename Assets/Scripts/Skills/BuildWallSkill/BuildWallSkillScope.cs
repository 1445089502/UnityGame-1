using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.BuildWallSkill
{
    public class BuildWallSkillScope : RangePreview
    {
        public BuildWallSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.green, null, type => type == Tile.ColliderType.None) { }
    }
}
