using Skills.SkillScope;
using UnityEngine;

namespace Skills
{
    public abstract class RangeSkill<TScope> : ISkill where TScope : BaseSkillScope
    {
        protected readonly TScope Scope;

        protected RangeSkill(TScope scope)
        {
            Scope = scope;
        }

        public void Enter() => Scope.Enter();
        public void Exit() => Scope.Exit();

        public Vector3Int[] GetRange() => Scope.GetRange();

        public void Update(float deltaTime) { }
        public void FixedUpdate(float deltaTime) { }
        public void LateUpdate(float deltaTime) { }
        public bool IsComplete => true;
    }
}
