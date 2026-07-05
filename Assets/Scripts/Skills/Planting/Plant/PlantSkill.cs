using Items.Coffee;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.Planting.Plant
{
    public class PlantSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly PlantSkillScope _scope;

        public PlantSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new PlantSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public bool PlantAt(Vector3Int cell)
        {
            var tile = _tilemap.GetTile(cell);
            if (tile == null || tile.name != "ground_watered") return false;

            var crop = Coffee.Instance;
            if (crop.stageTiles.Length == 0) return false;
            _tilemap.SetTile(cell, crop.stageTiles[0]);
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
