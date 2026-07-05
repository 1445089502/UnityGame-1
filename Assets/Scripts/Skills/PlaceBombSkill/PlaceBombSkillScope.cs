using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.PlaceBombSkill
{
    public class PlaceBombSkillScope : RangePreview
    {
        public PlaceBombSkillScope(Tilemap tilemap, Vector3Int originCell)
            : base(tilemap, originCell, Color.red, null, _ => true) {}
    }
}
