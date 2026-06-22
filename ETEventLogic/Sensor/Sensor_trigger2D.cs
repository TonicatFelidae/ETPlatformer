using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit.ChainCondition;

namespace ET.Engine.Event
{
    //one game object must have istrigger and collider 
    // if both have is triger enable, no collider will happen, same as both dont have rigidbody 
    [RequireComponent(typeof(Collider2D))]
    public class Sensor_trigger2D : BaseSensor
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Data.detectedObjects.Add(collision.gameObject);
            UpdateState();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            Data.detectedObjects.Remove(collision.gameObject);
            UpdateState();
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