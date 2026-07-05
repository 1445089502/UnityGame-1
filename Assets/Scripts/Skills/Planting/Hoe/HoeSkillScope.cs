using Inventory;
using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Hoe
{
    public class HoeSkillScope : RangePreview
    {
        public HoeSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, new Color(0.6f, 0.4f, 0.2f), null,
                type => type == Tile.ColliderType.None)
        {
            PreviewedCells.Clear();
            foreach (var cell in GetRange())
            {
                var tile = Tilemap.GetTile(cell);
                if (tile == null) continue;
                var name = tile.name;
                if (name == "road" || name == "ground" || name == "ground_watered")
                {
                    Tilemap.SetColor(cell, Color);
                    PreviewedCells.Add(cell);
                }
            }
        }
    }
}
