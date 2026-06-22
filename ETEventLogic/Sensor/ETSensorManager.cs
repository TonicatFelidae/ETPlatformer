using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ET;
using ET.Engine;
using ET.SupportKit.ChainCondition;
using UnityEngine.Events;
using ET.SupportKit.Collection;

namespace ET.Engine
{
    /*
     * Sensor: on triger event, trigger event
     * Chaincondition: compare any data to any event, if true push true signal
     * Buffer: buff true ultil satified then pish trueAction
     * Action: do action if trueAction
     * 
     */
    public class ETSensorManager: MonoBehaviour
    {
        public ChainConditionListSensor listSensor;
        public Dictionary<string, ETEventItem> eventDictionary;
        public ETEventItem[] eventItems;
        private void Awake()
        {
            eventDictionary = eventItems.ToDictionary_IItemID();
        }
        public void SubcribeEvent(string namex, UnityAction action )
        {
            //ventDictionary[namex] = ;
        }
    }
}
