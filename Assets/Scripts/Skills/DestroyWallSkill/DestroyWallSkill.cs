using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.DestroyWallSkill
{
    public class DestroyWallSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly DestroyWallSkillScope _scope;

        public DestroyWallSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new DestroyWallSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public void DestroyAt(Vector3Int cell)
        {
            var tile = _tilemap.GetTile(cell);
            if (tile != GameData.Instance.wallTile) return;
            _tilemap.SetTile(cell, GameData.Instance.roadTile);
        }

        public void Enter() => _scope.Enter();
        public void Exit() => _scope.Exit();
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
