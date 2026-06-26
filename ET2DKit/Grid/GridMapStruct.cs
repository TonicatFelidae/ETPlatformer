using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GridSystem
{
    struct OrderAndPos
    {
        public int index;
        public PositionPresents position;

        public OrderAndPos(int order, PositionPresents position)
        {
            this.index = order;
            this.position = position;
        }
    }
    public struct MovableAttackableTiles
    {
        public List<Vector2Int> moveable;
        public List<Vector2Int> attackable;
        public List<Vector2Int> border;
        public List<byte> borderPointType;
        public Dictionary<Vector2Int, List<Vector2Int>> attackablePerPanel;
        public Dictionary<Vector2Int, List<Vector2Int>> borderPerPanel;
        public Dictionary<Vector2Int, int[,]> mapmodPerPanel;


        public void Init()
        {
            moveable = new();
            attackable = new();
            border = new();
            attackablePerPanel = new();
            borderPerPanel = new();
            mapmodPerPanel = new();
        }
    }
    public enum HighlightType
    {
        Normal,
        Moveable,
        Attackable,
        UserRange,
        OpponentRange,
        CastSkill,
        AttackableAndMoveable,
    }
}
