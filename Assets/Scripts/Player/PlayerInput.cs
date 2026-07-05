using System;
using Inventory;
using Skills;
using Skills.BuildWallSkill;
using Skills.DestroyWallSkill;
using Skills.MoveSkill;
// using Skills.PlaceBombSkill; // DEMO遗留
using Skills.Planting.Harvest;
using Skills.Planting.Hoe;
using Skills.Planting.Plant;
using Skills.Planting.Water;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private GameData gameData;
        [SerializeField] private ItemBar itemBar;
        [SerializeField] private TMPro.TextMeshProUGUI goldText;
        [SerializeField] private float directionSelectInputDelay = 0.1f;

        private int _gold;

        private InputAction _moveInputAction;
        private InputAction _buildWallAction;
        private InputAction _destroyWallAction;
        private InputAction _waterAction;
        private InputAction _harvestAction;
        private InputAction _sellAction;
        private InputAction _buyAction;
        private InputAction _interactAction;
        private InputAction _slot1, _slot2, _slot3, _slot4, _slot5, _slot6, _slot7, _slot8;

        private Inventory.Inventory _inventory;
        private MoveSkill _moveSkill;
        private ISkill _activeSkill;
        private bool _isSkillActive;
        private int _lastToggleFrame;
        private float _directionSelectInputDelayTimer;

        private void Start()
        {
            GameData.Instance = gameData;
            Items.Coffee.Coffee.Instance = gameData.coffee;
            // Items.Bomb.BombStats.Instance = gameData.bombPrefab.GetComponent<Items.Bomb.BombStats>(); // DEMO遗留
            _inventory = new Inventory.Inventory();
            itemBar.SetInventory(_inventory);

            _moveInputAction = InputSystem.actions["Move"];
            _buildWallAction = InputSystem.actions["BuildWall"];
            _destroyWallAction = InputSystem.actions["DestroyWall"];
            _waterAction = InputSystem.actions["Water"];
            _harvestAction = InputSystem.actions["Harvest"];
            _sellAction = InputSystem.actions["Sell"];
            _buyAction = InputSystem.actions["Buy"];

            UpdateGoldText();
            _interactAction = InputSystem.actions["Interact"];
            _slot1 = InputSystem.actions["Slot1"];
            _slot2 = InputSystem.actions["Slot2"];
            _slot3 = InputSystem.actions["Slot3"];
            _slot4 = InputSystem.actions["Slot4"];
            _slot5 = InputSystem.actions["Slot5"];
            _slot6 = InputSystem.actions["Slot6"];
            _slot7 = InputSystem.actions["Slot7"];
            _slot8 = InputSystem.actions["Slot8"];
        }

        private void Update()
        {
            if (_slot1.WasPressedThisFrame()) itemBar.Select(0);
            if (_slot2.WasPressedThisFrame()) itemBar.Select(1);
            if (_slot3.WasPressedThisFrame()) itemBar.Select(2);
            if (_slot4.WasPressedThisFrame()) itemBar.Select(3);
            if (_slot5.WasPressedThisFrame()) itemBar.Select(4);
            if (_slot6.WasPressedThisFrame()) itemBar.Select(5);
            if (_slot7.WasPressedThisFrame()) itemBar.Select(6);
            if (_slot8.WasPressedThisFrame()) itemBar.Select(7);

            var scroll = Mouse.current?.scroll.ReadValue() ?? Vector2.zero;
            var idx = itemBar.SelectedIndex;
            if (idx < 0) idx = 0;
            if (scroll.y > 0) itemBar.Select(idx - 1);
            if (scroll.y < 0) itemBar.Select(idx + 1);

            if (_interactAction.WasPressedThisFrame() && Items.Item.GetPrice(itemBar.SelectedType) == 0)
                Toggle(cell => Items.Item.CreateSkill(itemBar.SelectedType, tilemap, cell));

            if (_buildWallAction.WasPressedThisFrame())
                Toggle(cell => new BuildWallSkill(tilemap, cell));
            if (_destroyWallAction.WasPressedThisFrame())
                Toggle(cell => new DestroyWallSkill(tilemap, cell));
            if (_waterAction.WasPressedThisFrame())
                Toggle(cell => new WaterSkill(tilemap, cell));
            if (_harvestAction.WasPressedThisFrame())
                Toggle(cell => new HarvestSkill(tilemap, cell));
            if (_sellAction?.WasPressedThisFrame() == true)
                TrySell();
            if (_buyAction?.WasPressedThisFrame() == true)
                TryBuy();

            var direction = DirectionSelect.GetDirection(_moveInputAction.ReadValue<Vector2>());

            if (_directionSelectInputDelayTimer > 0)
            {
                _directionSelectInputDelayTimer -= Time.deltaTime;
                direction = Vector3Int.zero;
            }

            if (_isSkillActive)
            {
                var target = tilemap.WorldToCell(transform.position) + direction;
                foreach (var cell in _activeSkill.GetRange())
                {
                    if (cell != target) continue;
                    ExecuteActiveSkill(target);
                    _activeSkill.Exit();
                    _isSkillActive = false;
                    _activeSkill = null;
                    _directionSelectInputDelayTimer = directionSelectInputDelay;
                    break;
                }
                return;
            }

            if (direction != Vector3Int.zero && (_moveSkill?.IsComplete ?? true))
            {
                var currentCell = tilemap.WorldToCell(transform.position);
                _moveSkill = new MoveSkill(gameObject, currentCell + direction, tilemap);
                _moveSkill.Enter();
            }
            _moveSkill?.Update(Time.deltaTime);
        }

        private void ExecuteActiveSkill(Vector3Int target)
        {
            switch (_activeSkill)
            {
                case BuildWallSkill s:  s.BuildAt(target);   break;
                case DestroyWallSkill s: s.DestroyAt(target); break;
                // case PlaceBombSkill s:  s.PlaceAt(target);   break; // DEMO遗留
                case PlantSkill s:
                    if (s.PlantAt(target))
                    {
                        _inventory.RemoveItem(Items.Item.ItemType.CoffeeSeed, 1);
                        itemBar.Refresh();
                    }
                    break;
                case WaterSkill s:      s.WaterAt(target);    break;
                case HoeSkill s:         s.UseAt(target);      break;
                case HarvestSkill s:
                    if (s.HarvestAt(target))
                    {
                        _inventory.AddItem(Items.Item.ItemType.CoffeeBean, 1);
                        itemBar.Refresh();
                    }
                    break;
            }
        }

        private void TrySell()
        {
            var type = itemBar.SelectedType;
            var price = Items.Item.GetPrice(type);
            if (price <= 0) return;

            _inventory.RemoveItem(type, 1);
            _gold += price;
            itemBar.Refresh();
            UpdateGoldText();
        }

        private void TryBuy()
        {
            if (_gold < 5) return;
            _gold -= 5;
            _inventory.AddItem(Items.Item.ItemType.CoffeeSeed, 1);
            itemBar.Refresh();
            UpdateGoldText();
        }

        private void UpdateGoldText()
        {
            if (goldText != null) goldText.text = $"G: {_gold}";
        }

        private void Toggle(Func<Vector3Int, ISkill> factory)
        {
            if (_lastToggleFrame == Time.frameCount) return;
            _lastToggleFrame = Time.frameCount;

            if (_isSkillActive)
            {
                _activeSkill?.Exit();
                _isSkillActive = false;
                _activeSkill = null;
            }
            else
            {
                _activeSkill = factory(tilemap.WorldToCell(transform.position));
                _activeSkill.Enter();
                _isSkillActive = true;
            }
        }
    }
}
