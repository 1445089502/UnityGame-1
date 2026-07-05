using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Hoe
{
    public class HoeSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly HoeSkillScope _scope;

        public HoeSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new HoeSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public void UseAt(Vector3Int cell)
        {
            var tile = _tilemap.GetTile(cell);
            if (tile == null) return;

            var road = GameData.Instance.roadTile;
            var ground = GameData.Instance.groundTile;
            var watered = GameData.Instance.groundWateredTile;

            if (tile == road)
                _tilemap.SetTile(cell, ground);
            else if (tile == ground || tile == watered)
                _tilemap.SetTile(cell, road);
        }

        public void Enter() => _scope.Enter();
        public void Exit() => _scope.Exit();
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
