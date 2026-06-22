using ET.SupportKit.ChainCondition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using ET.SupportKit.Logic;
using ET.Engine;
using ET.SupportKit.Mutable;

namespace ET.Engine.Event
{
    public class BaseSensor : MonoBehaviour, IBaseSensor
    {
        public UnityEvent onSignalChange;
       
        public GreenStateData GreenStateData
        {
            get
            {
                return greenStateData;
            }
            set
            {
                if (value.onValueChangeChecker)
                {
                    greenStateData = value;
                    onSignalChange.Invoke();
                }
            }
        }
        public GreenStateData greenStateData;
        public bool IsGreen =>TryPushGreenCondition(greenStateData.isGreen);
        public bool IsChannelGreen(QuickChannelID quickChannelID)
        {
            bool success = greenStateData.channelsGreenState.TryGetValue(quickChannelID, out bool ret);
            if (success) 
                return ret;
            return false;
        }
        public string id;
        public string ID
        {
            get { return id; }
        }

        public SensorData Data { get => data; set => data = value; }
        public SensorData data;

        public SingleConditionChannel_alpha1 triggerEventIf;

        private void Awake()
		{
            //id = Indentifix.Process(this);
            data.Init(); // all object that detected by sensor
            greenStateData.Init();
            triggerEventIf.Construct();
        }
        public bool TryPushGreenCondition(bool mainSignal)
        {

            if (triggerEventIf.channelAdvanceLogicBlock.logicGate == LogicGateAdvance.None)
            {
                return mainSignal;
            }
            else
            {
                return triggerEventIf.channelAdvanceLogicBlock.logicGateData.PushSignal(mainSignal);
            }
        }
        protected void UpdateState()
        {
            GreenStateData = triggerEventIf.ProcessData(GreenCheck, greenStateData);
        }
        private void Update()
        {
        }
        public virtual void LocalUpdate()
        {

        }
        public virtual Dictionary<string,object> DataConstruct()
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            //construct nessacsary value
            return ret;
        }
            
        public void OnValueChange(bool value)
        {
        }
        public bool GreenCheck(ConditionItem item)
        {
            bool ret = ConditionCheck(item);
            if (ret == false) ret = ConditionCheckBuildIn(item);
            LogicCheck(ref ret, item);
            return ret;
        }
        public virtual bool ConditionCheck(ConditionItem item)
        {
            switch (item.type)
            {
                case ChainConditionCheckType.Equal:
                    Debug.Log("No expectation!");
                    break;
                case ChainConditionCheckType.MoreThan:
                    Debug.Log("No expectation!");
                    break;
                case ChainConditionCheckType.LessThan:
                    Debug.Log("No expectation!");
                    break;
                case ChainConditionCheckType.HaveName:
                    Debug.Log("No expectation!");
                    break;
                case ChainConditionCheckType.HaveTag:
                    Debug.Log("No expectation!");
                    break;
                default:
                    break;
            }
            return false;

        }
        bool ConditionCheckBuildIn(ConditionItem item)
        {
            switch (item.type)
            {
                case ChainConditionCheckType.MouseOver:
                    if (data.isMouseOver) return true;
                    break;
            }
            return false;
        }
        void LogicCheck(ref bool ret, ConditionItem item)
        {
           // if (item.logicGate!= LogicGateAdvance.None && item.logicGateData!=null)
           // {
           //     ret = item.logicGateData.PushSignal(ret);
           // }
        }
        void LogicProceed(ref bool ret, ConditionItem item)
        {
           //if (item.logicGate != LogicGateAdvance.None && item.logicGateData != null)
           //{
           //    ret = item.logicGateData.PushSignal(ret);
           //}
        }
        void LogicCheck2(ConditionItem item)
        {
           // if (item.logicGate != LogicGateAdvance.None && item.logicGateData != null)
           // {
           //   //  GreenStateData = GreenStateData.channelsGreenState[item.]
           //   //  ret = item.logicGateData.PushSignal(ret);
           // }
        }
        public class LogicGateQuickmanager: MonoBehaviour
        {

        }

        //logic struct
        public struct ChannelLogicGate
        {
            public QuickChannelID ID;
            public IETLogic logicGate;
        }

        //EVENT//

        private void OnMouseEnter()
        {
            data.isMouseOver = true;
        }
        private void OnMouseExit()
        {
            data.isMouseOver = false;
        }
    }
}