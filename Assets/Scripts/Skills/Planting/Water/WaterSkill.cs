using Inventory;
using Items.Coffee;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Water
{
    public class WaterSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly WaterSkillScope _scope;

        public WaterSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new WaterSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public void WaterAt(Vector3Int cell)
        {
            var tile = _tilemap.GetTile(cell);
            if (tile == null) return;

            // 如果是作物，推进阶段
            var crop = Coffee.Instance;
            var index = System.Array.IndexOf(crop.stageTiles, tile);
            if (index >= 0 && index < crop.stageTiles.Length - 1)
            {
                _tilemap.SetTile(cell, crop.stageTiles[index + 1]);
                return;
            }

            // 如果是普通地面，浇水
            if (tile == GameData.Instance.groundTile)
                _tilemap.SetTile(cell, GameData.Instance.groundWateredTile);
        }

        public void Enter() => _scope.Enter();
        public void Exit() => _scope.Exit();
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
