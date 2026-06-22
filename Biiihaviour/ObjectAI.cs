using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ET.GO.Behaviour;
namespace ET.AI
{
    public class ObjectAI : MonoBehaviour
    {
        public Event[] events;
        public Condition[] conditions;
        public Action listBH;

    }
    [Serializable]
    public struct Condition
    {

    }
    [Serializable]
    public struct Event
    {

    }
    [Serializable]
    public struct Action
    {
        public List<Biiihaviour> listBH;
    }
}

