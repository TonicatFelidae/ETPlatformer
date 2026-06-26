using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ET.GridSystem.PhysicGrid
{
    /// <summary>
    /// Physic tile, fully monobehaviour
    /// </summary>
    public class XTile : MonoBehaviour
    {
        [SerializeField] Transform _tileTarget;
        [SerializeField] GameObject _matNormal;
        [SerializeField] GameObject _matMove;
        [SerializeField] GameObject _matUserCast;
        [SerializeField] GameObject _matOpponentCast;
        [SerializeField] GameObject _matAttack;
        [SerializeField] GameObject _matSkill;
        [SerializeField] TMP_Text _textDebug;
        Vector3 _rootScale;

        //Stack<CombatAction> _priorities;
        public Transform[] points;
        public Vector2Int loc;
        public int c;
        public int r;
        public void TurnAllPointNum(bool enable)
        {
            foreach (var item in points)
            {
                item.gameObject.SetActive(enable);
            }
        }
        public void SetPointNum(int id, int num)
        {
            points[id].gameObject.SetActive(true);
            points[id].Find("Text (TMP)").GetComponent<TMP_Text>().text = num.ToString();
        }
        public void Setup(int c, int r)
        {
            _rootScale = _tileTarget.localScale;
            //_priorities = new Stack<CombatAction>();
            this.c = c;
            this.r = r;
            _textDebug.text = $"{c},{r}";
            loc = new Vector2Int(c, r);
        }

        public void SetTile(HighlightType type)
        {
            _matNormal.SetActive(type == HighlightType.Normal);
            _matMove.SetActive(type == HighlightType.Moveable);
            _matUserCast.SetActive(type == HighlightType.UserRange);
            _matOpponentCast.SetActive(type == HighlightType.OpponentRange);
            _matAttack.SetActive(type == HighlightType.Attackable);
            _matSkill.SetActive(type == HighlightType.CastSkill);

            //switch (type)
            //{
            //    case HighlightType.Moveable:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("GTile"));
            //        break;
            //    case HighlightType.Attackable:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("GTile"));
            //        break;
            //    case HighlightType.UserRange:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("Gameplay"));
            //        break;
            //    case HighlightType.OpponentRange:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("Gameplay"));
            //        break;
            //    case HighlightType.CastSkill:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("Gameplay"));
            //        break;
            //    default:
            //        setLayer(this.gameObject, LayerMask.NameToLayer("Gameplay"));
            //        break;
            //}
        }
        public UnityEvent OnMouseEnterEvent;

        public void EnableTarget(bool enabled)
        {
            _tileTarget.localScale = _rootScale * (enabled ? 0.8f : 1f);
        }

        private void OnMouseEnter()
        {
            OnMouseEnterEvent?.Invoke();
        }

        //void setLayer(GameObject go, int layerNumber)
        //{
        //    if (go == null) return;
        //    foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        //    {
        //        trans.gameObject.layer = layerNumber;
        //    }
        //}
    }

}