using Skills.SkillScope;
using UnityEngine;

namespace Skills.PlaceBombSkill
{
    public class PlaceBombSkillScope : BaseSkillScope
    {
        private readonly int _range;

        public PlaceBombSkillScope(Vector3Int originCell, int range) : base(originCell)
        {
            _range = range;
        }

        public override Vector3Int[] GetRange()
        {
            var cells = new System.Collections.Generic.List<Vector3Int>();
            for (var i = 1; i <= _range; i++)
            {
                cells.Add(OriginCell + Vector3Int.up * i);
                cells.Add(OriginCell + Vector3Int.down * i);
                cells.Add(OriginCell + Vector3Int.left * i);
                cells.Add(OriginCell + Vector3Int.right * i);
            }
            return cells.ToArray();
        }
    }
}
