using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.SkillScope
{
    public class HighlightScope : BaseSkillScope
    {
        private readonly Tilemap _tilemap;
        private readonly Color _color;
        private readonly Func<Tile.ColliderType, bool> _predicate;
        private readonly List<Vector3Int> _highlightedCells = new();

        protected HighlightScope(Tilemap tilemap, Vector3Int originCell, Color color, Func<Tile.ColliderType, bool> predicate)
            : base(originCell)
        {
            _tilemap = tilemap;
            _color = color;
            _predicate = predicate;
        }

        public override void Enter()
        {
            _highlightedCells.Clear();
            foreach (var cell in GetRange())
            {
                if (!_predicate(_tilemap.GetColliderType(cell))) continue;

                _tilemap.SetColor(cell, _color);
                _highlightedCells.Add(cell);
            }
        }

        public override void Exit()
        {
            foreach (var cell in _highlightedCells)
            {
                _tilemap.SetColor(cell, Color.white);
            }
            _highlightedCells.Clear();
        }
    }
}
