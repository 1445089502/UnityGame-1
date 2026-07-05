using UnityEngine;
using UnityEngine.Tilemaps;

namespace Inventory
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Inventory/Game Data")]
    public class GameData : ScriptableObject
    {
        public static GameData Instance;

        [Header("Tiles")]
        public TileBase wallTile;
        public TileBase roadTile;
        public TileBase groundTile;
        public TileBase groundWateredTile;

        [Header("Prefabs")]
        public GameObject bombPrefab;

        [Header("Tools")]
        public Sprite hammerIcon;
        public Sprite wateringCanIcon;
        public Sprite sickleIcon;
        public Sprite hoeIcon;

        [Header("Crops")]
        public Items.Coffee.Coffee coffee;
    }
}
