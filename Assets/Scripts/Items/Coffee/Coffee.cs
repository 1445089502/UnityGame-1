using UnityEngine;
using UnityEngine.Tilemaps;

namespace Items.Coffee
{
    public class Coffee : ScriptableObject
    {
        public static Coffee Instance;

        public TileBase[] stageTiles;
        public Sprite seedIcon;
        public Sprite harvestIcon;
    }
}
