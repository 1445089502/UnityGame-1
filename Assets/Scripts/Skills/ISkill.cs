using UnityEngine;

namespace Skills
{
    public interface ISkill
    {
        void Enter();
        void Update(float deltaTime);
        void FixedUpdate(float deltaTime);
        void LateUpdate(float deltaTime);
        void Exit();
        bool IsComplete { get; }
        Vector3Int[] GetRange();
    }
}