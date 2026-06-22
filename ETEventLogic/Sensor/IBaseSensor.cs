using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit.Collection;

namespace ET.Engine.Event
{
    public interface IBaseSensor : IIDItem
    {
        public SensorData Data { get; set; }
    }
    public struct SensorData
    {
        public List<GameObject> detectedObjects;
        public RaycastHit2D[] detectedRaycastHit2D;
        //mouse
        public bool isMouseOver;
        public void Init()
        {
            detectedObjects = new();
            isMouseOver = false;
        }
        public T GetObject<T>() where T : Component
        {
            if (detectedObjects.Count>0)
            {
                foreach (GameObject item in detectedObjects)
                {
                    if (item.GetComponent<T>() != null) return item.GetComponent<T>();
                }
            }
            else
            {
                D.Sys.ChainConditionWarning("SensorData: Try get object in empty detectedObjectList!");
            }
            return null;
        }
        public List<T> GetObjects<T>() where T : Component
        {
            List<T> ret = new();
            if (detectedObjects.Count > 0)
            {
                foreach (GameObject item in detectedObjects)
                {
                    if (item.GetComponent<T>() != null) ret.Add(item.GetComponent<T>());
                }
                return ret;
            }
            else
            {
                D.Sys.ChainConditionWarning("SensorData: Try get objects in empty detectedObjectList!");
            }
            return null;
        }
        public GameObject Find(Predicate<GameObject> predicate)
        {
            return detectedObjects.Find(predicate);
        }

    }

    public enum SensorRay2DType
    {
        Single,
        All,
    }
    public enum IgnoreType
    {
        Self,
        Closest,
    }
}
