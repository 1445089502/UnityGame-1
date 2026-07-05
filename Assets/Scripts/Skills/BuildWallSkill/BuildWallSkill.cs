using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.BuildWallSkill
{
    public class BuildWallSkill : ISkill
    {
        private readonly Tilemap _tilemap;
        private readonly BuildWallSkillScope _scope;

        public BuildWallSkill(Tilemap tilemap, Vector3Int currentPosition)
        {
            _tilemap = tilemap;
            _scope = new BuildWallSkillScope(tilemap, currentPosition);
        }

        public Vector3Int[] GetRange() => _scope.GetRange();

        public void BuildAt(Vector3Int cell)
        {
            _tilemap.SetTile(cell, GameData.Instance.wallTile);
            _tilemap.SetColor(cell, Color.white);
        }

        public void Enter() => _scope.Enter();
        public void Exit() => _scope.Exit();
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
