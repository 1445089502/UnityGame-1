using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemBar : MonoBehaviour
    {
        private Inventory _inventory;
        private Transform _itemSlotContainer;
        private Transform _itemSlotTemplate;
        private List<RectTransform> _slots = new();
        private int _selectedIndex = -1;

        public int SelectedIndex => _selectedIndex;
        public Items.Item.ItemType SelectedType => _selectedIndex >= 0
            && _selectedIndex < _inventory.GetItems().Count
            ? _inventory.GetItems()[_selectedIndex].itemType
            : Items.Item.ItemType.CoffeeSeed;

        private void Awake()
        {
            _itemSlotContainer = transform.Find("ItemSlotContainer");
            _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");
        }

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            Refresh();
        }

        public void Refresh()
        {
            foreach (var slot in _slots) Destroy(slot.gameObject);
            _slots.Clear();

            var items = _inventory.GetItems();
            items.RemoveAll(i => i.amount <= 0);

            for (int i = 0; i < 8; i++)
            {
                var go = Instantiate(_itemSlotTemplate, _itemSlotContainer);
                go.gameObject.SetActive(true);
                var rt = (RectTransform)go.transform;
                rt.anchoredPosition = new Vector2(i * 40f, 0);

                var icon = rt.Find("Icon").GetComponent<Image>();
                var count = rt.Find("Count").GetComponent<TextMeshProUGUI>();
                if (i < items.Count)
                {
                    icon.sprite = items[i].GetSprite();
                    icon.enabled = true;
                    count.text = items[i].IsStackable() ? items[i].amount.ToString() : "";
                }
                else
                {
                    icon.sprite = null;
                    icon.enabled = false;
                    count.text = "";
                }
                _slots.Add(rt);
            }

            if (_selectedIndex >= 0) SetSel(_selectedIndex, true);
        }

        public void Select(int index)
        {
            if (index < 0 || index >= 8) return;
            if (index == _selectedIndex) { Deselect(); return; }

            Deselect();
            _selectedIndex = index;
            SetSel(_selectedIndex, true);
        }

        private void Deselect()
        {
            if (_selectedIndex >= 0 && _selectedIndex < 8)
                SetSel(_selectedIndex, false);
            _selectedIndex = -1;
        }

        private void SetSel(int index, bool selected)
        {
            var rt = _slots[index];
            var nor = rt.Find("NormalBackground")?.GetComponent<Image>();
            var sel = rt.Find("SelectedBackground")?.GetComponent<Image>();
            if (nor != null) nor.enabled = !selected;
            if (sel != null) sel.enabled = selected;
        }
    }
}
