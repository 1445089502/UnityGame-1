using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Items.Bomb
{
    public class BombBehaviour : MonoBehaviour
    {
        private Tilemap _tilemap;
        private Vector3Int _origin;
        private int _up, _down, _left, _right;
        private BombStats _stats;

        private float _timer;
        private bool _isFlashing;
        private GameObject _vRect, _hRect;
        private Renderer _vRenderer, _hRenderer;

        public void Init(Tilemap tilemap, Vector3Int origin, int up, int down, int left, int right)
        {
            _tilemap = tilemap;
            _origin = origin;
            _up = up; _down = down; _left = left; _right = right;
            _stats = GetComponent<BombStats>();
            _timer = _stats.explosionTime;

            CreateVisuals();
        }

        private void CreateVisuals()
        {
            const int rendererSortingOrder = -1;
            var center = _tilemap.GetCellCenterWorld(_origin);
            var cellSize = _tilemap.cellSize;
            var mat = new Material(Shader.Find("Sprites/Default"))
            {
                color = new Color(1f, 0.2f, 0.2f, 0.4f)
            };

            _vRect = CreateQuad(transform, center, new Vector3(cellSize.x, (_up + _down + 1) * cellSize.y, 1));
            _vRenderer = _vRect.GetComponent<Renderer>();
            _vRenderer.material = mat;
            _vRenderer.sortingOrder = rendererSortingOrder;

            _hRect = CreateQuad(transform, center, new Vector3((_left + _right + 1) * cellSize.x, cellSize.y, 1));
            _hRenderer = _hRect.GetComponent<Renderer>();
            _hRenderer.material = mat;
            _hRenderer.sortingOrder = rendererSortingOrder;
        }

        private static GameObject CreateQuad(Transform parent, Vector3 position, Vector3 scale)
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.SetParent(parent);
            quad.transform.position = position;
            quad.transform.localScale = scale;
            Destroy(quad.GetComponent<Collider>());
            return quad;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            var flashDur =  _stats.flashDuration;
            if (!_isFlashing && _timer <= flashDur)
                _isFlashing = true;

            if (_isFlashing)
            {
                var visible = Mathf.FloorToInt(_timer * 10) % 2 == 0;
                if (_vRenderer) _vRenderer.enabled = visible;
                if (_hRenderer) _hRenderer.enabled = visible;
            }

            if (_timer <= 0)
                Explode();
        }

        private void Explode()
        {
            var pos = _tilemap.WorldToCell(transform.position);

            for (var i = -_down; i <= _up; i++)
                TryDestroy(pos + new Vector3Int(0, i, 0));
            for (var i = -_left; i <= _right; i++)
            {
                if (i == 0) continue;
                TryDestroy(pos + new Vector3Int(i, 0, 0));
            }

            if (_vRect) Destroy(_vRect);
            if (_hRect) Destroy(_hRect);
            Destroy(gameObject);
        }

        private void TryDestroy(Vector3Int cell)
        {
            if (_tilemap.HasTile(cell))
                _tilemap.SetTile(cell, GameData.Instance.roadTile);
        }
    }
}
