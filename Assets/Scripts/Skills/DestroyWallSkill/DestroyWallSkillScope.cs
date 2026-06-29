using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.DestroyWallSkill
{
    public class DestroyWallSkillScope : HighlightScope
    {
        public DestroyWallSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.red, type => type != Tile.ColliderType.None) { }
    }
}
