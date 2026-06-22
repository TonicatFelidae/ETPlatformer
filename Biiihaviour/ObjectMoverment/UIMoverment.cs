using UnityEngine;
using UnityEngine.Events;

namespace ET.GO.Behaviour
{
    public class UIMoverment : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform rectTransformBound;
        private BH_UI_AnchorMove anchorMove;
        //public UnityAction<float> onValueChangeX;
        //public UnityAction<float> onValueChangeY;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            anchorMove = new BH_UI_AnchorMove().Setup<BH_UI_AnchorMove>(gameObject, MoveMethod.Translate    );
            anchorMove.SetRectTransformBound(rectTransformBound);

            //onValueChangeX += OnValueChangeX;
            //onValueChangeY += OnValueChangeY;
        }
        public void OnValueChangeX(float value)
        {
            anchorMove.MoveX(value);


        }
        public void OnValueChangeY(float value)
        {
            anchorMove.MoveY(value);


        }
        private void FixedUpdate()
        {
            anchorMove.Update();
        }

    }
}