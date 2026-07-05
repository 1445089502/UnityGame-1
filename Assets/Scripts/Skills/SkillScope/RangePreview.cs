using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.SkillScope
{
    public class RangePreview : BaseSkillScope
    {
        protected readonly Tilemap Tilemap;
        protected readonly Color Color;
        protected readonly Func<Tile.ColliderType, bool>[] Predicates;
        protected readonly List<Vector3Int> PreviewedCells = new();
        private readonly string _tileName;

        protected RangePreview(Tilemap tilemap, Vector3Int originCell, Color color,
            string tileName = null, params Func<Tile.ColliderType, bool>[] predicates)
            : base(originCell)
        {
            Tilemap = tilemap;
            Color = color;
            _tileName = tileName;
            Predicates = predicates;
        }

        public override void Enter()
        {
            PreviewedCells.Clear();
            foreach (var cell in GetRange())
            {
                if (!string.IsNullOrEmpty(_tileName))
                {
                    var tile = Tilemap.GetTile(cell);
                    if (tile == null || tile.name != _tileName) continue;
                }

                var pass = true;
                foreach (var p in Predicates)
                {
                    if (!p(Tilemap.GetColliderType(cell))) { pass = false; break; }
                }
                if (!pass) continue;

                Tilemap.SetColor(cell, Color);
                PreviewedCells.Add(cell);
            }
        }

        public override void Exit()
        {
            foreach (var cell in PreviewedCells)
            {
                Tilemap.SetColor(cell, Color.white);
            }
            PreviewedCells.Clear();
        }
    }
}
