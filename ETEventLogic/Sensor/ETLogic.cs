using ET.Engine;
using ET.SupportKit.ChainCondition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ET.PowerStruct;
using ET.SupportKit.Collection;

namespace ET.SupportKit.Logic
{
    public static class ConStruct
    {
        public static Func<LogicSetupDataPort, LogicSetupDataPortType> convertLogicSetupDataPortToDict = x => x.dataType;
    }
    public enum LogicGateBasic
    {
        Or,
        And,
    }
    public enum LogicGateBasicAlternative
    {
        Any, //or
        All, //and
    }
    public enum LogicGateAdvance
    { 
        None,
        Buffer,
        Filter,
        Memory,
        Repeater,
        Disruptor,
    }

    public interface IETLogic
    {
        public void Reset();
        public bool PushSignal(bool signal);
        /// <summary>
        /// Dodge signal, to update time, suitable for shooting system.
        /// </summary>
        public void UpdateTimeDodgeSignal();
        //public void UpdateSignal(bool signal);
    }
    public class ETLogic_Buffer: IETLogic
    {
        public bool isGreen;
        public bool isOn;
        public float time; //float
        private float curtime;
        public ETLogic_Buffer(float time)
        {
            this.time = time;
            curtime = time;
        }
        public bool PushSignal(bool signal)
        {
            if (signal)
            {
                isGreen = true;
                curtime = time;
            }
            UpdateTime();
            return isGreen;
        }
        public void Reset()
        {
            curtime = time;
        }
        public void UpdateSignal(bool signal)
        {
            //signalData.allChannelGreenState = PushSignal(signalData.channelsGreenStateBuffer[quickChannelID]);
        }
        void UpdateTime()
        {
            if (curtime <= 0)
            {
                isGreen = false;
            }
            else
            {
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
        public void UpdateTimeDodgeSignal()
        {

        }
    }
    public class ETLogic_Filter : IETLogic
    {
        public bool isGreen;
        public float time; //float
        private float curtime;
        public ETLogic_Filter(float time)
        {
            this.time = time;
            curtime = time;
        }
        public bool PushSignal(bool signal)
        {
            if (signal)
            {
                UpdateTime();
            }
            else
            {
                isGreen = false;
                curtime = time;
            }
            return isGreen;
        }
        public void Reset()
        {
            curtime = time;
        }
        public void UpdateSignal(GreenStateData signalData, QuickChannelID quickChannelID)
        {
            signalData.channelsGreenStateBuffer[quickChannelID] = PushSignal(signalData.channelsGreenStateBuffer[quickChannelID]);
        }
        void UpdateTime()
        {
            if (curtime <= 0)
            {
                isGreen = true;
            }
            else
            {
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
        public void UpdateTimeDodgeSignal()
        {

            if (curtime > 0)
            {
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
    }
    public class ETLogic_Filter_Attack : IETLogic
    {
        public bool isGreen;
        public float time; //float
        private float curtime;
        public ETLogic_Filter_Attack(float time)
        {
            this.time = time;
            curtime = 0;
        }
        public bool PushSignal(bool signal)
        {
            if (signal)
            {
                UpdateTime();
            }
            else
            {
                isGreen = false;
                curtime = time;
            }
            return isGreen;
        }
        public void Reset()
        {
            curtime = time;
        }
        public void UpdateSignal(GreenStateData signalData, QuickChannelID quickChannelID)
        {
            signalData.channelsGreenStateBuffer[quickChannelID] = PushSignal(signalData.channelsGreenStateBuffer[quickChannelID]);
        }
        void UpdateTime()
        {
            if (curtime <= 0)
            {
                isGreen = true;
            }
            else
            {
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
        public void UpdateTimeDodgeSignal()
        {

            if (curtime > 0)
            {
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
    }
    public class ETLogic_Memory : IETLogic
    {
        public bool isGreen = false;
        public bool PushSignal(bool signal)
        {
            if (signal)
            {
                isGreen = true;
            }
            return isGreen;
        }
        //public void UpdateSignal(GreenStateData signalData, QuickChannelID)
        //{
        //    signalData.channelsGreenStateBuffer[quickChannelID] = PushSignal(signalData.channelsGreenStateBuffer[quickChannelID]);
        //}
        public void Reset()
		{
            isGreen = false;

        }
        public void UpdateTimeDodgeSignal()
        {

        }
    }
    /// <summary>
    /// After receive an green signal, send green signal then send red signal for a buffer time, after that if there is a green signal the circle repeat
    /// </summary>
    public class ETLogic_Disruptor : IETLogic
    {
        public bool isGreen;
        public float time; //float
        private float curtime;
        public ETLogic_Disruptor(float time)
        {
            this.time = time;
            curtime = time;
        }
        public bool PushSignal(bool signal)
        {
            if (signal)
            {
                UpdateTime();
            }
            return isGreen;
        }
        public void UpdateSignal(GreenStateData signalData, QuickChannelID quickChannelID)
        {
            signalData.channelsGreenStateBuffer[quickChannelID] = PushSignal(signalData.channelsGreenStateBuffer[quickChannelID]);
        }
        public void Reset()
        {
            isGreen = false;

        }
        void UpdateTime()
        {
            if (curtime <= 0)
            {
                isGreen = true;
                curtime = time;
            }
            else
            {
                isGreen = false;
                curtime -= UnityEngine.Time.deltaTime;
            }
        }
        public void UpdateTimeDodgeSignal()
        {

        }
    }
    //block struct
    #region LogicBlock
    [Serializable]
    public struct LogicBlockBasic
    {
        public LogicGateBasic logicGate;
        public List<LogicSetupDataPort> setupData;
        public IETLogic logicGateData;
    }
    [Serializable]
    public struct LogicBlockBasicAlternative
    {
        public LogicGateBasicAlternative logicGate;
        public IETLogic logicGateData;
    }
    [Serializable]
    public struct LogicBlockAdvance
    {
        public LogicGateAdvance logicGate;
        public List<LogicSetupDataPort> setupData;
        public Dictionary<LogicSetupDataPortType,LogicSetupDataPort> setupDataDic;
        public IETLogic logicGateData;
        //public Func<LogicSetupDataPort, LogicSetupDataPortType> convertDataPortToDict = x=>x.dataType;
        public void Construct()
        {
            logicGateData = null;
            setupDataDic = setupData.ToDictionary(ConStruct.convertLogicSetupDataPortToDict);
            switch (logicGate)
            {
                case LogicGateAdvance.None:
                    break;
                case LogicGateAdvance.Buffer:
                    logicGateData = new ETLogic_Buffer((float)setupDataDic[LogicSetupDataPortType.TimeA].data.Value);
                    break;
                case LogicGateAdvance.Filter:
                    logicGateData = new ETLogic_Filter((float)setupDataDic[LogicSetupDataPortType.TimeA].data.Value);
                    break;
                case LogicGateAdvance.Memory:
                    logicGateData = new ETLogic_Memory();
                    break;
                case LogicGateAdvance.Disruptor:
                    logicGateData = new ETLogic_Disruptor((float)setupDataDic[LogicSetupDataPortType.TimeA].data.Value);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    [Serializable]
    public struct LogicSetupDataPort
    {
        public LogicSetupDataPortType dataType;
        public ET_Value data;
    }
    public enum LogicSetupDataPortType
    {
        TimeA,
        TimeB,
        TimeC,
    }


    public class Try
    {
        public void ff()
        {

        }
    }



}
