using ET.Engine.Event;
using ET.SupportKit.Logic;
using ET.SupportKit.Collection;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ET.Engine;
using ET.SupportKit.Mutable;

namespace ET.SupportKit.ChainCondition
{
    public delegate bool CheckGreenItem(ConditionItem item);
    public static class SupportKit_ChainCondition
    {
        public static bool IsGreen(this ChainConditionList chainList, CheckGreenItem isGreenItem)
        {
            List<bool> results = new List<bool>();
            if (chainList.items.Count >0)
            {
                foreach (var item in chainList.items)
                {
                    results.Add(isGreenItem(item));
                }
            }
            if (chainList.lists.Count >0)
            {
                foreach (var item in chainList.lists)
                {
                    results.Add(item.IsGreen(isGreenItem));
                }
            }
            switch (chainList.type)
            {
                case LogicGateBasic.And:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == false) return false;
                    }
                    return true;
                case LogicGateBasic.Or:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == true) return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        public static bool IsGreen(this SingleCondition chainList, CheckGreenItem isGreenItem)
        {
            List<bool> results = new List<bool>();
            if (chainList.items.Count > 0)
            {
                foreach (var item in chainList.items)
                {
                    results.Add(isGreenItem(item));
                }
            }
            switch (chainList.basicLogicGate)
            {
                case LogicGateBasic.And:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == false) return false;
                    }
                    return true;
                case LogicGateBasic.Or:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == true) return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public static GreenStateData ProcessData(this SingleConditionChannel_alpha1 chainList, CheckGreenItem isGreenItem, GreenStateData greenState)
        {
            greenState.ChangeWatcherStart();
            List<bool> results = new List<bool>();
            if (chainList.channels.Count > 0)
            {
                foreach (SingleConditionChannelItem_alpha1 item in chainList.channels)
                {
                    bool curRet = item.condition.IsGreen(isGreenItem);
                    results.Add(curRet);
                    greenState.channelsGreenState.ForceAddDetectDifferent(item.channelID, curRet, () => greenState.ChangeWatcherSwitchOn());
                }
            }
            switch (chainList.channelBasicLogicBlock.logicGate)
            {
                case LogicGateBasicAlternative.All:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == false)
                        {
                            ValueChangeChecker.SetValue(ref greenState.isGreen, false, () => greenState.ChangeWatcherSwitchOn());
                            return greenState;
                        }
                    }
                    ValueChangeChecker.SetValue(ref greenState.isGreen, true, () => greenState.ChangeWatcherSwitchOn());
                    return greenState;
                case LogicGateBasicAlternative.Any:
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] == true)
                        {
                            ValueChangeChecker.SetValue(ref greenState.isGreen, true, () => greenState.ChangeWatcherSwitchOn());
                            return greenState;
                        }
                    }
                    ValueChangeChecker.SetValue(ref greenState.isGreen, false, () => greenState.ChangeWatcherSwitchOn());
                    return greenState;
            }
            return greenState;
        }
        public static void ProcessData(this ChainConditionList chainList, CheckGreenItem processData)
        {

        }

    }
    [Serializable]
    public class ChainConditionList
    {
        public LogicGateBasic type;
        public List<ConditionItem> items;
        public List<ChainConditionList> lists;

        public void Construct() //0 this 1 else
        {
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ConditionItem curItem = items[i];
                    //curItem.logicGateData = Construct_AddLogicGate(items[i]);
                    items[i] = curItem;
                }
            }
            if (lists.Count > 0)
            {
                for (int i = 0; i < lists.Count; i++)
                {
                    Construct(lists[i]);
                }
            }
        }
        public void Construct(ChainConditionList chainList) //0 this 1 else
        {
            if (type == 0) chainList = this;
            if (chainList.items.Count > 0)
            {
                for (int i = 0; i < chainList.items.Count; i++)
                {
                    ConditionItem curItem = chainList.items[i];
                    // curItem.logicGateData = Construct_AddLogicGate(chainList.items[i]);
                    chainList.items[i] = curItem;
                }
            }
            if (chainList.lists.Count > 0)
            {
                for (int i = 0; i < chainList.lists.Count; i++)
                {
                    Construct(chainList.lists[i]);
                }
            }
        }
        //private IETLogic Construct_AddLogicGate(ConditionItem item)
        //{
        //    IETLogic ret = null;
        //    switch (item.logicGate)
        //    {
        //        case LogicGateAdvance.None:
        //            break;
        //        case LogicGateAdvance.Buffer:
        //            ret = new ETLogic_Buffer(item.logicValue);
        //            break;
        //        case LogicGateAdvance.Filter:
        //            ret = new ETLogic_Filter(item.logicValue);
        //            break;
        //        case LogicGateAdvance.Memory:
        //            ret = new ETLogic_Memory();
        //            break;
        //        case LogicGateAdvance.Disruptor:
        //            ret = new ETLogic_Disruptor(item.logicValue);
        //            break;
        //        default:
        //            break;
        //    }
        //    return ret;
        //}
    }
    [Serializable]
    public struct ConditionItem
    {
        public ChainConditionCheckType type;
        public float number;
        public string text;
    }

    public enum ChainConditionCheckType // use all and or all or save coding type and improveperformanace for now
    {
        Equal,
        MoreThan,
        LessThan,
        //para
        HaveName,
        HaveTag,
        //BUILD-IN ingnore all other condition
        MouseOver,
    }
    public enum ChainConditionValueType // use all and or all or save coding type and improveperformanace for now
    {
        DetectedObjects,
        DetectedComponents,
        DetectedObjectsCount,
    }
    public enum ChainConditionProcessType
    {
        Count,
        CountTag,
    }
    [Serializable]
    public struct ChainConditionListSensor
    {
        public LogicGateBasic type;
        public List<BaseSensor> items;
        public List<ChainConditionListSensor> lists;

        public void Construct() //0 this 1 else
        {
            //if (items.Count > 0)
            //{
            //    for (int i = 0; i < items.Count; i++)
            //    {
            //        ChainConditionItemSensor curItem = items[i];
            //        curItem.logicGateData = Construct_AddLogicGate(items[i]);
            //        items[i] = curItem;
            //    }
            //}
            //if (lists.Count > 0)
            //{
            //    for (int i = 0; i < lists.Count; i++)
            //    {
            //        Construct(lists[i]);
            //    }
            //}
        }
        public void Construct(ChainConditionListSensor chainList) //0 this 1 else
        {
            //if (type == 0) chainList = this;
            //if (chainList.items.Count > 0)
            //{
            //    for (int i = 0; i < chainList.items.Count; i++)
            //    {
            //        ChainConditionItemSensor curItem = chainList.items[i];
            //        curItem.logicGateData = Construct_AddLogicGate(chainList.items[i]);
            //        chainList.items[i] = curItem;
            //    }
            //}
            //if (chainList.lists.Count > 0)
            //{
            //    for (int i = 0; i < chainList.lists.Count; i++)
            //    {
            //        Construct(chainList.lists[i]);
            //    }
            //}
        }
        private IETLogic Construct_AddLogicGate(ChainConditionItemSensor item)
        {
            IETLogic ret = null;
            switch (item.logicGate)
            {
                case LogicGateAdvance.None:
                    break;
                case LogicGateAdvance.Buffer:
                    //ret = new ETLogic_Buffer(item.number);
                    break;
                case LogicGateAdvance.Filter:
                    //ret = new ETLogic_Filter(item.number);
                    break;
                case LogicGateAdvance.Memory:
                    ret = new ETLogic_Memory();
                    break;
                default:
                    break;
            }
            return ret;
        }
        private object GetCheckValue(ChainConditionItemSensor item)
        {
            switch (item.conditionCheckProtocol.valueType)
            {
                case ChainConditionValueType.DetectedObjects: return GetCheckValue_DetectedObjects(item, item.sensor.data.detectedObjects);
                //case ChainConditionValueType.DetectedComponents: // lol no
            }
            return null;
        }
        object GetCheckValue_DetectedObjects(ChainConditionItemSensor item, List<GameObject> listGO)
        {
            switch (item.conditionCheckProtocol.processType)
            {
                case ChainConditionProcessType.Count:
                    return listGO.Count;
                case ChainConditionProcessType.CountTag:
                    return listGO.Count;
            }
            return null;
        }
        object GetCheckValue_DetectedComponents<T>(ChainConditionItemSensor item, List<T> listCP)
        {
            switch (item.conditionCheckProtocol.processType)
            {
                case ChainConditionProcessType.Count:
                    return listCP.Count;
            }
            return null;
        }

    }
    #region condition sensor
    [Serializable]
    public struct ChainConditionItemSensor
    {
        public Dictionary<string, object> data; // use string because we will have some very specific case... umm no we can just use enum, inheric class for those specific case
        public ConditionCheckProtocol conditionCheckProtocol;
        public LogicGateAdvance logicGate;
        public BaseSensor sensor;

        [NonSerialized] public IETLogic logicGateData;

        public struct ConditionCheckProtocol
        {
            //
            public ChainConditionValueType valueType;
            public UnityEngine.Component valueTypeComnponent;
            //
            public ChainConditionProcessType processType;
            public object[] processValue;
            public ChainConditionProcessValue[] processValues;
            public ChainConditionCheckType checkType;
            public object checkValue;
        }
    }
    [Serializable]
    public struct ChainConditionProcessValue
    {
        public ChainConditionProcessValueType type;
        public object value;
    }
    #endregion
    public enum ChainConditionProcessValueType
    {
        
    }
    public enum SensorDataType
    {
        DO_Count,
        DO_TagPlayer,
        DO_TagEnemy,
    }
    public class DataProcessGame
    {
        public void ProcessData(SensorDataType type, SensorData sensorData, ref Dictionary<SensorDataType, object> dataOut)
        {
            object retValue = null;
            switch (type)
            {
                case SensorDataType.DO_Count:
                    retValue = sensorData.detectedObjects.Count;
                    break;
                case SensorDataType.DO_TagPlayer:
                    //retValue = sensorData.detectedObjects.FindAll(x => x.tag == "")
                    break;
                case SensorDataType.DO_TagEnemy:
                    break;
                default:
                    break;
            }
            dataOut.ForceAdd(type, retValue);
        }
    }
    [Serializable]
    public struct GreenStateData
    {
        //need to keep thing this way
        // allChannelGreenState invoke onValueChange when all signal correct
        //channel green state is for update function 
        public bool onValueChangeChecker;

        public Dictionary<QuickChannelID, bool> channelsGreenStateBuffer;

        public Dictionary<QuickChannelID, bool> channelsGreenState; // green single channel

        public bool isGreen;//put this on update to update logic gate time
        public void Init()
        {
            channelsGreenState = new();
        }
        public void ChangeWatcherStart()
        {
            onValueChangeChecker = false;
        }
        public void ChangeWatcherSwitchOn()
        {
            onValueChangeChecker = true;
        }
    }

}

