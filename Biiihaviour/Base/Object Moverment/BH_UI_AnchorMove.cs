using UnityEngine;

namespace ET.GO.Behaviour
{
    public class BH_UI_AnchorMove : Biiihaviour
    {
        public Vector2 direction = new();
        private RectTransform _rectTransformBound;
        public void SetRectTransformBound(RectTransform rectTransformBound)
        {
            this._rectTransformBound = rectTransformBound;
        }

        // this is both position and direction
        // posion use for moverment that use bound anchor
        // direction use for move use center anchor
        public void MoveX(float x)
        {
            direction.x = x;
        }
        public void MoveY(float y)
        {
            direction.y = y;
        }
        public override void Update()
        {
            Rect rect = _rectTransformBound.rect;
            Vector2 centerR = rect.center;
            Vector2 offset = centerR + direction * new Vector2(rect.width / 2, rect.height / 2);
            transformx.localPosition = offset;
        }
    }

}