using System.Collections.Generic;

namespace Inventory
{
    public class Inventory
    {
        private readonly List<Items.Item> _items = new();

        public Inventory()
        {
            _items.Add(new Items.Item { itemType = Items.Item.ItemType.CoffeeSeed, amount = 5 });
            _items.Add(new Items.Item { itemType = Items.Item.ItemType.Hammer, amount = 1 });
            _items.Add(new Items.Item { itemType = Items.Item.ItemType.Hoe, amount = 1 });
            _items.Add(new Items.Item { itemType = Items.Item.ItemType.WateringCan, amount = 1 });
            _items.Add(new Items.Item { itemType = Items.Item.ItemType.Sickle, amount = 1 });
        }

        public void AddItem(Items.Item.ItemType type, int amount)
        {
            var existing = _items.Find(i => i.itemType == type);
            if (existing != null)
            {
                existing.amount += amount;
            }
            else
            {
                _items.Add(new Items.Item { itemType = type, amount = amount });
            }
        }

        public void RemoveItem(Items.Item.ItemType type, int amount)
        {
            var existing = _items.Find(i => i.itemType == type);
            if (existing != null && existing.amount >= amount)
                existing.amount -= amount;
        }

        public List<Items.Item> GetItems() => _items;
    }
}
