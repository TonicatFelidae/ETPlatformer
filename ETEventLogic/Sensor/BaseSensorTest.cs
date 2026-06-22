using ET.SupportKit.ChainCondition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using ET.SupportKit.Logic;

namespace ET.Engine.Event
{
    public class BaseSensorTest : MonoBehaviour, IBaseSensor
    {
        public UnityAction onSignalChange;
        public bool isGreen;
        public string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public SensorData Data { get => data; set => data = value; }
        public SensorData data;

        public ChainConditionItemSensor greenCondition;

        private void Awake()
		{
             data.Init();

        }
        private void Start()
        {
            //greenCondition.Construct();
        }
        public void ProcessData()
        {

        }
       //public virtual void Update()
       //{
       //    bool newIsGreen = greenCondition.IsGreen(GreenCheck);
       //    if (newIsGreen != isGreen)
       //    {
       //        isGreen = newIsGreen;
       //        onSignalChange?.Invoke();
       //    }
       //}
        public void OnValueChange(bool value)
        {
        }
        public bool GreenCheck(ConditionItem item)
        {
            bool ret = ConditionCheck(item);
            if (ret == false) ret = ConditionCheckBuildIn(item);
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