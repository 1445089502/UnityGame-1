using Skills.PlaceBombSkill;
using UnityEngine;

namespace Items.Bomb
{
    public class BombStats : MonoBehaviour
    {
        public static BombStats Instance;

        [SerializeField] public float explosionTime = 3f;
        [SerializeField] public float flashDuration = 0.5f;
        [SerializeField] public ExplosionRange explosion = new();
        [SerializeField] public Sprite bombSprite;
    }
}
