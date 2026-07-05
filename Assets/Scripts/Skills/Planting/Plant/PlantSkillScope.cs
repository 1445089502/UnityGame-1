using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Plant
{
    public class PlantSkillScope : RangePreview
    {
        public PlantSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.green, "ground_watered") { }
    }
}
