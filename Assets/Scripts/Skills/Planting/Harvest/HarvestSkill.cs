using Inventory;
using Items.Coffee;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Harvest
{
    public class HarvestSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly HarvestSkillScope _scope;

        public HarvestSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new HarvestSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public bool HarvestAt(Vector3Int cell)
        {
            var crop = Coffee.Instance;
            if (crop.stageTiles.Length == 0) return false;
            var mature = crop.stageTiles[crop.stageTiles.Length - 1];

            var tile = _tilemap.GetTile(cell);
            if (tile == null || tile.name != mature.name) return false;

            _tilemap.SetTile(cell, GameData.Instance.groundWateredTile);
            return true;
        }

        public void Enter() => _scope.Enter();
        public void Exit() => _scope.Exit();
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
