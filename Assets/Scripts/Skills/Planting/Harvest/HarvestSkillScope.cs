using Items.Coffee;
using Skills.SkillScope;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Harvest
{
    public class HarvestSkillScope : RangePreview
    {
        public HarvestSkillScope(Tilemap tilemap, Vector3Int originCell)
            : base(tilemap, originCell, new Color(1f, 0.5f, 0f), null, type => type == Tile.ColliderType.None) { }

        public override void Enter()
        {
            PreviewedCells.Clear();
            var crop = Coffee.Instance;
            if (crop.stageTiles.Length == 0) return;
            var matureTile = crop.stageTiles[crop.stageTiles.Length - 1];

            foreach (var cell in GetRange())
            {
                var tile = Tilemap.GetTile(cell);
                if (tile == null || tile.name != matureTile.name) continue;
                Tilemap.SetColor(cell, Color);
                PreviewedCells.Add(cell);
            }
        }
    }
}
