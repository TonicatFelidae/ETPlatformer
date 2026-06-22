using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit.ChainCondition;

namespace ET.Engine.Event
{
    //Collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached.
    //Collision events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in response to collisions.  
    [RequireComponent(typeof(Collision2D))]
    public class Sensor_collider2D : BaseSensor
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Data.detectedObjects.Add(collision.collider.gameObject);
            Debug.Log(collision.collider.name + "Enter");
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            Data.detectedObjects.Remove(collision.collider.gameObject);
            Debug.Log(collision.collider.name + "Exit");
        }
        public override bool ConditionCheck(ConditionItem item)
        {
            if (Data.detectedObjects.Count > 0)
            {
                int count = Data.detectedObjects.Count;
                switch (item.type)
                {
                    case ChainConditionCheckType.Equal:
                        if (count == (int)item.number) return true;
                        break;
                    case ChainConditionCheckType.MoreThan:
                        if (count > (int)item.number) return true;
                        break;
                    case ChainConditionCheckType.LessThan:
                        if (count < (int)item.number) return true;
                        break;
                    case ChainConditionCheckType.HaveName:
                        foreach (var col in Data.detectedObjects)
                        {
                            if (col.name == item.text) return true;
                        }
                        break;
                    case ChainConditionCheckType.HaveTag:
                        foreach (var col in Data.detectedObjects)
                        {
                            if (col.tag == item.text) return true;
                        }
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
    }
}
