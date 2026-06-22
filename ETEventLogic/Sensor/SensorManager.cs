using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ET.SupportKit.Collection;
using ET.Engine.Event;
using UnityEngine.Events;
using ET;

[Serializable]
public class SensorManager
{
    // sensor need to excute before other code??
    /// <summary>
    /// sensor will use string so fuck enum, fuck u
    /// </summary>
    /// Inherated sensorcondition from this 
    public List<BaseSensor> sensors; 
    private Dictionary<string, IIDItem> sensorDic;
    public UnityEvent onAnySignalChange = new();

    public void Init()
    {
        sensorDic = sensors.ToDictionary_IItemID_Interface();
        foreach (var item in sensors)
        {
            item.onSignalChange.AddListener(OnAnySignalChange);
        }
    }
    void OnAnySignalChange()
    {
        onAnySignalChange.Invoke();
    }
    public BaseSensor Sensor(string ID)
    {
        return (BaseSensor)sensorDic[ID];
    }
    public enum SensorType
    {
        LegLeft,
        LegRight,
        Area0,
        Area1,
        Area2,
        Area3,
        Area4,
        Area5,
    }
}
