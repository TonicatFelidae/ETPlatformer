using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Engine;
using ET.Shooterkit;
using ET.SupportKit;
//using ET.SupportKit.Logic;
using System;

namespace ET.GO.Behaviour
{
    /*
    public class BH_attack : Biiihaviour
    {
        protected IPoolManager _poolManager;
        protected List<BarrelUnit> barrelUnits = new();
        protected Animator _attackAnimator;
        [Serializable]
        public struct BarrelUnit
        {
            public Transform barrel;
            public ProBarrel proBarrel;
            public ETLogic_Filter_Attack shotDelayFilter;
            public string bulletPath;
        }
        public override Biiihaviour Setup(GameObject gameObjectx)
        {
            base.Setup(gameObjectx);
            return this;
        }
        public BH_attack()
        {
        }
        public virtual void Construct(Animator animator)
        {
            _attackAnimator = animator;
            BarrelUnit ret = new BarrelUnit();
            ret.shotDelayFilter = new ETLogic_Filter_Attack(1);
            barrelUnits.Add(ret);
        }
        public void Attack(ControlType shootControlType)
        {

            switch (shootControlType)
            {
                case ControlType.FullAutomatic:
                    Action_FullAutomatic();
                    break;
                case ControlType.Click:
                    Action_Click();

                    break;
                default:
                    break;
            }
        }
        public virtual async void Action_Click()
        {
        }
        public virtual void Action_FullAutomatic()
        {
        }
        public void UpdateBulletDelayFilter()
        {
            barrelUnits[0].shotDelayFilter.UpdateTimeDodgeSignal();
        }

    }
    */
}

