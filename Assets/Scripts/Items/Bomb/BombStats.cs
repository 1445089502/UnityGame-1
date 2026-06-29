using UnityEngine;

namespace Items.Bomb
{
    public class BombStats : MonoBehaviour
    {
        [SerializeField] public float explosionTime = 3f;
        [SerializeField] public float flashDuration = 0.5f;
        [SerializeField] public int up = 2;
        [SerializeField] public int down = 2;
        [SerializeField] public int left = 2;
        [SerializeField] public int right = 2;
        [SerializeField] public int placementRange = 1;
        
    }
}
