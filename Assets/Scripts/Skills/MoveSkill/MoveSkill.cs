using Stats;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Skills.MoveSkill
{
    public class MoveSkill : ISkill
    {
        private readonly GameObject _gameObject;
        private readonly Vector3Int _targetCell;
        private readonly Tilemap _tilemap;
        private readonly float _speed;
        private Vector3 _targetWorldPosition;
        private bool _isComplete;

        public MoveSkill(GameObject gameObject, Vector3Int targetCell, Tilemap tilemap)
        {
            _gameObject = gameObject;
            _targetCell = targetCell;
            _tilemap = tilemap;

            var stats = gameObject.GetComponent<IStats>();
            _speed = stats.MoveSpeed;
        }

        public void Enter()
        {
            _targetWorldPosition = _tilemap.GetCellCenterWorld(_targetCell);
            _isComplete = false;
        }

        public void Update(float deltaTime)
        {
            
            if (_isComplete) return;

            if (!_tilemap.HasTile(_targetCell) || _tilemap.GetColliderType(_targetCell) != Tile.ColliderType.None)
            {
                _isComplete = true;
                return;
            }

            var newPos = Vector3.MoveTowards(_gameObject.transform.position, _targetWorldPosition, _speed * deltaTime);
            _gameObject.transform.position = newPos;

            if (!(Vector3.Distance(newPos, _targetWorldPosition) < 0.01f)) return;
            _gameObject.transform.position = _targetWorldPosition;
            _isComplete = true;
        }

        public void FixedUpdate(float dt) { }
        public void LateUpdate(float dt) { }
        public void Exit() { }
        public bool IsComplete => _isComplete;
    }
}