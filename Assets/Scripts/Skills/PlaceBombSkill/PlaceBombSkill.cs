using System.Collections.Generic;
using Inventory;
using Items.Bomb;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.PlaceBombSkill
{
    public class PlaceBombSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly PlaceBombSkillScope _placementScope;
        private readonly ExplosionRange _explosion;

        private readonly List<Vector3Int> _highlightedCells = new();
        private bool _isComplete;

        public PlaceBombSkill(Tilemap tilemap, Vector3Int origin, ExplosionRange explosion)
        {
            _tilemap = tilemap;
            _placementScope = new PlaceBombSkillScope(tilemap, origin);
            _explosion = explosion;
        }

        public void Enter()
        {
            _highlightedCells.Clear();
            foreach (var cell in _placementScope.GetRange())
            {
                _tilemap.SetColor(cell, Color.red);
                _highlightedCells.Add(cell);
            }
        }

        public void Exit()
        {
            foreach (var cell in _highlightedCells)
                _tilemap.SetColor(cell, Color.white);
            _highlightedCells.Clear();
        }

        public void PlaceAt(Vector3Int cell)
        {
            var bomb = Object.Instantiate(GameData.Instance.bombPrefab, _tilemap.GetCellCenterWorld(cell), Quaternion.identity);
            bomb.GetComponent<BombBehaviour>().Init(_tilemap, cell, _explosion.up, _explosion.down, _explosion.left, _explosion.right);
            _isComplete = true;
        }

        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => _isComplete;
        public Vector3Int[] GetRange() => _placementScope.GetRange();
    }
}
