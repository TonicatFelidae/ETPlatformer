using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit.Logic;
using ET.Engine;
using ET.PowerStruct;
using ET.SupportKit.Collection;

namespace ET.SupportKit.ChainCondition
{
    [Serializable]
    public class SingleConditionChannel_alpha1
    {
        public LogicBlockBasicAlternative channelBasicLogicBlock;
        public LogicBlockAdvance channelAdvanceLogicBlock;
        public List<SingleConditionChannelItem_alpha1> channels;
        public void Construct()
        {
            if (channels.Count > 0)
            {
                foreach (var item in channels)
                {
                    item.condition.Construct();
                    channelAdvanceLogicBlock.Construct();
                }
            }
        }
    }
    [Serializable]
    public struct SingleConditionChannelItem_alpha1
    {
        public QuickChannelID channelID;
        public SingleCondition condition;
    }
    [Serializable]
    public struct SingleCondition
    {
        public LogicGateBasic basicLogicGate;
        public List<ConditionItem> items;
        /// <summary>
        /// Construct logic gate
        /// </summary>
        public void Construct() 
        {
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ConditionItem curItem = items[i];
                    //curItem.logicGateData = ConstructLogicGate(items[i]);
                    items[i] = curItem;
                }
            }
            else
            {
                D.Sys.ChainConditionWarning("SingleCondition: Condition list is empty!");
            }
        }
        
    }

    


}
