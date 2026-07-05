using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.DestroyWallSkill
{
    public class DestroyWallSkillScope : RangePreview
    {
        public DestroyWallSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.red, null, type => type != Tile.ColliderType.None) { }
    }
}
