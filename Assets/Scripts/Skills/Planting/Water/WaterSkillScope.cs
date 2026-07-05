using Inventory;
using Items.Coffee;
using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Water
{
    public class WaterSkillScope : RangePreview
    {
        public WaterSkillScope(Tilemap tilemap, Vector3Int currentPosition)
            : base(tilemap, currentPosition, Color.blue, null,
                type => type == Tile.ColliderType.None)
        {
            PreviewedCells.Clear();
            var crop = Coffee.Instance;
            foreach (var cell in GetRange())
            {
                var tile = Tilemap.GetTile(cell);
                if (tile == null) continue;

                var isGround = tile == GameData.Instance.groundTile;
                var isCrop = System.Array.IndexOf(crop.stageTiles, tile) >= 0
                          && System.Array.IndexOf(crop.stageTiles, tile) < crop.stageTiles.Length - 1;

                if (isGround || isCrop)
                {
                    Tilemap.SetColor(cell, Color);
                    PreviewedCells.Add(cell);
                }
            }
        }
    }
}
