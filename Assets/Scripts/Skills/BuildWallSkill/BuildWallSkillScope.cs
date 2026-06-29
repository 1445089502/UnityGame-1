using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.BuildWallSkill
{
    public class BuildWallSkillScope : HighlightScope
    {
        public BuildWallSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.green, type => type == Tile.ColliderType.None) { }
    }
}
