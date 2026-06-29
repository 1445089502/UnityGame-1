using System;
using Items.Bomb;
using Skills;
using Skills.BuildWallSkill;
using Skills.DestroyWallSkill;
using Skills.MoveSkill;
using Skills.PlaceBombSkill;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase wallTile;
        [SerializeField] private TileBase roadTile;
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private float directionSelectInputDelay = 0.1f;
        
        private InputAction _moveInputAction;
        private InputAction _buildWallAction;
        private InputAction _destroyWallAction;
        private InputAction _placeBombAction;

        private MoveSkill _moveSkill;
        private BuildWallSkill _buildWallSkill;
        private DestroyWallSkill _destroyWallSkill;
        private PlaceBombSkill _placeBombSkill;

        private bool _isBuilding;
        private bool _isDestroy;
        private bool _isPlacingBomb;
        private float _directionSelectInputDelayTimer;

        private void Start()
        {
            _moveInputAction = InputSystem.actions["Move"];
            _buildWallAction = InputSystem.actions["BuildWall"];
            _destroyWallAction = InputSystem.actions["DestroyWall"];
            _placeBombAction = InputSystem.actions["PlaceBomb"];
        }

        private void Update()
        {
            if (_buildWallAction.WasPressedThisFrame())
                (_isBuilding, _buildWallSkill) = ToggleSkill(_isBuilding, _buildWallSkill, cell => new BuildWallSkill(tilemap, cell, wallTile));
            if (_destroyWallAction.WasPressedThisFrame())
                (_isDestroy, _destroyWallSkill) = ToggleSkill(_isDestroy, _destroyWallSkill, cell => new DestroyWallSkill(tilemap, cell, roadTile));
            if (_placeBombAction.WasPressedThisFrame())
                (_isPlacingBomb, _placeBombSkill) = ToggleSkill(_isPlacingBomb, _placeBombSkill, cell => {
                    var bombStats = bombPrefab.GetComponent<BombStats>();
                    return new PlaceBombSkill(tilemap, cell, bombPrefab, roadTile, bombStats.up, bombStats.down, bombStats.left, bombStats.right, bombStats.placementRange);
                });

            var direction = DirectionSelect.GetDirection(_moveInputAction.ReadValue<Vector2>());

            // 避免一帧内执行完技能，导致方向选择后 直接进移动状态
            if (_directionSelectInputDelayTimer > 0)
            {
                _directionSelectInputDelayTimer -= Time.deltaTime;
                direction = Vector3Int.zero;
            }

            if (direction == Vector3Int.zero) return;

            if (_isBuilding)
            {
                var target = tilemap.WorldToCell(transform.position) + direction;
                foreach (var cell in _buildWallSkill.GetRange())
                {
                    if (cell != target) continue;
                    _buildWallSkill.BuildAt(target);
                    _buildWallSkill.Exit();
                    _isBuilding = false;
                    _buildWallSkill = null;
                    _directionSelectInputDelayTimer = directionSelectInputDelay;
                    break;
                }
                return;
            }

            if (_isDestroy)
            {
                var target = tilemap.WorldToCell(transform.position) + direction;
                foreach (var cell in _destroyWallSkill.GetRange())
                {
                    if (cell != target) continue;
                    _destroyWallSkill.DestroyAt(target);
                    _destroyWallSkill.Exit();
                    _isDestroy = false;
                    _destroyWallSkill = null;
                    _directionSelectInputDelayTimer = directionSelectInputDelay;
                    break;
                }
                return;
            }

            if (_isPlacingBomb)
            {
                var target = tilemap.WorldToCell(transform.position) + direction;
                foreach (var cell in _placeBombSkill.GetRange())
                {
                    if (cell != target) continue;
                    _placeBombSkill.PlaceAt(target);
                    _placeBombSkill.Exit();
                    _isPlacingBomb = false;
                    _placeBombSkill = null;
                    _directionSelectInputDelayTimer = directionSelectInputDelay;
                    break;
                }
                return;
            }

            // 移动
            if (direction != Vector3Int.zero && (_moveSkill?.IsComplete ?? true))
            {
                var currentCell = tilemap.WorldToCell(transform.position);
                _moveSkill = new MoveSkill(gameObject, currentCell + direction, tilemap);
                _moveSkill.Enter();
            }
            _moveSkill?.Update(Time.deltaTime);
        }

        private (bool active, T skill) ToggleSkill<T>(bool isActive, T skill, Func<Vector3Int, T> factory) where T : ISkill
        {
            if (isActive)
            {
                skill?.Exit();
                return (false, default);
            }
            var newSkill = factory(tilemap.WorldToCell(transform.position));
            newSkill.Enter();
            return (true, newSkill);
        }

    }
}
