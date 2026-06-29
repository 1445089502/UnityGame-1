using UnityEngine;
namespace Systems
{
    public static class DirectionSelect
    {
        public static Vector3Int GetDirection(Vector2 input)
        {
            if (input.sqrMagnitude < 0.01f) return Vector3Int.zero;

            var absX = Mathf.Abs(input.x);
            var absY = Mathf.Abs(input.y);

            if (absX <= absY)
                return input.y > 0 ? Vector3Int.up : Vector3Int.down;
            else
                return input.x < 0 ? Vector3Int.left : Vector3Int.right;
        }

    }
    
}