using Inventory;
using Skills;
using Skills.DestroyWallSkill;
// using Skills.PlaceBombSkill; // DEMO遗留
using Skills.Planting.Harvest;
using Skills.Planting.Hoe;
using Skills.Planting.Plant;
using Skills.Planting.Water;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Items
{
    public class Item
    {
        public enum ItemType { CoffeeSeed, CoffeeBean, Hammer, WateringCan, Sickle, Hoe /* Bomb - DEMO遗留 */ }

        public ItemType itemType;
        public int amount;

        public Sprite GetSprite()
        {
            switch (itemType)
            {
                // case ItemType.Bomb: return Bomb.BombStats.Instance.bombSprite; // DEMO遗留
                case ItemType.CoffeeSeed: return Coffee.Coffee.Instance.seedIcon;
                case ItemType.CoffeeBean: return Coffee.Coffee.Instance.harvestIcon;
                case ItemType.Hammer: return GameData.Instance.hammerIcon;
                case ItemType.WateringCan: return GameData.Instance.wateringCanIcon;
                case ItemType.Sickle: return GameData.Instance.sickleIcon;
                case ItemType.Hoe: return GameData.Instance.hoeIcon;
                default: return null;
            }
        }

        public bool IsStackable()
        {
            switch (itemType)
            {
                // case ItemType.Bomb: // DEMO遗留
                case ItemType.CoffeeSeed:
                case ItemType.CoffeeBean:
                    return true;
                default: return false;
            }
        }

        public static int GetPrice(ItemType type)
        {
            switch (type)
            {
                case ItemType.CoffeeBean: return 10;
                default: return 0;
            }
        }

        public static ISkill CreateSkill(ItemType type, Tilemap tilemap, Vector3Int cell)
        {
            switch (type)
            {
                // case ItemType.Bomb: return new PlaceBombSkill(tilemap, cell, Bomb.BombStats.Instance.explosion); // DEMO遗留
                case ItemType.CoffeeSeed: return new PlantSkill(tilemap, cell);
                case ItemType.Hammer: return new DestroyWallSkill(tilemap, cell);
                case ItemType.WateringCan: return new WaterSkill(tilemap, cell);
                case ItemType.Sickle: return new HarvestSkill(tilemap, cell);
                case ItemType.Hoe: return new HoeSkill(tilemap, cell);
                default: return null;
            }
        }
    }
}
