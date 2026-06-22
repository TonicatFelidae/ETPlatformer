using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit.ChainCondition;
using UnityEditor;

namespace ET.Engine.Event
{
    public class Sensor_FOV : BaseSensor
    {
        public int distance; // 0 infinity
        float _distance;
        public SensorRay2DType rayType;
        RaycastHit2D[] _rayHit;
        private void Awake()
        {
            if (distance == 0) _distance = Mathf.Infinity;
            else _distance = distance;
        }
        public void FixedUpdate()
        {
#if UNITY_EDITOR
            if (distance == 0) _distance = Mathf.Infinity;
            else _distance = distance;
#endif
            switch (rayType)
            {
                case SensorRay2DType.Single:
                    _rayHit[0] = data.detectedRaycastHit2D[0] = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), _distance);
                    break;
                case SensorRay2DType.All:
                    _rayHit = data.detectedRaycastHit2D = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector2.up), _distance);
                    break;
                default:
                    break;
            }
        }
        public override bool ConditionCheck(ConditionItem item)
        {
            if (_rayHit.Length > 0)
            {
                if (_rayHit.Length > 0)
                {
                    switch (item.type)
                    {
                        case ChainConditionCheckType.Equal:
                            if (_rayHit.Length == (int)item.number) return true;
                            break;
                        case ChainConditionCheckType.MoreThan:
                            if (_rayHit.Length > (int)item.number) return true;
                            break;
                        case ChainConditionCheckType.LessThan:
                            if (_rayHit.Length < (int)item.number) return true;
                            break;
                        case ChainConditionCheckType.HaveName:
                            foreach (var ray in _rayHit)
                            {
                                if (ray.collider.name == item.text) return true;
                            }
                            break;
                        case ChainConditionCheckType.HaveTag:
                            foreach (var ray in _rayHit)
                            {
                                if (ray.collider.tag == item.text) return true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            float gizmo_distance = distance == 0 ? 1000 : distance;
            if (_rayHit != null && _rayHit.Length > 0)
            {
                Gizmos.color = Color.yellow;
                switch (rayType)
                {
                    case SensorRay2DType.Single:
                        gizmo_distance = _rayHit[0].distance;
                        break;
                    case SensorRay2DType.All:
                        break;
                    default:
                        break;
                }
                foreach (var item in _rayHit)
                {
                    Gizmos.DrawWireSphere(item.point, 0.1f);
                }
            }
            Vector3 direction = transform.TransformDirection(Vector3.up * gizmo_distance);
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}