using System.Collections.Generic;
using Items.Bomb;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.PlaceBombSkill
{
    public class PlaceBombSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly GameObject _bombPrefab;
        private readonly TileBase _roadTileBase;
        private readonly PlaceBombSkillScope _scope;
        private readonly int _explosionUp, _explosionDown, _explosionLeft, _explosionRight;

        private readonly List<Vector3Int> _highlightedCells = new();
        private bool _isComplete;

        public PlaceBombSkill(Tilemap tilemap, Vector3Int origin, GameObject bombPrefab, TileBase roadTileBase,
            int explosionUp, int explosionDown, int explosionLeft, int explosionRight, int placementRange = 1)
        {
            _tilemap = tilemap;
            _bombPrefab = bombPrefab;
            _roadTileBase = roadTileBase;
            _scope = new PlaceBombSkillScope(origin, placementRange);
            _explosionUp = explosionUp;
            _explosionDown = explosionDown;
            _explosionLeft = explosionLeft;
            _explosionRight = explosionRight;
        }

        /// <summary>施法范围（用于 PlayerInput 方向选择）</summary>
        public Vector3Int[] GetRange() => _scope.GetRange();

        public void Enter()
        {
            _highlightedCells.Clear();
            foreach (var cell in GetRange())
            {
                _tilemap.SetTileFlags(cell, TileFlags.None);
                _tilemap.SetColor(cell, Color.red);
                _highlightedCells.Add(cell);
            }
        }

        public void Exit()
        {
            foreach (var cell in _highlightedCells)
            {
                _tilemap.SetColor(cell, Color.white);
            }
            _highlightedCells.Clear();
        }

        /// <summary>在指定格子放置炸弹，传入爆炸范围</summary>
        public void PlaceAt(Vector3Int cell)
        {
            var bomb = Object.Instantiate(_bombPrefab, _tilemap.GetCellCenterWorld(cell), Quaternion.identity);
            bomb.GetComponent<BombBehaviour>().Init(_tilemap, cell, _explosionUp, _explosionDown, _explosionLeft, _explosionRight, _roadTileBase);
            _isComplete = true;
        }

        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => _isComplete;
    }
}
