using System.Collections.Generic;
using UnityEngine;

namespace Skills.SkillScope
{
    public abstract class BaseSkillScope : ISkill
    {
        private bool _isComplete;
        protected readonly Vector3Int OriginCell;

        protected int Up { get; set; } = 1;
        protected int Down { get; set; } = 1;
        protected int Left { get; set; } = 1;
        protected int Right { get; set; } = 1;

        protected BaseSkillScope(Vector3Int originCell)
        {
            OriginCell = originCell;
        }

        public BaseSkillScope Scope => this;

        public virtual Vector3Int[] GetRange()
        {
            var cells = new List<Vector3Int>();
            for (var i = 1; i <= Up; i++)    cells.Add(OriginCell + Vector3Int.up * i);
            for (var i = 1; i <= Down; i++)  cells.Add(OriginCell + Vector3Int.down * i);
            for (var i = 1; i <= Left; i++)  cells.Add(OriginCell + Vector3Int.left * i);
            for (var i = 1; i <= Right; i++) cells.Add(OriginCell + Vector3Int.right * i);
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
