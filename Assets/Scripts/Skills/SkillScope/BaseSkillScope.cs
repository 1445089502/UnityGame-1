using System.Collections.Generic;
using UnityEngine;

namespace Skills.SkillScope
{
    public abstract class BaseSkillScope : ISkill
    {
        private bool _isComplete;
        protected readonly Vector3Int OriginCell;
        private readonly int _up, _down, _left, _right;

        protected BaseSkillScope(Vector3Int originCell, int up = 1, int down = 1, int left = 1, int right = 1)
        {
            OriginCell = originCell;
            _up = up;
            _down = down;
            _left = left;
            _right = right;
        }

        public virtual Vector3Int[] GetRange()
        {
            var cells = new List<Vector3Int>();
            for (var i = 1; i <= _up; i++)    cells.Add(OriginCell + Vector3Int.up * i);
            for (var i = 1; i <= _down; i++)  cells.Add(OriginCell + Vector3Int.down * i);
            for (var i = 1; i <= _left; i++)  cells.Add(OriginCell + Vector3Int.left * i);
            for (var i = 1; i <= _right; i++) cells.Add(OriginCell + Vector3Int.right * i);
            _isComplete = true;
            return cells.ToArray();
        }

        public virtual void Enter() { }
        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public virtual void Exit() { }
        public bool IsComplete => _isComplete;
    }
}
